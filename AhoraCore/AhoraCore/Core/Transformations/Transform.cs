using OpenTK;
using System;

namespace AhoraCore.Core.Transformations
{
    public class Transform
    {
        bool ischangedWorld = false;

        bool ischangedLocal = false;
        
        /// <summary>
        /// Локальный вектор положения
        /// </summary>
        private Vector3 PositionLocal;
        /// <summary>
        ///Локальный вектор маштаба
        /// </summary>
        private Vector3 ScaleLocal;
        /// <summary>
        ///Локальный кватернион 
        /// </summary>
        private Quaternion RotationQLoacal;
        /// <summary>
        ///Локальные углы Эйлера
        /// </summary>
        private Vector3 EulerRotationLoacal;
        /// <summary>
        /// Мировой вектор положения
        /// </summary>
        private Vector3 PositionWorld;
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

        private Matrix4 localTranformM;

        private Matrix4 worldTranformM;

        public Transform(Vector3 position)
        {
            PositionWorld = position;
            ischangedWorld = true;
            ischangedLocal = true;
            PositionLocal = new Vector3();
            ScaleLocal = new Vector3(1, 1, 1);
            EulerRotationLoacal = new Vector3();
            ScaleWorld = new Vector3(1, 1, 1);
            EulerRotationWorld = new Vector3();
            localTranformM = new Matrix4();
            worldTranformM = new Matrix4();
        }

        public Transform(float x, float y, float z)  
        {
            PositionWorld = new Vector3(x, y, z);
            ischangedWorld = true;
            ischangedLocal = true;
            PositionWorld = new Vector3(x,y,z);
            PositionLocal = new Vector3();
            ScaleLocal = new Vector3(1,1,1);
            EulerRotationLoacal = new Vector3();
            ScaleWorld = new Vector3(1, 1, 1);
            EulerRotationWorld = new Vector3();
            localTranformM = new Matrix4();
            worldTranformM = new Matrix4();
        }

        public Vector3 GetWorldTranslation()
        {
            return PositionWorld;
        }

        public void SetWorldTranslation(Vector3 translation)
        {
            ischangedLocal = true;
            PositionWorld = translation;
        }

        public void SetWorldTranslation(float x, float y, float z)
        {

            ischangedLocal = true;
            PositionWorld.X = x;
            PositionWorld.Y = y;
            PositionWorld.Z = z;// = translation;

        }

        public Vector3 GetWorldRotation()
        {
            return EulerRotationWorld;
        }

        public void SetWorldRotation(Vector3 rotation)
        {
            ischangedWorld = true;
            EulerRotationWorld = rotation;
        }

        public void SetWorldRotation(float x, float y, float z)
        {
            ischangedWorld = true;
            EulerRotationWorld.X = x;
            EulerRotationWorld.Y = y;
            EulerRotationWorld.Z = z;
            //= new Vector3(x, y, z);
        }

        public Vector3 GetWorldScaling()
        {
            return ScaleWorld;
        }

        public void SetWorldScaling(Vector3 scaling)
        {
            ischangedWorld = true;
            ScaleWorld = scaling;
        }

        public void SetWorldScaling(float x, float y, float z)
        {
            ischangedWorld = true;
            ScaleWorld.X = x;//
            ScaleWorld.Y = y;//
            ScaleWorld.Z = z;//
        }

        public void SetWorldScaling(float s)
        {
            ischangedWorld = true;
            ScaleWorld.X = s;//
            ScaleWorld.Y = s;//
            ScaleWorld.Z = s;//
        }

        public Vector3 GetLocalTranslation()
        {
            return PositionLocal;
        }

        public void SetLocalTranslation(Vector3 localTranslation)
        {
            ischangedLocal = true;
            PositionLocal = localTranslation;
        }

        public void SetLocalTranslation(float x, float y, float z)
        {
            ischangedLocal = true;
            PositionLocal.X = x;
            PositionLocal.Y = y;
            PositionLocal.Z = z;// = new Vector3(x, y, z);
        }

        public Vector3 GetLocalRotation()
        {
            return EulerRotationLoacal;
        }

        public void SetLocalRotation(Vector3 localRotation)
        {
            ischangedLocal = true;
            EulerRotationLoacal = localRotation;
        }

        public void SetLocalRotation(float x, float y, float z)
        {
            ischangedLocal = true;
            EulerRotationLoacal.X = x;
            EulerRotationLoacal.Y = y;
            EulerRotationLoacal.Z = z;// = new Vector3(x, y, z);
        }

        public Vector3 GetLocalScaling()
        {
            return ScaleLocal;
        }

        public void SetLocalScaling(Vector3 localScaling)
        {
            ischangedLocal = true;
            ScaleLocal = localScaling;
        }

        public void SetLocalScaling(float x, float y, float z)
        {
            ischangedLocal = true;
            ScaleLocal.X = x;
            ScaleLocal.Y = y;
            ScaleLocal.Z = z;
        }

        public Matrix4 GetWorldTransformMat()
        {
            return ischangedWorld? MakeTransformM(ref worldTranformM, ref ischangedWorld) : worldTranformM;
        }

        public Matrix4 GetLocalTransformMat()
        {
            return ischangedLocal ? MakeTransformM(ref localTranformM, ref ischangedLocal) : localTranformM;
        }

        private Matrix4 MakeTransformM(ref Matrix4 matrix, ref bool key)
        {
            key = false;
            float cr = (float)Math.Cos(EulerRotationWorld.X * 0.5);
            float sr = (float)Math.Sin(EulerRotationWorld.X * 0.5);
            float cp = (float)Math.Cos(EulerRotationWorld.Y * 0.5);
            float sp = (float)Math.Sin(EulerRotationWorld.Y * 0.5);
            float cy = (float)Math.Cos(EulerRotationWorld.Z * 0.5);
            float sy = (float)Math.Sin(EulerRotationWorld.Z * 0.5);

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

            float scalingX = ScaleWorld.X;
            float scalingY = ScaleWorld.Y;
            float scalingZ = ScaleWorld.Z;

            // Apply rotation and scale simultaneously, simply adding the translation.
            matrix.M11 = (1f - (yy + zz)) * scalingX;
            matrix.M12 = (xy + wz) * scalingX;
            matrix.M13 = (xz - wy) * scalingX;
            matrix.M14 = PositionWorld.X;

            matrix.M21 = (xy - wz) * scalingY;
            matrix.M22 = (1f - (xx + zz)) * scalingY;
            matrix.M23 = (yz + wx) * scalingY;
            matrix.M24 = PositionWorld.Y;

            matrix.M31 = (xz + wy) * scalingZ;
            matrix.M32 = (yz - wx) * scalingZ;
            matrix.M33 = (1f - (xx + yy)) * scalingZ;
            matrix.M34 = PositionWorld.Z;

            matrix.M41 = 0f;
            matrix.M42 = 0f;
            matrix.M43 = 0f;
            matrix.M44 = 1f;

            return matrix;
        }
    }
}
