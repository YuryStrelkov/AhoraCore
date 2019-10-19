using AhoraCore.Core.Buffers;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.GUI.CharSets;
using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;


namespace AhoraCore.Core.GUI.UIElemets
{
    public class GUIText : GUIComponent
    {
        private FontTypes font;

        public FontTypes GUIFont { get { return font; }
            set
            {
                font = value;
                if (Textures.ContainsKey("TextSamplerTex"))
                {
                    Textures.Remove("TextSamplerTex");
                }
                Textures.Add("TextSamplerTex", CharactesSet.Fonts[font].CharactersMap);
            }
        }

        public override void OnClick()
        {
        }

        public override void OnFocus()
        {
        }

        public override void OnRelease()
        {
        }
        public void setText(string text)
        {
            UniformBuffer.Bind();

            float[] arr = new float[text.Length];
            
            for (int i=0 ; i < text.Length; i++)
            {
                arr[i] = text[i];
            }
            UniformBuffer.UpdateBufferIteam("symbols", arr);

            UniformBuffer.UpdateBufferIteam("fillnes", arr.Length);

            DebugBuffers.displayBufferData(UniformBuffer);    
            UniformBuffer.Unbind();
        }

        public override void Render()
        {
            GL.Enable(EnableCap.Blend);

            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            Bind(GUIShader);

            foreach (string key in Textures.Keys)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + texCounter);

                Textures[key].Bind();

                GUIShader.SetUniformi(key, texCounter);

                texCounter++;
            }
            CharactesSet.Fonts[font].Bind(GUIShader);

            GeometryStorageManager.Data.RenderIteam("Point3D");

            GL.Disable(EnableCap.Blend);
        }

        public override void Render(AShader shader)
        {
            GL.Enable(EnableCap.Blend);

            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            Bind(shader);

            foreach (string key in Textures.Keys)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + texCounter);

                Textures[key].Bind();

                shader.SetUniformi(key, texCounter);

                texCounter++;
            }

            CharactesSet.Fonts[font].Bind(shader);

            GeometryStorageManager.Data.RenderIteam("Point3D");

            GL.Disable(EnableCap.Blend);
        }

        public GUIText(AShader sdr, FontTypes font, int rows, int cols)
        {
            GUIShader = sdr;

            GUIFont = font;

            EnableBuffering("Characters");

            SetBindigLocation(CES.ICES.UniformBindingsLocations.ComponentData);

            UniformBuffer.addBufferItem("rows", 1);

            UniformBuffer.addBufferItem("coloms", 1);

            UniformBuffer.addBufferItem("fillnes", 1);

            UniformBuffer.addBufferItem("capacity", 1);

            UniformBuffer.addBufferItem("symbols", 256);

            ConfirmBuffer();

            UniformBuffer.Bind();

            UniformBuffer.UpdateBufferIteam("rows", rows);

            UniformBuffer.UpdateBufferIteam("coloms", cols);

            UniformBuffer.UpdateBufferIteam("fillnes", 0);
            UniformBuffer.UpdateBufferIteam("capacity", 1);


            UniformBuffer.Unbind();

        }


        public GUIText(FontTypes font, int rows, int cols)
        {
            GUIFont = font;

            EnableBuffering("Characters");

            SetBindigLocation(CES.ICES.UniformBindingsLocations.ComponentData);

            UniformBuffer.addBufferItem("rows",1);

            UniformBuffer.addBufferItem("coloms", 1);
            
            UniformBuffer.addBufferItem("fillnes", 2);

            UniformBuffer.addBufferItem("symbols", 256);

            ConfirmBuffer();

            UniformBuffer.Bind();

            UniformBuffer.UpdateBufferIteam("rows", rows);

            UniformBuffer.UpdateBufferIteam("cols", cols);

            UniformBuffer.UpdateBufferIteam("fillnes", 0);

            UniformBuffer.Unbind();

        }
    }
}
