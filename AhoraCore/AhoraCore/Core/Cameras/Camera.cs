using AhoraCore.Core.CES;
using AhoraCore.Core.CES.Components;
using AhoraCore.Core.Shaders;
using OpenTK;
using System.Runtime.CompilerServices;
namespace AhoraCore.Core.Cameras
{

    public class Camera: GameEntity
    {
        private CameraComponent cam;
        
        public Matrix4 PespectiveMatrix{
            get
            {
                return cam.PespectiveMatrix;
            }
        }

        public Matrix4 ViewMatrix
        {
            get
            {
                return cam.ViewMatrix;
            }
        }

        public bool IsUpdated { get; set; }

        public Camera() : base()
        {
            AddComponent(ComponentsTypes.CameraComponent, new CameraComponent());

            cam = GetComponent<CameraComponent>(ComponentsTypes.CameraComponent);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public void SwitchToFace(int faceIndex)
        {
            cam.SwitchToFace(faceIndex);
            IsUpdated = true;
        }

        public void TiltCamera(float tiltAngle)
        {
            cam.TiltCamera(tiltAngle);
            IsUpdated = true;
        }

        public void Move(float x, float y, float z)
        {
            cam.MoveCam(x,y,z);
     
            IsUpdated = true;
        }

        public void Bind(AShader sdr)
        {
            cam.Bind(sdr);
        }
        public void AddRotation(float x, float y)
        {
            cam.RotateCam(x, y);
            IsUpdated = true;
        }
     }
}
