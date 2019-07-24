using AhoraCore.Core.Cameras;
using OpenTK;
using OpenTK.Input;
using System.Runtime.CompilerServices;

namespace AhoraCore.Core.Input
{
    public static class MouseInput
    {
        private static int CursorX1 = 0;
        private static int CursorY1 = 0;
        private static int CursorX2 = 0;
        private static int CursorY2 = 0;
        static Vector3 shiftVertical = Vector3.Zero;
        static Vector3 shiftHorizontal = Vector3.Zero;
        static Vector3 basisY = new Vector3(0, 1, 0);
        static Vector3 basisX = new Vector3(1, 0, 0);
        static float deltaX = 0;
        static float deltaY = 0;
        static bool isPressed = false;

        static public void UpdateMouseStatment()
        {
            MouseDown(Mouse.GetCursorState());
            MouseUp(Mouse.GetCursorState());
            MouseMove(Mouse.GetCursorState());
        }

        static private void MouseDown(MouseState e)
        {
            if (isPressed)
            {
                return;
            }
            if (e.IsAnyButtonDown)
            {
                CursorX1 = e.X;
                CursorY1 = e.Y;
                isPressed = true;
            }
            //// isLeftButton=
        }

        static private void MouseUp(MouseState e)
        {

            if (!e.IsAnyButtonDown)
            {
                isPressed = false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static private void MouseMove(MouseState e)
        {
            if (isPressed && (e.RightButton.Equals(OpenTK.Input.ButtonState.Pressed)))
            {
                //// испорльзовать производную
                CursorX2 = e.X;
                CursorY2 = e.Y;
                deltaX = 200f * ((CursorX2 * 1.0f - CursorX1 * 1.0f) /
                System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width);
                deltaY = 200f * ((CursorY2 * 1.0f - CursorY1 * 1.0f) /
                System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width);
                CameraInstance.Get().AddRotation(deltaX, deltaY);
                //  CameraInstance.Get().updateCamera();

            }
            if (isPressed && (e.MiddleButton.Equals(OpenTK.Input.ButtonState.Pressed)))
            {
                shiftHorizontal = basisX -
                                 ( CameraInstance.Get().GetWorldTransform().Rotation * basisX) *
                                   CameraInstance.Get().GetWorldTransform().Rotation;
                shiftHorizontal.Normalize();
                shiftVertical = basisY -
                                 (  CameraInstance.Get().GetWorldTransform().Rotation * basisY) *
                                    CameraInstance.Get().GetWorldTransform().Rotation;
                shiftVertical.Normalize();
                CameraInstance.Get().Move(shiftVertical.X + shiftHorizontal.X,
                                                               shiftVertical.Y + shiftHorizontal.Y,
                                                               shiftVertical.Z + shiftHorizontal.Z);
                //     CameraInstance.Get().updateCamera();

            }
            else
            {
                CursorX1 = e.X;
                CursorY1 = e.Y;
                CameraInstance.Get().IsUpdated = false;

            }
        }
    }
}
