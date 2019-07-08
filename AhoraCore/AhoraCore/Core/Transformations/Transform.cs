using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.Transformations
{
    public class Transform
    {
        /// <summary>
        /// Матрица поворота
        /// </summary>
        public Matrix4 RotationM { get; private set; }
        /// <summary>
        /// матрица перемещения
        /// </summary>
        public Matrix4 TranslationM { get; private set; }
        /// <summary>
        /// Матрица масштаба
        /// </summary>
        public Matrix4 ScaleM { get; private set; }
        /// <summary>
        /// Произведение поворота и смещения ( а она вообще нужна ? )
        /// </summary>
        public Matrix4 RotAndShift { get; private set; }
        /// <summary>
        /// Матрица модели
        /// </summary>
        public Matrix4 ModelM { get; private set; }
        /// <summary>
        /// Вектор положения
        /// </summary>
        public Vector3 Position { get; private set; }
        /// <summary>
        /// Вектор маштаба
        /// </summary>
        public Vector3 Scale { get; private set; }
        /// <summary>
        /// Кватернион 
        /// </summary>
        public Quaternion RotationQ { get; private set; }
        /// <summary>
        /// Углы Эйлера
        /// </summary>
        public Vector3 EulerRotation { get; private set; }
        
        private void initRotM()
        {
            Matrix4 M = new Matrix4();
            //x
            float xx = RotationQ.X * RotationQ.X;

            float xy = RotationQ.X * RotationQ.Y;
            float xz = RotationQ.X * RotationQ.Z;
            float xw = RotationQ.X * RotationQ.W;
            //y
            float yy = RotationQ.Y * RotationQ.Y;

            float yz = RotationQ.Y * RotationQ.Z;
            float yw = RotationQ.Y * RotationQ.W;
            //z
            float zz = RotationQ.Z * RotationQ.Z;

            float zw = RotationQ.Z * RotationQ.W;


            M.M11 = 1 - 2 * (yy + zz);
            M.M12 = 2 * (xy - zw);
            M.M13 = 2 * (xz + yw);
            M.M14 = 0;

            M.M21 = 2 * (xy + zw);
            M.M22 = 1 - 2 * (xx + zz);
            M.M23 = 2 * (yz - xw);
            M.M24 = 0;

            M.M31 = 2 * (xz - yw);
            M.M32 = 2 * (yz + xw);
            M.M33 = 1 - 2 * (xx + yy);
            M.M34 = 0;

            M.M41 = 0;
            M.M42 = 0;
            M.M43 = 0;
            M.M44 = 1;

            RotationM = M;
        }

      
        public Transform(Vector3 position)
        {
            Position = position;
            Scale = Vector3.One;
            EulerRotation = Vector3.Zero;
            RotationQ = Quaternion.FromEulerAngles(EulerRotation.X, EulerRotation.Y, EulerRotation.Z);

            initRotM();
            TranslationM = Matrix4.CreateTranslation(0, 0, 0);
            RotAndShift = TranslationM;
            ScaleM = Matrix4.CreateScale(Vector3.One);
            refresh();
        }

        public Transform(float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
            Scale = Vector3.One;
            EulerRotation = Vector3.Zero;
            //rotationM = Matrix4.CreateRotationX(0) * Matrix4.CreateRotationY(0) * Matrix4.CreateRotationZ(0);
            RotationQ = Quaternion.FromEulerAngles(EulerRotation.X, EulerRotation.Y, EulerRotation.Z);
            initRotM();
            TranslationM = Matrix4.CreateTranslation(0, 0, 0);
            RotAndShift = TranslationM;
            ScaleM = Matrix4.CreateScale(Vector3.One);
            refresh();

        }

        public void setEulrerAngles(float x, float y, float z)
        {
            EulerRotation = new Vector3(x, y, z);
            RotationQ = Quaternion.FromEulerAngles(EulerRotation.X, EulerRotation.Y, EulerRotation.Z);
            initRotM();
            refresh();


        }

        public void addEulrerAngles(float x, float y, float z)
        {
            EulerRotation=new Vector3(EulerRotation.X + x,
                                      EulerRotation.Y + y,
                                      EulerRotation.Z + z);
            RotationQ = Quaternion.FromEulerAngles(EulerRotation.X, EulerRotation.Y, EulerRotation.Z);
            initRotM();
            refresh();


        }

        public void addPosition(float x, float y, float z)
        {
            Position = new Vector3(Position.X + x,
                                   Position.Y + y,
                                   Position.Z + z);
            TranslationM = Matrix4.CreateTranslation( Position);
            refresh();
        }

        public void setPosition(float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
            TranslationM = Matrix4.CreateTranslation(Position);
            refresh();

        }

        public void setScale(float x, float y, float z)
        {
            Scale = new Vector3(x, y, z);
            ScaleM = Matrix4.CreateScale(x, y, z);
            refresh();


        }
        
        public void setEulrerAngles(Vector3 angles)
        {
            EulerRotation = angles;
            RotationQ = Quaternion.FromEulerAngles(EulerRotation.X, EulerRotation.Y, EulerRotation.Z);
            initRotM();
            ///rotationM = Matrix4.CreateRotationX(angles.X) * Matrix4.CreateRotationY(angles.Y) * Matrix4.CreateRotationZ(angles.Z);
            refresh();


        }

        public void setRotationByMatrix(Matrix4 rotation)
        {
            RotationM = rotation;/// Matrix4.CreateRotationX(angles.X) * Matrix4.CreateRotationY(angles.Y) * Matrix4.CreateRotationZ(angles.Z);
            refresh();
        }

        public void setPosition(Vector3 pos)
        {
            Position = pos;

            TranslationM = Matrix4.CreateTranslation(Position);
            refresh();


        }

        public void addPosition(Vector3 pos)
        {
            Position += pos;

            TranslationM = Matrix4.CreateTranslation(Position);
            refresh();


        }

        public void setScale(Vector3 scl)
        {
            Scale = scl;
            ScaleM = Matrix4.CreateScale(scl.X, scl.Y, scl.Z);
            refresh();
        }
    
        private void refresh()
        {
            ModelM= RotationM * ScaleM * TranslationM;
            RotAndShift = RotationM * TranslationM;
        }
    }
}
