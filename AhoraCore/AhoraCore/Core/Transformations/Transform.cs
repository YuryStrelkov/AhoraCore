using OpenTK;
using System;

namespace AhoraCore.Core.Transformations
{
    public class Transform
    {
        bool ischanged;

      ///  bool ischangedLocal = false;
        
        /// <summary>
        /// Локальный вектор положения
        /// </summary>
        private Vector3 position;
        /// <summary>
        ///Локальный вектор маштаба
        /// </summary>
        private Vector3 scale;
        /// <summary>
        ///Локальный кватернион 
        /// </summary>
        private Quaternion rotationQ;
        /// <summary>
        ///Локальные углы Эйлера
        /// </summary>
        private Vector3 rotation;
        /// <summary>
        /// Мировой вектор положения
        /// </summary>
      /*  private Vector3 PositionWorld;
        /// <summary>
        /// Мировой вектор маштаба
        /// </summary>
        private Vector3 ScaleWorld;
        /// <summary>
        /// Мировой rватернион 
        /// </summary>
        private Quaternion RotationQWorld;
        /// <summary>
        /// Мировые углы Эйлера
        /// </summary>
        private Vector3 EulerRotationWorld;
        */
        private Matrix4 TranformM;

        public Vector3 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                ischanged = true;
            }
        }

        public Vector3 Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;
                ischanged = true;
            }
        }

        public Quaternion RotationQ
        {
            get
            {
                return rotationQ;
            }

            set
            {
                rotationQ = value;
                ischanged = true;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
                ischanged = true;
            }
        }

        //private Matrix4 worldTranformM;

        public Transform(Vector3 position_)
        {
            position = position_;
            ischanged = true;
            scale = new Vector3(1, 1, 1);
            rotation = new Vector3();
            TranformM = new Matrix4();
            rotationQ = new Quaternion();
        }

        public Transform(float x, float y, float z)  
        {
            position = new Vector3(x, y, z); ;
            ischanged = true;
            scale = new Vector3(1, 1, 1);
            rotation = new Vector3();
            TranformM = new Matrix4();
            rotationQ = new Quaternion();
        }

        public Vector3 Translation()
        {
            return Position;
        }

        public void SetTranslation(Vector3 translation)
        {
            ischanged = true;
            position= translation;
        }

        public void SetTranslation(float x, float y, float z)
        {

            ischanged = true;
            position.X = x;
            position.Y = y;
            position.Z = z;// = translation;

        }

        public void SetRotation(Vector3 rotation_)
        {
            ischanged = true;
            rotation = rotation_;
        }

        public void SetRotation(float x, float y, float z)
        {
            ischanged = true;
            rotation.X = x;
            rotation.Y = y;
            rotation.Z = z;
            //= new Vector3(x, y, z);
        }
 
        public void SetScaling(Vector3 scaling)
        {
            ischanged = true;
            scale = scaling;
        }

        public void SetScaling(float x, float y, float z)
        {
            ischanged = true;
            scale.X = x;//
            scale.Y = y;//
            scale.Z = z;//
        }

        public void SetScaling(float s)
        {
            ischanged = true;
            scale.X = s;//
            scale.Y = s;//
            scale.Z = s;//
        }
 
        public Matrix4 GetTransformMat()
        {
            return ischanged? MakeTransformM(ref TranformM, ref ischanged) : TranformM;
        }
        
        private Matrix4 MakeTransformM(ref Matrix4 matrix, ref bool key)
        {
            key = false;
            float cr = (float)Math.Cos(Rotation.X * 0.5);
            float sr = (float)Math.Sin(Rotation.X * 0.5);
            float cp = (float)Math.Cos(Rotation.Y * 0.5);
            float sp = (float)Math.Sin(Rotation.Y * 0.5);
            float cy = (float)Math.Cos(Rotation.Z * 0.5);
            float sy = (float)Math.Sin(Rotation.Z * 0.5);

            float w = cy * cr * cp + sy * sr * sp;
            float x = cy * sr * cp - sy * cr * sp;
            float y = cy * cr * sp + sy * sr * cp;
            float z = sy * cr * cp - cy * sr * sp;

            // Cache some data for further computations
            float x2 = x + x;
            float y2 = y + y;
            float z2 = z + z;

            float xx = x * x2, xy = x * y2, xz = x * z2;
            float yy = y * y2, yz = y * z2, zz = z * z2;
            float wx = w * x2, wy = w * y2, wz = w * z2;

            float scalingX = Scale.X;
            float scalingY = Scale.Y;
            float scalingZ = Scale.Z;

            // Apply rotation and scale simultaneously, simply adding the translation.
            matrix.M11 = (1f - (yy + zz)) * scalingX;
            matrix.M12 = (xy + wz) * scalingX;
            matrix.M13 = (xz - wy) * scalingX;
            matrix.M14 = Position.X;

            matrix.M21 = (xy - wz) * scalingY;
            matrix.M22 = (1f - (xx + zz)) * scalingY;
            matrix.M23 = (yz + wx) * scalingY;
            matrix.M24 = Position.Y;

            matrix.M31 = (xz + wy) * scalingZ;
            matrix.M32 = (yz - wx) * scalingZ;
            matrix.M33 = (1f - (xx + yy)) * scalingZ;
            matrix.M34 = Position.Z;

            matrix.M41 = 0f;
            matrix.M42 = 0f;
            matrix.M43 = 0f;
            matrix.M44 = 1f;

            return matrix;
        }
    }
}
