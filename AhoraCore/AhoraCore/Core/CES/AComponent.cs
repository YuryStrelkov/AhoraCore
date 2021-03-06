﻿using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Shaders;
using OpenTK;

namespace AhoraCore.Core.CES
{

    public enum  ComponentsTypes
    {
        RenderComponent = 0,
        MaterialComponent = 1,
        TransformComponent = 2,
        ShaderComponent = 3,
        NodeComponent = 4,
        GeometryComponent = 5,
        CameraComponent = 6,
        TerrainComponent = 7,
        TerrainSettings = 8,
        TerrainFloraShader = 9,
        TerrainShader=10,
        GUIComponent = 11,
        EmptyComponent = 12
    }

    public abstract class AComponent<ParentType> : UniformBufferedObject,  IBehavoir/// where ParentType : IGameEntity
    {
        public ComponentsTypes ComponentType { get; protected set; }

        public bool IsRenderable { get; set; }

        public string Component { get; protected set; }

        protected ParentType parent;

        public abstract void Delete();

        public abstract void Enable();

        public abstract void Disable();

        public abstract void Input();

        public abstract void Render();
        
        public abstract void Render(AShader shader);

        public abstract void Update();

        public abstract void Clear();

        public void SetParent(ParentType parent)
        {
            this.parent = parent;
        }

        public ParentType GetParent()
        {
            return parent;
        }

        public T GetParent<T>() where T: ParentType
        {
            return (T)parent;
        }
        
        public AComponent():base()
        {
            ComponentType = ComponentsTypes.EmptyComponent;
        }

    }
}