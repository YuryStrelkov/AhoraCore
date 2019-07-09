﻿using OpenTK;
using System;
using System.Runtime.CompilerServices;
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
         
        public float Tilt { get; protected set; }

        public Matrix4 TiltM { get; protected set; }

        public Vector3 LookAt { get; protected set; }

        public Vector3 Right { get; protected set; }

        public Vector3 Up { get; protected set; }

        public Matrix4 PespectiveMatrix;

        public Matrix4 ViewMatrix;

        public float FOV = MathHelper.DegreesToRadians(70);

        public float Aspect = 1;

        public float MoveSpeed;

        public float MouseSensitivity;
      
            public Camera(float sensitivity, float speed, float aspect) : base(Vector3.Zero)
            {
                Tilt = 0;/// -MathHelper.Pi;
                TiltM = Matrix4.Identity;
                MoveSpeed = speed;
                MouseSensitivity = sensitivity;
                Aspect = aspect;
                FOV = MathHelper.DegreesToRadians(90.0f);
                Matrix4.CreatePerspectiveFieldOfView(FOV, aspect, 0.5f, 50000, out PespectiveMatrix);
                LookAt = new Vector3(0, 0, 1);
                ViewMatrix = Matrix4.LookAt(GetWorldTranslation(), GetWorldTranslation() + LookAt, Vector3.UnitY);
            }

            public Camera() : base(Vector3.Zero)
            {
                Tilt = 0;/// -MathHelper.Pi;
                TiltM = Matrix4.Identity;
                FOV = MathHelper.DegreesToRadians(70);
                MoveSpeed = 0.1f;
                MouseSensitivity = 0.5f;
                Aspect = 1;
                FOV = MathHelper.DegreesToRadians(70.0f);
                Matrix4.CreatePerspectiveFieldOfView(FOV, 1.4f, 0.5f, 50000, out PespectiveMatrix);
                LookAt = new Vector3(0, 0, 1);
                ViewMatrix = Matrix4.LookAt(GetWorldTranslation(), GetWorldTranslation() + LookAt, Vector3.UnitY);
            }
        
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]

            public void SwitchToFace(int faceIndex)
            {
                switch (faceIndex)
                {

                    case 0:
                    ViewMatrix = Matrix4.LookAt(GetWorldTranslation(), GetWorldTranslation() + new Vector3(1, 0, 0), new Vector3(0.0f, -1.0f, 0.0f));
                        break;
                    case 1:
                    ViewMatrix = Matrix4.LookAt(GetWorldTranslation(), GetWorldTranslation() + new Vector3(-1, 0, 0), new Vector3(0.0f, -1.0f, 0.0f));
                        break;
                    case 2:
                    ViewMatrix = Matrix4.LookAt(GetWorldTranslation(), GetWorldTranslation() + new Vector3(0, 1, 0), new Vector3(0.0f, 0.0f, 1.0f));
                        break;
                    case 3:
                    ViewMatrix = Matrix4.LookAt(GetWorldTranslation(), GetWorldTranslation() + new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, -1.0f));
                        break;
                    case 4:
                    ViewMatrix = Matrix4.LookAt(GetWorldTranslation(), GetWorldTranslation() + new Vector3(0, 0, 1), new Vector3(0.0f, -1.0f, 0.0f));
                        break;
                    case 5:
                    ViewMatrix = Matrix4.LookAt(GetWorldTranslation(), GetWorldTranslation() + new Vector3(0, 0, -1), new Vector3(0.0f, -1.0f, 0.0f));
                        break;
                }
            }

            public void TiltCamera(float tiltAngle)
            {
                Tilt += tiltAngle;
                TiltM = Matrix4.CreateRotationZ(Tilt);

            }

            public void Move(float x, float y, float z)
            {
                Vector3 offset = new Vector3();
                Vector3 forward = new Vector3((float)Math.Sin(GetWorldRotation().X), 0, (float)Math.Cos(GetWorldRotation().X));
                Vector3 right = new Vector3(-forward.Z, 0, forward.X);

                offset += x * right;
                offset += y * forward;
                offset.Y += z;

                offset.NormalizeFast();
                offset = Vector3.Multiply(offset, MoveSpeed);
                SetWorldTranslation(GetWorldTranslation() + offset);
                ViewMatrix = Matrix4.LookAt(GetWorldTranslation(), GetWorldTranslation() + LookAt, Vector3.UnitY);
            }


            public void AddRotation(float x, float y)
            {

                x = x * MouseSensitivity;
                y = y * MouseSensitivity;

                SetWorldRotation((GetWorldRotation().X + x) % ((float)Math.PI * 2.0f),
                                Math.Max(Math.Min(GetWorldRotation().Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f)
                                , GetWorldRotation().Z);

            LookAt = new Vector3((float)(Math.Sin(GetWorldRotation().X) * Math.Cos(GetWorldRotation().Y)),
                                (float)Math.Sin(GetWorldRotation().Y),
                                (float)(Math.Cos(GetWorldRotation().X) * Math.Cos(GetWorldRotation().Y)));
      
                ViewMatrix = Matrix4.LookAt(GetWorldTranslation(), GetWorldTranslation() + LookAt, Vector3.UnitY);
            }
       
        }
}
