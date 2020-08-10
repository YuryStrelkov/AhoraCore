using AhoraCore.Core.CES;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.Shaders;
using System.Collections.Generic;
using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Materials;
using OpenTK;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.DataManaging;

namespace AhoraCore.Core.GUI
{
    public abstract class GUIComponent : AComponent<IGameEntity>, IGUIComponent
    {
        protected Dictionary<string,Texture> Textures;

        protected Matrix2 InverseClipSpaceTransform;

        protected Vector2 ClipSpacePosition;

        protected AShader GUIShader;

        public bool IsPressed { get; protected set; }

        public bool IsReleased { get; protected set; }

        public bool IsInFocus { get; protected set; }

        protected int texCounter = 0;

        public override void Clear()
        {
            if (UniformBuffer!=null)
            {
                UniformBuffer.Clear();
            }
        }

        public override void Delete()
        {
            if (UniformBuffer != null)
            {
                UniformBuffer.Delete();
            }
        }

        public override void Disable()
        {
            UniformBuffer.Unbind();
        }

        public override void Enable()
        {
            UniformBuffer.Bind();
        }

        public bool HasFocus()
        {
            return false;
        }

        public abstract void OnClick();

        public abstract void OnFocus();

        public abstract void OnRelease();

    ///    public abstract void Render();

        public void RenderDefault()
        {

            GL.Enable(EnableCap.Blend);

            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            foreach (string key in Textures.Keys)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + texCounter);

                Textures[key].Bind();

                GUIShader.SetUniformi(key, texCounter);

                texCounter++;
            }
            GeometryStorageManager.Data.RenderIteam("Point3D");

            GL.Disable(EnableCap.Blend);
        }

        public void AddTexture2GUI(string ID)
        {
            if (TextureStorrage.Textures.Iteams.ContainsKey(ID))
            {
                Textures.Add(ID,TextureStorrage.Textures.Iteams[ID].Data);
                return;
            }
            Textures.Add(ID, TextureStorrage.Textures.Iteams["DefaultTexture"].Data);
        }

        public void AddTexture2GUI(string ID,string storrageID)
        {
            if (TextureStorrage.Textures.Iteams.ContainsKey(storrageID))
            {
                Textures.Add(ID, TextureStorrage.Textures.Iteams[storrageID].Data);
                return;
            }
            Textures.Add(ID, TextureStorrage.Textures.Iteams["DefaultTexture"].Data);
        }

        public void RenderDefault(AShader shader)
        {

            GL.Enable(EnableCap.Blend);

            GL.BlendFunc(BlendingFactor.SrcAlpha,BlendingFactor.OneMinusSrcAlpha);

            foreach (string key in Textures.Keys)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + texCounter);

                Textures[key].Bind();

                shader.SetUniformi(key, texCounter);
                texCounter++;
            }

            texCounter = 0;

            GeometryStorageManager.Data.RenderIteam("Point3D");

            GL.Disable(EnableCap.Blend);
        }

        public override void Input()
        {
        }

        public override void Update()
        {
        }

        public GUIComponent(AShader shdr) :base()
        {
            Component = "GUIComponentData";

            GUIShader = shdr;

            Textures = new Dictionary<string, Texture>();

            ComponentType = ComponentsTypes.GUIComponent;

        }

        public GUIComponent() : base()
        {
            Component = "GUIComponentData";

            Textures = new Dictionary<string, Texture>();

            ComponentType = ComponentsTypes.GUIComponent;
        }
    }
}
