using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.IBuffres;
using AhoraCore.Core.Buffers.SpecificBuffers;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Buffers
{
    public class PatchBuffer : ABuffer, IAttribyteable
    {
        private bool isBinded;

        public int VerticesNumber {get; private set; }

        public int VarsPerVertex{ get; private set; }

        public VerticesBuffer VBO { get; private set; }

        public PatchBuffer(float [] vertices, int v_p_v)
        {
            VarsPerVertex = v_p_v;
            Create(vertices.Length);
            Bind();
            VBO.LoadBufferData(vertices);
            Unbind();
        }

        public override void Bind()
        {
            if (isBinded)
            {
                return;
            }
            GL.BindVertexArray(ID);
            VBO.Bind();
            EnableAttribytes();
            isBinded = true;
        }

        public override void Bind(BufferTarget bindTarget)
        {
            
        }

        public override void Clear()
        {
            VBO.Clear();
        }

        public override void Create()
        {
            ID = ID == -1 ? GL.GenVertexArray() : ID;
        }

        public override void Create(int cap)
        {
            VerticesNumber = cap / VarsPerVertex;
            Create();
            GL.BindVertexArray(ID);
            VBO = new VerticesBuffer();
            VBO.Create(cap);
            GL.VertexAttribPointer(0, VarsPerVertex,  VertexAttribPointerType.Float, false, VarsPerVertex * sizeof(float), 0);
            GL.PatchParameter(PatchParameterInt.PatchVertices, VerticesNumber);
            Unbind();
        }

        private void Create(float[] data)
        {
            VerticesNumber = data.Length / VarsPerVertex;
            Create();
            GL.BindVertexArray(ID);
            VBO = new VerticesBuffer();
            VBO.Create(data);
            GL.VertexAttribPointer(0, VarsPerVertex, VertexAttribPointerType.Float, false, VarsPerVertex * sizeof(float), 0);
            GL.PatchParameter(PatchParameterInt.PatchVertices, VerticesNumber);
            GL.BindVertexArray(0);
            VBO.Unbind();
        }

        public override void Delete()
        {
            GL.DeleteVertexArray(ID);
            VBO.Delete();
        }

        public override void Unbind()
        {
            if (!isBinded)
            {
                return;
            }
            GL.BindVertexArray(0);
            VBO.Unbind();
            DisableAttribytes();
            isBinded = false;
        }

        public void EnableAttribytes()
        {
            GL.EnableVertexAttribArray(0);
        }

        public void DisableAttribytes()
        {
            GL.DisableVertexAttribArray(0);
        }

        public void DrawPatch()
        {
            GL.BindVertexArray(ID);
            EnableAttribytes();
            GL.DrawArrays(PrimitiveType.Patches, 0, VerticesNumber);
            DisableAttribytes();
            GL.BindVertexArray(0);
        }
    }
}
