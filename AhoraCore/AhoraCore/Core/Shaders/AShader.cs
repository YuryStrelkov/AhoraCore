﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;


namespace AhoraCore.Core.Shaders
{

    public abstract class AShader
    {
        private Dictionary<ShaderType, int> shaderPrograms;

        private Dictionary<string, int> uniforms;

        public int ShaderID   { get;private set; }

        public int VertexShaderID
        {
            get {
                return shaderPrograms.ContainsKey(ShaderType.VertexShader) ? shaderPrograms[ShaderType.VertexShader] : -1;
                }
            private set
                {
                shaderPrograms[ShaderType.VertexShader] = value;
            }
        }

        public int FragmentShaderID
        {
            get
            {
                return shaderPrograms.ContainsKey(ShaderType.FragmentShader) ? shaderPrograms[ShaderType.FragmentShader] : -1;
            }
            private set
            {
                shaderPrograms[ShaderType.VertexShader] = value;
            }
        }

        public int GeometryShaderID
        {
            get
            {
                return shaderPrograms.ContainsKey(ShaderType.GeometryShader) ? shaderPrograms[ShaderType.GeometryShader] : -1;
            }
            private set
            {
                shaderPrograms[ShaderType.VertexShader] = value;
            }
        }

        public int TessControlShaderID
        {
            get
            {
                return shaderPrograms.ContainsKey(ShaderType.TessControlShader) ? shaderPrograms[ShaderType.TessControlShader] : -1;
            }
            private set
            {
                shaderPrograms[ShaderType.VertexShader] = value;
            }
        }

        public int TessEvaluationShaderID
        {
            get
            {
                return shaderPrograms.ContainsKey(ShaderType.TessEvaluationShader) ? shaderPrograms[ShaderType.TessEvaluationShader] : -1;
            }
            private set
            {
                shaderPrograms[ShaderType.VertexShader] = value;
            }
        }

        public int ComputeShaderID
        {
            get
            {
                return shaderPrograms.ContainsKey(ShaderType.ComputeShader) ? shaderPrograms[ShaderType.ComputeShader] : -1;
            }
            private set
            {
                shaderPrograms[ShaderType.VertexShader] = value;
            }
        }

        public void AddUniform(string uniform)
        {
            int uniformLocation = GL.GetUniformLocation(ShaderID, uniform);

            if (uniformLocation == 0xFFFFFFFF)
            {
                Console.WriteLine("ShderID "+ ShaderID + " Error: Could not find uniform: " + uniform);
            }

            uniforms.Add(uniform, uniformLocation);
        }

        public void AddUniformBlock(string uniform)
        {
            int uniformLocation = GL.GetUniformBlockIndex(ShaderID, uniform);
            if (uniformLocation == 0xFFFFFFFF)
            {
                Console.WriteLine("ShderID " + ShaderID + " Error: Could not find uniform: " + uniform);
            }

            uniforms.Add(uniform, uniformLocation);
        }

        public void Bind()
        {
            GL.UseProgram(ShaderID);
            UpdateUniforms();
        }

        public void Unbind()
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
            Unbind();

            foreach (ShaderType k in shaderPrograms.Keys)
            {
                GL.DetachShader(ShaderID, shaderPrograms[k]);
                GL.DeleteShader(shaderPrograms[k]);
            }
            GL.DeleteProgram(ShaderID);
        }

        public abstract void BindAttribytes();

        public abstract void UpdateUniforms();

        public void BindAttributeLocation(int attrIndex,string attrName)
        {
            GL.BindAttribLocation(ShaderID, attrIndex, attrName);
        }

        public void LoadShaderFromFile(string path2shaderCode, ShaderType type)
        {
            try
            {
                StreamReader sr = new StreamReader(Path.GetFullPath(path2shaderCode));

                shaderPrograms.Add(type, LoadShader(sr.ReadToEnd(), type));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void LoadShaderFromstring(string code, ShaderType type)
        {
            shaderPrograms.Add(type, LoadShader(code, type));
        }

        private int LoadShader(string code, ShaderType type)
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
        
        public void SetUniformi(string uniformName, int value)
        {
            GL.Uniform1(uniforms[uniformName], value);
        }

        public void SetUniformf(string uniformName, float value)
        {
            GL.Uniform1(uniforms[uniformName], value);
        }

        public void SetUniform(string uniformName, Vector2 value)
        {
            GL.Uniform2(uniforms[uniformName], value.X, value.Y);
        }

        public void SetUniform(string uniformName, Vector3 value)
        {
            GL.Uniform3(uniforms[uniformName], value.X, value.Y, value.Z);
        }

        public void SetUniform(string uniformName, Vector4 value)
        {
            GL.Uniform4(uniforms[uniformName], value.X, value.Y, value.Z, value.W);
        }

        public void SetUniform(string uniformName, Matrix4 value)
        {
           GL.UniformMatrix4(uniforms[uniformName], true, ref value);
        }

        public void BndUniformBlock(string uniformBlockName, int uniformBlockBinding)
        {
            GL.UniformBlockBinding(ShaderID, uniforms[uniformBlockName], uniformBlockBinding);
        }

        public AShader( string vshader, string fshader, bool fromFile=true)
        {

            ShaderID = GL.CreateProgram();

            shaderPrograms = new Dictionary<ShaderType, int>();

            if (fromFile)
            {
                LoadShaderFromFile(vshader, ShaderType.VertexShader);
                LoadShaderFromFile(fshader, ShaderType.FragmentShader);
            }
            else
            {
                LoadShaderFromstring(vshader, ShaderType.VertexShader);
                LoadShaderFromstring(fshader, ShaderType.FragmentShader);
            }

           
            Link();
            Validate();
            BindAttribytes();
        }

    }
}
