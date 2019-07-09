using AhoraCore.Core.Cameras;
using OpenTK;
using OpenTK.Input;
using System.Runtime.CompilerServices;


namespace AhoraCore.Core.Input
{
    public static class KeysInput
    {
        static bool isUpdated = false;

        static float runStep = 0.01f;

        public static bool IsUpdated()
        {
            return isUpdated;
        }

        //static  public KeyboardInput(viewPort vp)
        //{
        //    controlledVP = vp;
        //    vp.KeyPress += keysInput;
        //}
        public static bool UpdateKeysStatment()
        {
            KeysInputCheck(Keyboard.GetState());
            return isUpdated;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static private void KeysInputCheck(KeyboardState e)
        {
            if (isForward())
            {
                CameraInstance.Get().Move(0f, runStep, 0f);
                isUpdated = true;

                DragEnvoirmentModel();
                return;
            }

            if (isBack())
            {
                CameraInstance.Get().Move(0f, -runStep, 0f);
                isUpdated = true;

                DragEnvoirmentModel(); return;
            }

            if (isLeft())
            {
                CameraInstance.Get().Move(-runStep, 0f, 0f);
                isUpdated = true;

                DragEnvoirmentModel(); return;
            }

            if (isRight())
            {
                CameraInstance.Get().Move(runStep, 0f, 0f);
                isUpdated = true;

                DragEnvoirmentModel(); return;
            }

            if (isUp())
            {
                CameraInstance.Get().Move(0f, 0f, runStep);
                isUpdated = true;

                DragEnvoirmentModel(); return;
            }

            if (isDown())
            {
                CameraInstance.Get().Move(0f, 0f, -runStep);
                isUpdated = true;

                DragEnvoirmentModel(); return;
            }

            if (isResetPosition())
            {
                CameraInstance.Get().GetWorldTransform().SetWorldTranslation(new Vector3(0, 0, -1));
                isUpdated = true;    ///        CameraInstance.Get().updateCamera();

                DragEnvoirmentModel(); return;
            }

            if (Keyboard.GetState()[Key.R])
            {
                CameraInstance.Get().TiltCamera(-MathHelper.Pi / 36);
                isUpdated = true;

                return;
            }

            if (Keyboard.GetState()[Key.T])
            {
                CameraInstance.Get().TiltCamera(MathHelper.Pi / 36);
                isUpdated = true;
                return;
            }

            if (isOne())
            {
               // Camera.setCamera("free_cam");
                isUpdated = true;
            }

            if (isTwo())
            {
              //  Camera.setCamera("plane_cam");
                isUpdated = true;
            }

            if (isThree())
            {
                //Camera.setCamera("plane_third_person_cam");
                isUpdated = true;

            }
            isUpdated = false;

            if (isFour())
            {
                //Display.getInstance().GetScene().getSceneRender().displayNormals(); return;
            }

            if (isFive())
            {
              //  Display.getInstance().GetScene().getSceneRender().displaySpecular(); return;
            }

            if (isSix())
            {
                //Display.getInstance().GetScene().getSceneRender().displaySSAO(); return;
            }

            if (isSeven())
            {
                //Display.getInstance().GetScene().getSceneRender().displayTangent(); return;
            }

            if (isExit())
            {
                //Display.getInstance().stopRender();
                //Display.getInstance().Close();
            }


        }

        static private bool isOne()
        {
            return Keyboard.GetState()[Key.Number1];
        }

        static private bool isTwo()
        {
            return Keyboard.GetState()[Key.Number2];
        }

        static private bool isThree()
        {
            return Keyboard.GetState()[Key.Number3];
        }

        static private bool isFour()
        {
            return Keyboard.GetState()[Key.Number4];
        }

        static private bool isFive()
        {
            return Keyboard.GetState()[Key.Number5];
        }

        static private bool isSix()
        {
            return Keyboard.GetState()[Key.Number6];
        }

        static private bool isSeven()
        {
            return Keyboard.GetState()[Key.Number7];
        }

        static private bool isForward()
        {
            return Keyboard.GetState()[Key.W];
        }

        static private bool isBack()
        {
            return Keyboard.GetState()[Key.S];
        }

        static private bool isLeft()
        {
            return Keyboard.GetState()[Key.A];
        }

        static private bool isRight()
        {
            return Keyboard.GetState()[Key.D];
        }

        static private bool isUp()
        {
            return Keyboard.GetState()[Key.E];
        }

        static private bool isDown()
        {
            return Keyboard.GetState()[Key.Q];
        }

        static private bool isResetPosition()
        {
            return Keyboard.GetState()[Key.Z];
        }

        /*  static private bool isFreeOrLockCam()
          {
              lockCam = !lockCam;
              return Keyboard.GetState()[Key.R];
          }*/

        static private bool isExit()
        {
            return Keyboard.GetState()[Key.X];
        }

        static private bool isPlus()
        {
            return Keyboard.GetState()[Key.P];
        }

        static private bool isMinus()
        {
            return Keyboard.GetState()[Key.M];
        }

        static private void DragEnvoirmentModel()
        {
            /*   Display.getInstance().GetScene().GetModel(Display.getInstance().GetScene().getContainer().envoirmentModel).getTransform()
                                                                                   .setPosition(CameraInstance.Get().Position.X,
                                                                                               CameraInstance.Get().Position.Y,
                                                                                               CameraInstance.Get().Position.Z);*/

        }
    }
}
