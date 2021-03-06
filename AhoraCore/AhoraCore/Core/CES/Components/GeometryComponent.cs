﻿using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.DataManaging;
using System;
using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.CES.Components
{
    public class GeometryComponent : AComponent<IGameEntity>
    {
        public string ModelID { get; set; }

        public override void Clear()
        {
        }

        public override void Delete()
        {
        }

        public override void Disable()
        {
          
        }

        public override void Enable()
        {
          
        }

        public override void Input()
        {
          
        }

        public override void Render()
        {
            GeometryStorageManager.Data.RenderIteam(ModelID);
        }

        public override void Update()
        {

        }

        public override void Render(AShader shader)
        {
            throw new NotImplementedException();
        }

        public GeometryComponent(string ModelID)
        {
            Component = "GeometryData";

            this.ModelID = ModelID;

            ComponentType = ComponentsTypes.GeometryComponent;
        }
    }
}
