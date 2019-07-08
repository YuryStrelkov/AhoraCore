using OpenTK;
using System;
using AhoraCore.Core.Buffers.UniformsBuffer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.Cameras
{
    public enum CameraUniformsNames
    {
        viewMatrix = 0,
        pespectiveMatrix = 1,
        cameraPosition = 2,
        cameraLookAt = 3,
        cameraFOV = 4,
        cameraAspect = 5
    }
    public class Camera:Transformations.Transform
    {
            public string cameraName = "";

            private UniformsBuffer<string> cameraUniformBuffer;
        
            private float tilt = 0;/// -MathHelper.Pi;

            private Matrix4 tiltM = Matrix4.Identity;

            public Vector3 lookAt;

            public Vector3 right;

            public Vector3 up;

            //public Vector3 deltaShift;

            public Matrix4 pespectiveMatrix;

            public Matrix4 viewMatrix;

            private float fov = MathHelper.DegreesToRadians(70);

            private float aspect = 1;

            public float MoveSpeed;

            public float MouseSensitivity;
            /// <summary>
            /// на тот случай, когда мы двинули мышкой или ещё чего
            /// </summary>

            #region Constructors
            public Camera(float sensitivity, float speed, float aspect) : base(Vector3.Zero)
            {
                MoveSpeed = speed;
                MouseSensitivity = sensitivity;
                this.aspect = aspect;
                fov = MathHelper.DegreesToRadians(90.0f);
                Matrix4.CreatePerspectiveFieldOfView(fov, aspect, 0.5f, 50000, out pespectiveMatrix);
                lookAt = new Vector3(0, 0, 1);
                viewMatrix = Matrix4.LookAt(Position, Position + lookAt, Vector3.UnitY);
            }

            public Camera() : base(Vector3.Zero)
            {
                MoveSpeed = 0.1f;
                MouseSensitivity = 0.5f;
                this.aspect = 1;
                fov = MathHelper.DegreesToRadians(70.0f);
                Matrix4.CreatePerspectiveFieldOfView(fov,1.4f, 0.5f, 50000, out pespectiveMatrix);
                lookAt = new Vector3(0, 0, 1);
                viewMatrix = Matrix4.LookAt(Position, Position + lookAt, Vector3.UnitY);
            }
            #endregion


            public void bindCameraToShader(AShader shader)
            {
              ///  cameraUniformBuffer.enableBuffer(shader, "Camera", 0);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void updateCamera()
            {
                try
                {
                  //  cameraUniformBuffer.loadSubData2Buffer("viewMatrix", utils.Matrix2Array((Matrix4.Invert(ParentTransform)) * viewMatrix * tiltM));
                }
                catch (System.InvalidOperationException e)
                {
                    Console.WriteLine("Warning : singular matrix in camera transform chain : "+ e.StackTrace.ToString());
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void switchToFace(int faceIndex)
            {
                switch (faceIndex)
                {

                    case 0:
                        viewMatrix = Matrix4.LookAt(Position, Position + new Vector3(1, 0, 0), new Vector3(0.0f, -1.0f, 0.0f));
                        break;
                    case 1:
                        viewMatrix = Matrix4.LookAt(Position, Position + new Vector3(-1, 0, 0), new Vector3(0.0f, -1.0f, 0.0f));
                        break;
                    case 2:
                        viewMatrix = Matrix4.LookAt(Position, Position + new Vector3(0, 1, 0), new Vector3(0.0f, 0.0f, 1.0f));
                        break;
                    case 3:
                        viewMatrix = Matrix4.LookAt(Position, Position + new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, -1.0f));
                        break;
                    case 4:
                        viewMatrix = Matrix4.LookAt(Position, Position + new Vector3(0, 0, 1), new Vector3(0.0f, -1.0f, 0.0f));
                        break;
                    case 5:
                        viewMatrix = Matrix4.LookAt(Position, Position + new Vector3(0, 0, -1), new Vector3(0.0f, -1.0f, 0.0f));
                        break;
                }
              ///  cameraUniformBuffer.loadSubData2Buffer(0, utils.Matrix2Array(viewMatrix));
            }

            public void tiltCamera(float tiltAngle)
            {
                tilt += tiltAngle;
                tiltM = Matrix4.CreateRotationZ(tilt);

            }

            public void Move(float x, float y, float z)
            {
                Vector3 offset = new Vector3();
                Vector3 forward = new Vector3((float)Math.Sin(EulerRotation.X), 0, (float)Math.Cos(EulerRotation.X));
                Vector3 right = new Vector3(-forward.Z, 0, forward.X);

                offset += x * right;
                offset += y * forward;
                offset.Y += z;

                offset.NormalizeFast();
                offset = Vector3.Multiply(offset, MoveSpeed);
                addPosition(offset);
                viewMatrix = Matrix4.LookAt(Position, Position + lookAt, Vector3.UnitY);
            }

            public void AddRotation(float x, float y)
            {

                x = x * MouseSensitivity;
                y = y * MouseSensitivity;

                setEulrerAngles((EulerRotation.X + x) % ((float)Math.PI * 2.0f),
                                Math.Max(Math.Min(EulerRotation.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f)
                                , EulerRotation.Z);

                lookAt.X = (float)(Math.Sin(EulerRotation.X) * Math.Cos(EulerRotation.Y));
                lookAt.Y = (float)Math.Sin(EulerRotation.Y);
                lookAt.Z = (float)(Math.Cos(EulerRotation.X) * Math.Cos(EulerRotation.Y));

                viewMatrix = Matrix4.LookAt(Position, Position + lookAt, Vector3.UnitY);
            }

           
        }
}
