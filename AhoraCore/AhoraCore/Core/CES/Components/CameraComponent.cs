using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Context;
using AhoraCore.Core.Utils;
using OpenTK;
using System;
using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.CES.Components
{
    class CameraComponent : AComponent<IGameEntity>
    {
       
        public Matrix4 ViewMatrix { get; private set;}

        public Matrix4 ProjectionMatrix { get; private set; }
 
        public Vector3 LookAt { get; protected set; }

        public Vector3 Right { get; protected set; }

        public Vector3 Up { get; protected set; }

        public Vector4[]FrustumPlanes { get; protected set; }

        public Vector4[] LocalFrustumPlanes { get; protected set; }

        public float FOV = MathHelper.DegreesToRadians(70);

        public float Aspect { get; set; }

        public float MoveSpeed;

        public float ZFar { get; set;}

        public float ZNear { get; set; }

        public bool IsUpdated { get; set; }

        public float MouseSensitivity;

        public float TiltAngle = 0;

        public override void Clear()
        {
            UniformBuffer.Clear();
        }

        public override void Delete()
        {
            UniformBuffer.Delete();
        }

        public override void Disable()
        {
        }

        public override void Enable()
        {
        }

        public override void Input()
        {
        }

        public override void Render()
        {
        }

        public override void Update()
        {
        }


      
        private void UpdateFrustumPlanes()
        {
            TransformFrustum2CamSpace();

            UpdateBufferIteam("frustumPlanes", MathUtils.ToArray(LocalFrustumPlanes));
            
            IsUpdated = true;
        }

        public void UpdateView()
        {
            UpdateFrustumPlanes();

            UpdateBufferIteam("viewMatrix", MathUtils.ToArray(ViewMatrix));

            UpdateBufferIteam("cameraLookAt", MathUtils.ToArray(LookAt));

            IsUpdated = true;
        }

        public void UpdateProj()
        {
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(FOV, Aspect, ZNear, ZFar);

            InitLocalFrustumPlanes();

            UpdateFrustumPlanes();

            UpdateBufferIteam("projectionMatrix", MathUtils.ToArray(ProjectionMatrix));

            IsUpdated = true;
        }

        public void UpdatePosition()
        {
            UpdateFrustumPlanes();

            UpdateBufferIteam("viewMatrix", MathUtils.ToArray(ViewMatrix));

            UpdateBufferIteam("cameraPosition", MathUtils.ToArray(GetParent().GetWorldPos()));

            IsUpdated = true;
        }

        public void SwitchToFace(int faceIndex)
        {
            Vector3 target, up;
            IsUpdated = true;
            switch (faceIndex)
            {

                case 0:
                    target = GetParent().GetWorldPos() + new Vector3(1, 0, 0);
                    up = new Vector3(0.0f, -1.0f, 0.0f);
                    break;
                case 1:
                    target = GetParent().GetWorldPos() + new Vector3(-1, 0, 0);
                    up = new Vector3(0.0f, -1.0f, 0.0f);
                    break;
                case 2:
                    target = GetParent().GetWorldPos() + new Vector3(0, 1, 0);
                    up = new Vector3(0.0f, 0.0f, 1.0f);
                    break;
                case 3:
                    target = GetParent().GetWorldPos() + new Vector3(0, -1, 0);
                    up = new Vector3(0.0f, 0.0f, -1.0f);
                    break;
                case 4:
                    target = GetParent().GetWorldPos() + new Vector3(0, 0, 1);
                    up = new Vector3(0.0f, -1.0f, 0.0f);
                    break;
                case 5:
                    target = GetParent().GetWorldPos() + new Vector3(0, 0, -1);
                    up = new Vector3(0.0f, -1.0f, 0.0f);
                    break;
                default:
                    Console.WriteLine("Warning: Default value in Vector3");
                    target = GetParent().GetWorldPos() + new Vector3(1, 0, 0);
                    up = new Vector3(0.0f, -1.0f, 0.0f);
                    break;
            }
            ViewMatrix = Matrix4.LookAt(GetParent().GetWorldPos(), target, up);

            UpdateView();

        }

        public bool IsPointInFrustum(Vector3 point)
        {
            for (   int i = 0; i < 6; i++)
            {
                if (LocalFrustumPlanes[i].X * point.X + LocalFrustumPlanes[i].Y * point.Y + LocalFrustumPlanes[i].Z * point.Z + LocalFrustumPlanes[i].W <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsSphereInFrustum(Vector3 center, float radius)
        {
            for (int i = 1; i < 6; i++)
            {
                if (LocalFrustumPlanes[i].X * center.X + LocalFrustumPlanes[i].Y * center.Y + LocalFrustumPlanes[i].Z * center.Z + LocalFrustumPlanes[i].W <= -radius)
                {
                    return false;
                }
            }
            // Иначе сфера внутри
            return true;
        }

        public bool IsAABBInFrustum(Vector3 minBound, Vector3 maxBound)
        {
            return false;
        }

        public void RotateCam(float x, float y)
        {
            x = x * MouseSensitivity;
            y = y * MouseSensitivity;

            GetParent().SetWorldRotation((GetParent().GetWorldRot().X + x) % ((float)Math.PI * 2.0f),
                                                    Math.Max(Math.Min(GetParent().GetWorldRot().Y + y,
                                                    (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f)
                                                    , GetParent().GetWorldRot().Z);

            LookAt = new Vector3((float)(Math.Sin(GetParent().GetWorldRot().X) * Math.Cos(GetParent().GetWorldRot().Y)),
                                 (float)Math.Sin(GetParent().GetWorldRot().Y),
                                 (float)(Math.Cos(GetParent().GetWorldRot().X) * Math.Cos(GetParent().GetWorldRot().Y)));

            ViewMatrix = Matrix4.LookAt(GetParent().GetWorldPos(), GetParent().GetWorldPos() + LookAt, Vector3.UnitY);
           
            UpdateView();
        }

        public void MoveCam(float x, float y, float z)
        {
            Vector3 offset = new Vector3();

            Vector3 forward = new Vector3((float)Math.Sin(GetParent().GetWorldRot().X), 0, (float)Math.Cos(GetParent().GetWorldRot().X));

            Vector3 right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;

            offset += y * forward;

            offset.Y += z;

            offset.NormalizeFast();

            offset = Vector3.Multiply(offset, MoveSpeed);

            GetParent().SetWorldTranslation(GetParent().GetWorldPos() +  50*offset);

            ViewMatrix = Matrix4.LookAt(GetParent().GetWorldPos(), GetParent().GetWorldPos() + LookAt, Vector3.UnitY);
 
            UpdatePosition();
        }

        private void InitLocalFrustumPlanes()
        {
            FrustumPlanes = new Vector4[6];

            float an = FOV / 2; // * (3.141592653589f / 180.0f);

            float si = (float)Math.Sin(an);

            float co = (float)Math.Cos(an);

            FrustumPlanes[0] = new Vector4(co,  0.0f, -Aspect * si, 0.0f);
            FrustumPlanes[1] = new Vector4(-co, 0.0f, -Aspect * si, 0.0f);

            FrustumPlanes[0].Normalize();
            FrustumPlanes[1].Normalize();

            FrustumPlanes[2] = new Vector4(0.0f, 0.0f, 1, ZFar);
            FrustumPlanes[3] = new Vector4(0.0f, 0.0f,-1,-ZNear);

            FrustumPlanes[4] = new Vector4(0, co, -si, 0.0f);
            FrustumPlanes[5] = new Vector4(0, -co,-si, 0.0f);

            FrustumPlanes[4].Normalize();
            FrustumPlanes[5].Normalize();

 

            LocalFrustumPlanes = new Vector4[6]; 
        }

        private void TransformFrustum2CamSpace()
        {
            for (int i = 0; i < 6; i++)
            {
                LocalFrustumPlanes[i].X = FrustumPlanes[i].X * ViewMatrix.M11 + FrustumPlanes[i].Y * ViewMatrix.M12 + FrustumPlanes[i].Z * ViewMatrix.M13;
                LocalFrustumPlanes[i].Y = FrustumPlanes[i].X * ViewMatrix.M21 + FrustumPlanes[i].Y * ViewMatrix.M22 + FrustumPlanes[i].Z * ViewMatrix.M23;
                LocalFrustumPlanes[i].Z = FrustumPlanes[i].X * ViewMatrix.M31 + FrustumPlanes[i].Y * ViewMatrix.M32 + FrustumPlanes[i].Z * ViewMatrix.M33;
                LocalFrustumPlanes[i].W = FrustumPlanes[i].W + FrustumPlanes[i].X * ViewMatrix.M41 + FrustumPlanes[i].Y *
                                                                ViewMatrix.M42 + FrustumPlanes[i].Z * ViewMatrix.M43;
            }
        }

        public override void Render(AShader shader)
        {
            throw new NotImplementedException();
        }

        public CameraComponent() : base()
        {

            ZFar =  MainContext.ZFar;

            ZNear = MainContext.ZNear;

            Component = "CameraData";

            EnableBuffering("CameraData");

            MarkBuffer(new string[] { "viewMatrix", "projectionMatrix", "frustumPlanes", "cameraPosition", "cameraLookAt" }, new int[] { 16, 16,4*6, 4,4 });

            ConfirmBuffer();

            SetBindigLocation(UniformBindingsLocations.CameraData);
            
            MoveSpeed = 0.1f;

            MouseSensitivity = 0.05f;

            Aspect = MainContext.ScreenAspectRatio;

            FOV = MathHelper.DegreesToRadians(70.0f);

            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(FOV, Aspect, ZNear, ZFar);

            LookAt = new Vector3(0, 0, 1);

            ViewMatrix = Matrix4.LookAt(Vector3.Zero, Vector3.Zero + LookAt, Vector3.UnitY);
            
            UniformBuffer.Bind();
        
            UniformBuffer.UpdateBufferIteam("viewMatrix", MathUtils.ToArray(ViewMatrix));

            UniformBuffer.UpdateBufferIteam("projectionMatrix", MathUtils.ToArray(ProjectionMatrix));

            InitLocalFrustumPlanes();

            UniformBuffer.UpdateBufferIteam("frustumPlanes", MathUtils.ToArray(LocalFrustumPlanes));

            UniformBuffer.UpdateBufferIteam("cameraPosition", new float[] { 0, 0, 0, 1 });

            UniformBuffer.UpdateBufferIteam("cameraLookAt", new float[] { 0, 0, -1, 0 });
            
            UniformBuffer.Unbind();
            
            IsUpdated = true;
        }

    }
}
