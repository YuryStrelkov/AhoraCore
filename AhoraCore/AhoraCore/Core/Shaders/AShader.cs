using OpenTK.Graphics.OpenGL;
using System;
using System.IO;


namespace AhoraCore.Core.Shaders
{

    public abstract class AShader
    {
        public int ShaderID { get; private set; }

        public int VertexShaderID  { get; private set; }

        public int FragmentShaderID { get; private set; }

        public int GeometryShaderID { get; private set; }

        public int TesselationShaderID { get; private set; }

        public void Bind()
        {
            GL.UseProgram(ShaderID);
        }

        public void Disable()
        {
            GL.UseProgram(0);
        }

        private  void Link()
        {
            GL.LinkProgram(ShaderID);
            Console.WriteLine(GL.GetProgramInfoLog(ShaderID));
        }

        private void Validate()
        {
            GL.ValidateProgram(ShaderID);
            Console.WriteLine(GL.GetProgramInfoLog(ShaderID));
        }

        public void DeleteShader()
        {
            Disable();
            GL.DetachShader(ShaderID, VertexShaderID);
            GL.DetachShader(ShaderID, FragmentShaderID);
            GL.DeleteShader(VertexShaderID);
            GL.DeleteShader(FragmentShaderID);
            if (GeometryShaderID!=-1)
            {
                GL.DetachShader(ShaderID, GeometryShaderID);
                GL.DeleteShader(GeometryShaderID);
            }
            if (TesselationShaderID != -1)
            {
                GL.DetachShader(ShaderID, TesselationShaderID);
                GL.DeleteShader(TesselationShaderID);
            }
            GL.DeleteProgram(ShaderID);
        }

        public abstract void BindAttribytes();

        public void BindAttributeLocation(int attrIndex,string attrName)
        {
            GL.BindAttribLocation(ShaderID, attrIndex, attrName);
        }

        public void LoadShaderFromFile(string path2shaderCode, ShaderType type)
        {
            try
            {
                StreamReader sr = new StreamReader(Path.GetFullPath(path2shaderCode));
                if (type == ShaderType.VertexShader)
                {
                    VertexShaderID = loadShader(sr.ReadToEnd(), type);
                }
                else if (type == ShaderType.FragmentShader)
                {
                    FragmentShaderID =  loadShader(sr.ReadToEnd(), type);
                }
                else if (type == ShaderType.GeometryShader)
                {
                    GeometryShaderID = loadShader(sr.ReadToEnd(), type);
                }
                else if (type == ShaderType.TessControlShader)
                {
                    TesselationShaderID = loadShader(sr.ReadToEnd(), type);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void LoadShaderFromString(String code, ShaderType type)
        {
            if (type == ShaderType.VertexShader)
            {
                VertexShaderID = loadShader(code, type);
            }
            if (type == ShaderType.FragmentShader)
            {
                FragmentShaderID = loadShader(code, type);
            }
            else if (type == ShaderType.GeometryShader)
            {
                GeometryShaderID = loadShader(code, type);
            }
            else if (type == ShaderType.TessControlShader)
            {
                TesselationShaderID = loadShader(code, type);
            }
        }

        private int loadShader(String code, ShaderType type)
        {
            int status_code;
            string info;

            int programID = GL.CreateShader(type);

            GL.ShaderSource(programID, code);

            GL.CompileShader(programID);

            GL.GetShaderInfoLog(programID, out info);

            GL.GetShader(programID, ShaderParameter.CompileStatus, out status_code);

            if (status_code != 1)
            {
                throw new ApplicationException(info);
            }

            GL.AttachShader(ShaderID, programID);

            return programID;
        }

        private void InitDefault()
        {
            VertexShaderID = -1;

            FragmentShaderID = -1;

            GeometryShaderID = -1;

            TesselationShaderID = -1;

            ShaderID = -1;
        }

        public AShader( string vshader, string fshader, bool fromFile=true)
        {
            InitDefault();

            ShaderID = GL.CreateProgram();

            if (fromFile)
            {
                LoadShaderFromFile(vshader, ShaderType.VertexShader);
                LoadShaderFromFile(fshader, ShaderType.FragmentShader);
            }
            else
            {
                LoadShaderFromString(vshader, ShaderType.VertexShader);
                LoadShaderFromString(fshader, ShaderType.FragmentShader);
            }
            Link();
            Validate();
            BindAttribytes();
        }

    }
}
