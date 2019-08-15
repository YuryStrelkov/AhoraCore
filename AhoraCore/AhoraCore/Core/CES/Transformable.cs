using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Transformations;
using AhoraCore.Core.Utils;
using OpenTK;


namespace AhoraCore.Core.CES
{
    public class Transformable: ITransformable
    {
        protected Transform WorldTransform;

        protected Transform LocalTransform;

        protected UniformBufferedObject transformUniformBuffer;

        public Transformable( Vector3 worldPos, Vector3 localPos)
        {
            transformUniformBuffer = new UniformBufferedObject();
            transformUniformBuffer.EnableBuffering("TransformData");
            transformUniformBuffer.MarkBuffer(new string[2] { "localTransform" , "worldTransform" }, new int[2] {16, 16});
            transformUniformBuffer.ConfirmBuffer();
            transformUniformBuffer.SetBindigLocation(UniformBindingsLocations.TransformData);

            WorldTransform = new Transform(worldPos.X, worldPos.Y, worldPos.Z);
            LocalTransform = new Transform(localPos.X, localPos.Y, localPos.Z);

            transformUniformBuffer.UpdateBufferIteam("localTransform", MathUtils.ToArray(LocalTransform.GetTransformMat()));
            transformUniformBuffer.UpdateBufferIteam("worldTransform", MathUtils.ToArray(WorldTransform.GetTransformMat()));
        }

        public Transformable()
        {
            transformUniformBuffer = new UniformBufferedObject();
            transformUniformBuffer.EnableBuffering("TransformData");
            transformUniformBuffer.MarkBuffer(new string[2] { "localTransform", "worldTransform" }, new int[2] { 16, 16 });
            transformUniformBuffer.ConfirmBuffer();
            transformUniformBuffer.SetBindigLocation(UniformBindingsLocations.TransformData);
            WorldTransform = new Transform(0, 0, 0);
            LocalTransform = new Transform(0, 0, 0);

            transformUniformBuffer.UpdateBufferIteam("localTransform", MathUtils.ToArray(Matrix4.Identity));
            transformUniformBuffer.UpdateBufferIteam("worldTransform", MathUtils.ToArray(Matrix4.Identity));

        }

        private void UpdateTransformBuffer(ref Transform transform, string ItemID)
        {
            if (transform.IsChanged)
            {
                transformUniformBuffer.UniformBuffer.Bind();
                transformUniformBuffer.UpdateBufferIteam(ItemID, MathUtils.ToArray(transform.GetTransformMat()));
            }
       }

        public void SetLocalTranslation(float x, float y, float z)
        {
            LocalTransform.SetTranslation(x, y, z);
            UpdateTransformBuffer(ref LocalTransform, "localTransform");
        }

        public void SetWorldTranslation(float x, float y, float z)
        {
            WorldTransform.SetTranslation(x, y, z);
            UpdateTransformBuffer(ref WorldTransform, "worldTransform");

        }

        public void SetLocalTranslation(Vector3 translation)
        {
            LocalTransform.SetTranslation(translation);
            UpdateTransformBuffer(ref LocalTransform, "localTransform");
        }

        public void SetWorldTranslation(Vector3 translation)
        {
            WorldTransform.SetTranslation(translation);
            UpdateTransformBuffer(ref WorldTransform, "worldTransform");

        }

        public void SetLocalScale(float x, float y, float z)
        {
            LocalTransform.SetScaling(x, y, z);
            UpdateTransformBuffer(ref LocalTransform, "localTransform");

        }

        public void SetWorldScale(float x, float y, float z)
        {
            WorldTransform.SetScaling(x, y, z);
            UpdateTransformBuffer(ref WorldTransform, "worldTransform");

        }

        public void SetLocalScale(Vector3 scale)
        {
            LocalTransform.SetScaling(scale);
            UpdateTransformBuffer(ref LocalTransform, "localTransform");
        }

        public void SetWorldScale(Vector3 scale)
        {
            WorldTransform.SetScaling(scale);
            UpdateTransformBuffer(ref WorldTransform, "worldTransform");

        }

        public void SetLocalRotation(float x, float y, float z)
        {
            LocalTransform.SetRotation(x, y, z);
            UpdateTransformBuffer(ref LocalTransform, "localTransform");

        }

        public void SetWorldRotation(float x, float y, float z)
        {
            WorldTransform.SetRotation(x, y, z);
            UpdateTransformBuffer(ref WorldTransform, "worldTransform");

        }

        public void SetLocalRotation(Vector3 rotation)
        {
            LocalTransform.SetRotation(rotation);
            UpdateTransformBuffer(ref LocalTransform, "localTransform");

        }

        public void SetWorldRotation(Vector3 rotation)
        {
            WorldTransform.SetRotation(rotation);
            UpdateTransformBuffer(ref WorldTransform, "worldTransform");

        }

        public Matrix4 GetLocalTransMat()
        {
            return LocalTransform.GetTransformMat();
        }

        public Matrix4 GetWorldTransMat()
        {
            return WorldTransform.GetTransformMat();

        }

        public Vector3 GetLocalPos()
        {
            return LocalTransform.Position;

        }

        public Vector3 GetWorldPos()
        {
            return WorldTransform.Position;
        }

        public Vector3 GetWorldScl()
        {
            return WorldTransform.Scale;
        }

        public Vector3 GetLocalScl()
        {
            return LocalTransform.Scale;
        }

        public Vector3 GetWorldRot()
        {
            return WorldTransform.Rotation;
        }

        public Vector3 GetLocalRot()
        {
            return LocalTransform.Rotation;
        }
    }
}
