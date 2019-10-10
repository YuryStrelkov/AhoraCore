using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Context;
using AhoraCore.Core.Utils;
using OpenTK;
using System;

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


        private void InitfrustumPlanes()
        {
            // ax * bx * cx +  d = 0; store a,b,c,d
            FrustumPlanes = new Vector4[6];
            //left plane
            Vector4 leftPlane = new Vector4(
                   ProjectionMatrix.M41 + ProjectionMatrix.M11
                        * (float)((Math.Tan(MathUtils.ToRads(FOV/ 2))
                        * ((double)MainContext.ScreenWidth
                         / (double)MainContext.ScreenHeight))),
                   ProjectionMatrix.M42 + ProjectionMatrix.M12,
                   ProjectionMatrix.M43 + ProjectionMatrix.M13,
                   ProjectionMatrix.M44 + ProjectionMatrix.M14);

            FrustumPlanes[0] = MathUtils.NormalizePlane(leftPlane);

            //right plane
            Vector4 rightPlane = new Vector4(
                   ProjectionMatrix.M41 - ProjectionMatrix.M11
                        * (float)((Math.Tan(MathUtils.ToRads(FOV/ 2))
                        * ((double)MainContext.ScreenWidth
                        / (double)MainContext.ScreenHeight))),
                   ProjectionMatrix.M42 - ProjectionMatrix.M12,
                   ProjectionMatrix.M43 - ProjectionMatrix.M13,
                   ProjectionMatrix.M44 - ProjectionMatrix.M14);

            FrustumPlanes[1] = MathUtils.NormalizePlane(rightPlane);

            //bot plane
            Vector4 botPlane = new Vector4(
                   ProjectionMatrix.M41 + ProjectionMatrix.M21,
                   ProjectionMatrix.M42 + ProjectionMatrix.M22
                        * (float)Math.Tan(MathUtils.ToRads(FOV/ 2)),
                   ProjectionMatrix.M43 + ProjectionMatrix.M23,
                   ProjectionMatrix.M44 + ProjectionMatrix.M24);

            FrustumPlanes[2] = MathUtils.NormalizePlane(botPlane);

            //top plane
            Vector4 topPlane = new Vector4(
                   ProjectionMatrix.M41 - ProjectionMatrix.M21,
                   ProjectionMatrix.M42 - ProjectionMatrix.M22
                        * (float)Math.Tan(FOV/ 2),
                   ProjectionMatrix.M43 - ProjectionMatrix.M23,
                   ProjectionMatrix.M44 - ProjectionMatrix.M24);

            FrustumPlanes[3] = MathUtils.NormalizePlane(topPlane);

            //near plane
            Vector4 nearPlane = new Vector4(
                   ProjectionMatrix.M41 + ProjectionMatrix.M31,
                   ProjectionMatrix.M42 + ProjectionMatrix.M32,
                   ProjectionMatrix.M43 + ProjectionMatrix.M33,
                   ProjectionMatrix.M44 + ProjectionMatrix.M34);

            FrustumPlanes[4] = MathUtils.NormalizePlane(nearPlane);

            //far plane
           Vector4 farPlane = new Vector4(
           ProjectionMatrix.M41 - ProjectionMatrix.M31,
           ProjectionMatrix.M42 - ProjectionMatrix.M32,
           ProjectionMatrix.M43 - ProjectionMatrix.M33,
           ProjectionMatrix.M44 - ProjectionMatrix.M34);

            FrustumPlanes[5] = MathUtils.NormalizePlane(farPlane);
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
            for (int i = 0; i < 2; i++)
            {
                if (LocalFrustumPlanes[i].X * point.X + LocalFrustumPlanes[i].Y * point.Y + LocalFrustumPlanes[i].Z * point.Z - LocalFrustumPlanes[i].W <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsSphereInFrustum(Vector3 center, float radius)
        {
            for (int i = 0; i < 2; i++)
            {

                if (LocalFrustumPlanes[i].X * center.X + LocalFrustumPlanes[i].Y * center.Y + LocalFrustumPlanes[i].Z * center.Z + LocalFrustumPlanes[i].W <= radius)
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
            float normalizer = 1,semiFOV = FOV/2f;

            FrustumPlanes = new Vector4[6];

            FrustumPlanes[0] = new Vector4(0, 0,-1, ZFar);

            FrustumPlanes[1] = new Vector4(0, 0, 1, -ZNear);


            FrustumPlanes[2] = new Vector4(0, -1, ZFar, 0);

            normalizer = (float)Math.Sqrt(FrustumPlanes[2].Y * FrustumPlanes[2].Y + FrustumPlanes[2].Z * FrustumPlanes[2].Z);

            FrustumPlanes[2].Y /= normalizer;

            FrustumPlanes[2].Z /= normalizer;

            FrustumPlanes[3] = new Vector4(0, 1, ZFar, 0);

            FrustumPlanes[3].Y /= normalizer;

            FrustumPlanes[3].Z /= normalizer;

            semiFOV = (float)Math.Atan2(1 / Math.Tan(semiFOV), Aspect);

            FrustumPlanes[4] = new Vector4((float)(-Math.Sin(semiFOV) * Math.Cos(semiFOV)), 0, (float)Math.Cos(semiFOV), 0);

            normalizer = (float)Math.Sqrt(FrustumPlanes[4].Y * FrustumPlanes[4].Y + FrustumPlanes[4].Z * FrustumPlanes[4].Z);

            FrustumPlanes[4].X /= normalizer;

            FrustumPlanes[4].Z /= normalizer;

            FrustumPlanes[5] = new Vector4((float)(Math.Sin(semiFOV) * Math.Cos(semiFOV)), 0, (float)Math.Cos(semiFOV), 0);

            FrustumPlanes[5].X /= normalizer;

            FrustumPlanes[5].Z /= normalizer;

            LocalFrustumPlanes = FrustumPlanes;
        }


        private void TransformFrustum2CamSpace()
        {
            ///m.M11, m.M21, m.M31, m.M41,
            ///m.M12, m.M22, m.M32, m.M42,
            ///m.M13, m.M23, m.M33, m.M43,
            ///m.M14, m.M24, m.M34, m.M44

            ///// for colomn major matrices
            for (int i = 0; i < 2; i++)
            {
                LocalFrustumPlanes[i].X = FrustumPlanes[i].X * ViewMatrix.M11 + FrustumPlanes[i].Y * ViewMatrix.M21 + FrustumPlanes[i].Z * ViewMatrix.M31;
                LocalFrustumPlanes[i].Y = FrustumPlanes[i].X * ViewMatrix.M12 + FrustumPlanes[i].Y * ViewMatrix.M22 + FrustumPlanes[i].Z * ViewMatrix.M32;
                LocalFrustumPlanes[i].Z = FrustumPlanes[i].X * ViewMatrix.M13 + FrustumPlanes[i].Y * ViewMatrix.M23 + FrustumPlanes[i].Z * ViewMatrix.M33;
                LocalFrustumPlanes[i].W = FrustumPlanes[i].W  + FrustumPlanes[i].X * ViewMatrix.M14 + FrustumPlanes[i].Y *
                                                                ViewMatrix.M24 + FrustumPlanes[i].Z * ViewMatrix.M34;
            }
        }


        public CameraComponent() : base()
        {

            ZFar = MainContext.ZFar;

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

           /// TransformFrustum2CamSpace();

            UniformBuffer.UpdateBufferIteam("frustumPlanes", MathUtils.ToArray(LocalFrustumPlanes));

            UniformBuffer.UpdateBufferIteam("cameraPosition", new float[] { 0, 0, 0, 1 });

            UniformBuffer.UpdateBufferIteam("cameraLookAt", new float[] { 0, 0, -1, 0 });
            
            UniformBuffer.Unbind();
            
            IsUpdated = true;
        }

    }
}
