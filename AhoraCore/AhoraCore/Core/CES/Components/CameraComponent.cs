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

        public float FOV = MathHelper.DegreesToRadians(70);

        public float Aspect = 1;

        public float MoveSpeed;

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
                   ProjectionMatrix.M41 +ProjectionMatrix.M11
                        * (float)((Math.Tan(MathUtils.ToRads(FOV/ 2))
                        * ((double)MainContext.ScreenWidth
                         / (double)MainContext.ScreenHeight))),
                   ProjectionMatrix.M42 +ProjectionMatrix.M12,
                   ProjectionMatrix.M43 +ProjectionMatrix.M13,
                   ProjectionMatrix.M44 +ProjectionMatrix.M14);

            FrustumPlanes[0] = MathUtils.NormalizePlane(leftPlane);

            //right plane
            Vector4 rightPlane = new Vector4(
                   ProjectionMatrix.M41 -ProjectionMatrix.M11
                        * (float)((Math.Tan(MathUtils.ToRads(FOV/ 2))
                        * ((double)MainContext.ScreenWidth
                        / (double)MainContext.ScreenHeight))),
                   ProjectionMatrix.M42 -ProjectionMatrix.M12,
                   ProjectionMatrix.M43 -ProjectionMatrix.M13,
                   ProjectionMatrix.M44 -ProjectionMatrix.M14);

            FrustumPlanes[1] = MathUtils.NormalizePlane(rightPlane);

            //bot plane
            Vector4 botPlane = new Vector4(
                   ProjectionMatrix.M41 +ProjectionMatrix.M21,
                   ProjectionMatrix.M42 +ProjectionMatrix.M22
                        * (float)Math.Tan(MathUtils.ToRads(FOV/ 2)),
                   ProjectionMatrix.M43 +ProjectionMatrix.M23,
                   ProjectionMatrix.M44 +ProjectionMatrix.M24);

            FrustumPlanes[2] = MathUtils.NormalizePlane(botPlane);

            //top plane
            Vector4 topPlane = new Vector4(
                   ProjectionMatrix.M41 -ProjectionMatrix.M21,
                   ProjectionMatrix.M42 -ProjectionMatrix.M22
                        * (float)Math.Tan(MathUtils.ToRads(FOV/ 2)),
                   ProjectionMatrix.M43 -ProjectionMatrix.M23,
                   ProjectionMatrix.M44 -ProjectionMatrix.M24);

            FrustumPlanes[3] = MathUtils.NormalizePlane(topPlane);

            //near plane
            Vector4 nearPlane = new Vector4(
                   ProjectionMatrix.M41 +ProjectionMatrix.M31,
                   ProjectionMatrix.M42 +ProjectionMatrix.M32,
                   ProjectionMatrix.M43 +ProjectionMatrix.M33,
                   ProjectionMatrix.M44 +ProjectionMatrix.M34);

            FrustumPlanes[4] = MathUtils.NormalizePlane(nearPlane);

            //far plane
           Vector4 farPlane = new Vector4(
           ProjectionMatrix.M41 -ProjectionMatrix.M31,
           ProjectionMatrix.M42 -ProjectionMatrix.M32,
           ProjectionMatrix.M43 -ProjectionMatrix.M33,
           ProjectionMatrix.M44 -ProjectionMatrix.M34);

            FrustumPlanes[5] = MathUtils.NormalizePlane(farPlane);
        }




        public void UpdateFrustumPlanes()
        {
            UpdateBufferIteam("frustumPlanes", MathUtils.ToArray(FrustumPlanes));
            
            IsUpdated = true;
        }

        public void UpdateView()
        {
            UpdateBufferIteam("viewMatrix", MathUtils.ToArray(ViewMatrix));

            UpdateBufferIteam("cameraLookAt", MathUtils.ToArray(LookAt));

            IsUpdated = true;
        }

        public void UpdateProj()
        {
            UpdateBufferIteam("projectionMatrix", MathUtils.ToArray(ProjectionMatrix));
            IsUpdated = true;
        }

        public void UpdatePosition()
        {
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

        public CameraComponent() : base()
        {
            Component = "CameraData";

            EnableBuffering("CameraData");

            MarkBuffer(new string[] { "viewMatrix", "projectionMatrix", "frustumPlanes", "cameraPosition", "cameraLookAt" }, new int[] { 16, 16,4*6, 4,4 });

            ConfirmBuffer();

            SetBindigLocation(UniformBindingsLocations.CameraData);
            
            FOV = MathHelper.DegreesToRadians(70);

            MoveSpeed = 0.1f;

            MouseSensitivity = 0.05f;

            Aspect = MainContext.ScreenAspectRatio;

            FOV = MathHelper.DegreesToRadians(70.0f);

            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(FOV, Aspect, 0.5f, 50000);

            LookAt = new Vector3(0, 0, 1);

            ViewMatrix = Matrix4.LookAt(Vector3.Zero, Vector3.Zero + LookAt, Vector3.UnitY);

            InitfrustumPlanes();

            UniformBuffer.Bind();
        
            UniformBuffer.UpdateBufferIteam("viewMatrix", MathUtils.ToArray(ViewMatrix));

            UniformBuffer.UpdateBufferIteam("projectionMatrix", MathUtils.ToArray(ProjectionMatrix));

            UniformBuffer.UpdateBufferIteam("frustumPlanes", MathUtils.ToArray(FrustumPlanes));

            UniformBuffer.UpdateBufferIteam("cameraPosition", new float[] { 0, 0, 0, 1 });

            UniformBuffer.UpdateBufferIteam("cameraLookAt", new float[] { 0, 0, -1, 0 });
            
            UniformBuffer.Unbind();

            IsUpdated = true;
        }

    }
}
