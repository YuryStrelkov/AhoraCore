using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Transformations;
using AhoraCore.Core.Utils;
using OpenTK;
using System;
using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.CES.Components
{
    public class TransformComponent : AComponent<IGameEntity>
    {
        public Transform WorldTransform { get; protected set; }

        public Transform LocalTransform { get; protected set; }

        public Matrix4  ParentTransform { get; protected set; }

        public override void Clear()
        {
            UniformBuffer.Clear();
        }

        public override void Delete()
        {
            UniformBuffer.Delete();
        }

        public override void Disable()
        {
            UniformBuffer.Unbind();
        }

        public override void Enable()
        {
            UniformBuffer.Bind();
        }

        public override void Input()
        {
          
        }

        public override void Render()
        {
           Bind(GetParent().GetComponent<ShaderComponent>(ComponentsTypes.ShaderComponent).Shader);
        }

        public override void Update()
        {
          Enable();
          UniformBuffer.UpdateBufferIteam("parentTransform",GetParent().GetParentTransMat());
        }

        private void updateLocal()
        {
            Enable();
            UniformBuffer.UpdateBufferIteam("localTransform", MathUtils.ToArray(LocalTransform.GetTransformMat()));
        }

        private void updateWorld()
        {
            Enable();
            UniformBuffer.UpdateBufferIteam("worldTransform", MathUtils.ToArray(GetWorldTransMat()));
    ///        Console.WriteLine(" Parent etntity : " + GameEntityStorrage.Entities.GetParent(parent.EntityID).EntityID + "  This entity : " + parent.EntityID);
            ParentTransform = GetParent().GetParentTransMat() * GetWorldTransMat();
       //     Console.WriteLine(ParentTransform.ToString());
        }



        public void SetLocalTranslation(float x, float y, float z)
        {
            LocalTransform.SetTranslation(x, y, z);
            updateLocal();
        }

        public void SetWorldTranslation(float x, float y, float z)
        {
            WorldTransform.SetTranslation(x, y, z);
            updateWorld();
        }

        public void SetLocalTranslation(Vector3 translation)
        {
            LocalTransform.SetTranslation(translation);
            updateLocal();

        }

        public void SetWorldTranslation(Vector3 translation)
        {
            WorldTransform.SetTranslation(translation);
            updateWorld();

        }

        public void SetLocalScale(float x, float y, float z)
        {
            LocalTransform.SetScaling(x,y,z);
            updateLocal();

        }

        public void SetWorldScale(float x, float y, float z)
        {
            WorldTransform.SetScaling(x,y,z);
            updateWorld();

        }

        public void SetLocalScale(Vector3 scale)
        {
            LocalTransform.SetScaling(scale);
            updateLocal();

        }

        public void SetWorldScale(Vector3 scale)
        {
            WorldTransform.SetScaling(scale);
            updateWorld();

        }

        public void SetLocalRotation(float x, float y, float z)
        {
            LocalTransform.SetRotation(x,y,z);
            updateLocal();

        }

        public void SetWorldRotation(float x, float y, float z)
        {
            WorldTransform.SetRotation(x, y, z);
            updateWorld();

        }

        public void SetLocalRotation(Vector3 rotation)
        {
            LocalTransform.SetRotation(rotation);
            updateLocal();

        }

        public void SetWorldRotation(Vector3 rotation)
        {
            WorldTransform.SetRotation(rotation);
            updateWorld();

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

        public override void Render(AShader shader)
        {
            throw new NotImplementedException();
        }

        public TransformComponent():base()
        {

            Component = "TransformData";

            EnableBuffering("TransformData");
         
            MarkBuffer(new string[3] { "parentTransform", "localTransform", "worldTransform" }, new int[3] { 16, 16, 16 });

            ConfirmBuffer();

            ParentTransform = Matrix4.Identity;

            SetBindigLocation(UniformBindingsLocations.TransformData);

            WorldTransform = new Transform(0, 0, 0);

            LocalTransform = new Transform(0, 0, 0);

            Enable();
            UniformBuffer.UpdateBufferIteam("parentTransform",MathUtils.ToArray(Matrix4.Identity));
            UniformBuffer.UpdateBufferIteam("localTransform", MathUtils.ToArray(LocalTransform.GetTransformMat()));
            UniformBuffer.UpdateBufferIteam("worldTransform", MathUtils.ToArray(WorldTransform.GetTransformMat()));
            Disable();
            ComponentType = ComponentsTypes.TransformComponent;
        }
    }
}
