using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Utils;
using OpenTK;
using System;

namespace AhoraCore.Core.CES.Components
{
    class CameraComponent : AComponent<IGameEntity>
    {
       
        public Matrix4 ViewMatrix { get; private set;}

        public Matrix4 PespectiveMatrix { get; private set; }

        public Matrix4 TiltMatrix { get; private set; }

        public Vector3 LookAt { get; protected set; }

        public Vector3 Right { get; protected set; }

        public Vector3 Up { get; protected set; }
        

        public float FOV = MathHelper.DegreesToRadians(70);

        public float Aspect = 1;

        public float MoveSpeed;

        public bool IsUpdated { get; set; }

        public float MouseSensitivity;

        public float TiltAngle = 0;

        public override void Clear()
        {
        }

        public override void Delete()
        {
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


        public void UpdateView()
        {

        }

        public void UpdateProj()
        {

        }

        public void TiltCamera(float tiltAngle)
        {
            TiltAngle += tiltAngle;
            TiltMatrix  = Matrix4.CreateRotationZ(TiltAngle);
            UpdateBufferIteam("tiltMatix", MathUtils.ToArray(TiltMatrix));
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
            ViewMatrix.Transpose();
            UpdateBufferIteam("viewMatrix", MathUtils.ToArray(ViewMatrix));
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

            UpdateBufferIteam("viewMatrix", MathUtils.ToArray(ViewMatrix));

            IsUpdated = true;
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

            UpdateBufferIteam("viewMatrix", MathUtils.ToArray(ViewMatrix));

            IsUpdated = true;
        }

        public CameraComponent() : base()
        {
            Component = "CameraData";

            EnableBuffering("CameraData");

            MarkBuffer(new string[] { "viewMatrix", "projectionMatrix", "tiltMatix" }, new int[] { 16, 16, 16 });

            ConfirmBuffer();

            SetBindigLocation(UniformBindingsLocations.CameraData);
            
            TiltMatrix = Matrix4.Identity;

            FOV = MathHelper.DegreesToRadians(70);

            MoveSpeed = 0.1f;

            MouseSensitivity = 0.05f;

            Aspect = 1;

            FOV = MathHelper.DegreesToRadians(70.0f);

            PespectiveMatrix = Matrix4.CreatePerspectiveFieldOfView(FOV, 1.4f, 0.5f, 50000);

            LookAt = new Vector3(0, 0, 1);

            ViewMatrix = Matrix4.LookAt(Vector3.Zero, Vector3.Zero + LookAt, Vector3.UnitY);
           
            UniformBuffer.Bind();
        
            UniformBuffer.UpdateBufferIteam("viewMatrix", MathUtils.ToArray(ViewMatrix));

            UniformBuffer.UpdateBufferIteam("projectionMatrix", MathUtils.ToArray(PespectiveMatrix));

            UniformBuffer.UpdateBufferIteam("tiltMatix", MathUtils.ToArray(TiltMatrix));

            UniformBuffer.Unbind();

            IsUpdated = true;
        }

    }
}
