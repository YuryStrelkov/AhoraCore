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

        public string WorldTransformKey { get; protected set; }

        public string LocalTransformKey { get; protected set; }

        public Transformable( Vector3 worldPos, Vector3 localPos)
        {
            transformUniformBuffer = new UniformBufferedObject();
 
                transformUniformBuffer.EnableBuffering("TransformData");
                WorldTransformKey = "worldTransform";
                LocalTransformKey = "localTransform";
       

            transformUniformBuffer.MarkBuffer(new string[2] { LocalTransformKey, WorldTransformKey }, new int[2] { 16, 16 });
            transformUniformBuffer.ConfirmBuffer();
            transformUniformBuffer.SetBindigLocation(UniformBindingsLocations.TransformData);

            WorldTransform = new Transform(worldPos.X, worldPos.Y, worldPos.Z);
            LocalTransform = new Transform(localPos.X, localPos.Y, localPos.Z);

            transformUniformBuffer.UpdateBufferIteam(LocalTransformKey, MathUtils.ToArray(LocalTransform.GetTransformMat()));
            transformUniformBuffer.UpdateBufferIteam(WorldTransformKey, MathUtils.ToArray(WorldTransform.GetTransformMat()));
        }

        public Transformable(string prefix, Vector3 worldPos, Vector3 localPos)
        {
            transformUniformBuffer = new UniformBufferedObject();
            if (prefix.Length == 0)
            {
                transformUniformBuffer.EnableBuffering(prefix + "TransformData");
                WorldTransformKey = "world"+  prefix + "Transform";
                LocalTransformKey = "local" + prefix + "Transform";
            }
            else
            {
                transformUniformBuffer.EnableBuffering("TransformData");
                WorldTransformKey = "worldTransform";
                LocalTransformKey = "localTransform";
            }

            transformUniformBuffer.MarkBuffer(new string[2] { LocalTransformKey , WorldTransformKey }, new int[2] {16, 16});
            transformUniformBuffer.ConfirmBuffer();
            transformUniformBuffer.SetBindigLocation(UniformBindingsLocations.TransformData);

            WorldTransform = new Transform(worldPos.X, worldPos.Y, worldPos.Z);
            LocalTransform = new Transform(localPos.X, localPos.Y, localPos.Z);

            transformUniformBuffer.UpdateBufferIteam(LocalTransformKey, MathUtils.ToArray(LocalTransform.GetTransformMat()));
            transformUniformBuffer.UpdateBufferIteam(WorldTransformKey, MathUtils.ToArray(WorldTransform.GetTransformMat()));
        }

        public Transformable(string prefix)
        {
            transformUniformBuffer = new UniformBufferedObject();
            if (prefix.Length == 0)
            {
                transformUniformBuffer.EnableBuffering(prefix + "TransformData");
                WorldTransformKey = "world" + prefix + "Transform";
                LocalTransformKey = "local" + prefix + "Transform";
            }
            else
            {
                transformUniformBuffer.EnableBuffering("TransformData");
                WorldTransformKey = "worldTransform";
                LocalTransformKey = "localTransform";
            }

            transformUniformBuffer.MarkBuffer(new string[2] { LocalTransformKey, WorldTransformKey }, new int[2] { 16, 16 });
            transformUniformBuffer.ConfirmBuffer();
            transformUniformBuffer.SetBindigLocation(UniformBindingsLocations.TransformData);

            WorldTransform = new Transform(0,0,0);
            LocalTransform = new Transform(0,0,0);

            transformUniformBuffer.UpdateBufferIteam(LocalTransformKey, MathUtils.ToArray(LocalTransform.GetTransformMat()));
            transformUniformBuffer.UpdateBufferIteam(WorldTransformKey, MathUtils.ToArray(WorldTransform.GetTransformMat()));
        }

        public Transformable()
        {
            transformUniformBuffer = new UniformBufferedObject();
            transformUniformBuffer.EnableBuffering("TransformData");
            WorldTransformKey = "worldTransform";
            LocalTransformKey = "localTransform";
            transformUniformBuffer.MarkBuffer(new string[2] { LocalTransformKey, WorldTransformKey }, new int[2] { 16, 16 });
            transformUniformBuffer.ConfirmBuffer();
            transformUniformBuffer.SetBindigLocation(UniformBindingsLocations.TransformData);
            WorldTransform = new Transform(0, 0, 0);
            LocalTransform = new Transform(0, 0, 0);

            transformUniformBuffer.UpdateBufferIteam(LocalTransformKey, MathUtils.ToArray(Matrix4.Identity));
            transformUniformBuffer.UpdateBufferIteam(WorldTransformKey, MathUtils.ToArray(Matrix4.Identity));

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
            UpdateTransformBuffer(ref LocalTransform, LocalTransformKey);
        }

        public void SetWorldTranslation(float x, float y, float z)
        {
            WorldTransform.SetTranslation(x, y, z);
            UpdateTransformBuffer(ref WorldTransform, WorldTransformKey);

        }

        public void SetLocalTranslation(Vector3 translation)
        {
            LocalTransform.SetTranslation(translation);
            UpdateTransformBuffer(ref LocalTransform, LocalTransformKey);
        }

        public void SetWorldTranslation(Vector3 translation)
        {
            WorldTransform.SetTranslation(translation);
            UpdateTransformBuffer(ref WorldTransform, WorldTransformKey);

        }

        public void SetLocalScale(float x, float y, float z)
        {
            LocalTransform.SetScaling(x, y, z);
            UpdateTransformBuffer(ref LocalTransform, LocalTransformKey);

        }

        public void SetWorldScale(float x, float y, float z)
        {
            WorldTransform.SetScaling(x, y, z);
            UpdateTransformBuffer(ref WorldTransform, WorldTransformKey);

        }

        public void SetLocalScale(Vector3 scale)
        {
            LocalTransform.SetScaling(scale);
            UpdateTransformBuffer(ref LocalTransform, LocalTransformKey);
        }

        public void SetWorldScale(Vector3 scale)
        {
            WorldTransform.SetScaling(scale);
            UpdateTransformBuffer(ref WorldTransform, WorldTransformKey);

        }

        public void SetLocalRotation(float x, float y, float z)
        {
            LocalTransform.SetRotation(x, y, z);
            UpdateTransformBuffer(ref LocalTransform, LocalTransformKey);

        }

        public void SetWorldRotation(float x, float y, float z)
        {
            WorldTransform.SetRotation(x, y, z);
            UpdateTransformBuffer(ref WorldTransform, WorldTransformKey);

        }

        public void SetLocalRotation(Vector3 rotation)
        {
            LocalTransform.SetRotation(rotation);
            UpdateTransformBuffer(ref LocalTransform, LocalTransformKey);

        }

        public void SetWorldRotation(Vector3 rotation)
        {
            WorldTransform.SetRotation(rotation);
            UpdateTransformBuffer(ref WorldTransform, WorldTransformKey);

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
