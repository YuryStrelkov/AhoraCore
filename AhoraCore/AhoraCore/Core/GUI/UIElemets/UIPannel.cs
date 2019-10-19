using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.GUI.UIElemets
{
    public class UIPannel : GUIComponent
    {
        public override void Input()
        {
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
        
        public override void Update()
        {
        }

        public override void Render()
        {
            RenderDefault();
        }
        public override void Render(AShader sdr)
        {
            RenderDefault(sdr);
        }

        public UIPannel(AShader shader) : base(shader)
        {
        }

        public UIPannel() : base()
        {
        }
    }
}
