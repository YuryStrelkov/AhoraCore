
namespace AhoraCore.Core.Cameras
{
    public class CameraInstance
    {
        private static Camera camera;// = new simpleCamera();

        private static string cameraName = "";

        public static string CameraName()
        {
            return cameraName;
        }

        public static void setCamera(string cameraName, Camera activeCam)
        {
            camera = activeCam;
        }

        public static void setCamera(string cameraName)
        {
        //    Camera.cameraName = cameraName;
        //    Camera.camera = Display.getInstance().GetScene().GetCamera(cameraName);
        }

        public static Camera Get()
        {
            if (camera == null)
            {
                camera = new Camera();
            }
            return camera;
        }
    }
}
