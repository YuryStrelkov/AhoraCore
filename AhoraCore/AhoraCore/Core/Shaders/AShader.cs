﻿using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.UniformsBuffer;
using AhoraCore.Core.CES;
using AhoraCore.Core.CES.ICES;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AhoraCore.Core.Shaders
{

    public abstract class AShader: ABindableObject<ShaderType>
    {
        public string ShaderName { get; private set; }
        
        private Dictionary<ShaderType, int> shaderPrograms;

        private Dictionary<string, int> uniforms;

 ///       protected UniformsBuffer<string> UniformBuffer;// { protected get; private set; }

        public bool IsBuffered { get; private set; }

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

        protected void AddUniform(string uniform)
        {
            int uniformLocation = GL.GetUniformLocation(ShaderID, uniform);

            if (uniformLocation == -1)
            {
                Console.WriteLine("ShderID "+ ShaderID + " Error: Could not find uniform: " + uniform);
            }

            uniforms.Add(uniform, uniformLocation);
        }

/*
        protected void EnableBuffering()
        {
            IsBuffered = true;
            UniformBuffer = new UniformsBuffer<string>();
        }
*/
  /*      protected void MarkBuffer(string[] itemName, int[] itemLength)
        {

            /////
            ////Добавлять элементы буфера строго в порядке следования ихв шедере    
            /////
            if (!IsBuffered)
            {
                Console.WriteLine("Buffering is off ...");
                return;
            }
            for (int i=0;i< itemName.Length;i++)
            {
                UniformBuffer.addBufferItem(itemName[i], itemLength[i]);
            }
        }
        */
        /*
        protected void ConfirmBuffer()
        {
            if (!IsBuffered)
            {
                Console.WriteLine("Buffering is off ...");
                return;
            }
            UniformBuffer.Create(1);
           // GL.UseProgram(ShaderID);
            UniformBuffer.LinkBufferToShder(this, "ShaderData",0);
            //GL.UseProgram(0);
            uniforms.Add("ShaderData", UniformBuffer.Uniform_block_index);
        }
        */
        /*protected void AddUniformBuffred(string uniform, int size)
        {
            if (!IsBuffered)
            {
                Console.WriteLine("Buffering is off ...");
                return;
            }
            UniformBuffer.addBufferItem(uniform, size);
        }
        */
        protected void AddUniformBlock(string uniform)
        {
            int uniformLocation = GL.GetUniformBlockIndex(ShaderID, uniform);
           
            if (uniformLocation == -1)
            {
                Console.WriteLine("ShderID " + ShaderID + " Error: Could not find uniform: " + uniform);
            }
            uniforms.Add(uniform, uniformLocation);
        }

        protected void AddAttribyte(string uniform)
        {
            int uniformLocation = GL.GetAttribLocation(ShaderID, uniform);
            if (uniformLocation == 0xFFFFFFFF)
            {
                Console.WriteLine("ShderID " + ShaderID + " Error: Could not find uniform: " + uniform);
            }

            uniforms.Add(uniform, uniformLocation);
            BindAttributeLocation(uniformLocation, uniform);
        }

        public override void Bind()
        {
            GL.UseProgram(ShaderID);
            /*
            if (IsBuffered)
            {
               UniformBuffer.Bind(this);
            }*/
            UpdateUniforms();
        }

        public override void Bind(ShaderType target)
        {
            GL.UseProgram(ShaderID);
            UpdateUniforms();
        }

        public override void Clear()
        {
             
        }

        public override void Unbind()
        {
            GL.UseProgram(0);
        }

        protected  void Link()
        {
            GL.LinkProgram(ShaderID);
            Console.WriteLine(GL.GetProgramInfoLog(ShaderID));
        }

        protected void Validate()
        {
            GL.ValidateProgram(ShaderID);
            Console.WriteLine(GL.GetProgramInfoLog(ShaderID));
        }

        public override void Delete()
        {
            Unbind();

            foreach (ShaderType k in shaderPrograms.Keys)
            {
                GL.DetachShader(ShaderID, shaderPrograms[k]);
                GL.DeleteShader(shaderPrograms[k]);
            }
            GL.DeleteProgram(ShaderID);
        }

        public override void Create()
        {
            ShaderID = GL.CreateProgram();

            shaderPrograms = new Dictionary<ShaderType, int>();

            uniforms = new Dictionary<string, int>();
        }

        public abstract void UpdateUniforms();

        public abstract void UpdateUniforms(IGameEntity e);

        protected abstract void BindUniforms();

        protected abstract void BindAttributes();

        private void BindAttributeLocation(int attrIndex,string attrName)
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
            Regex includes = new Regex(@"\w*#include CameraDefinition;\w*");

            code = includes.Replace(code,Properties.Resources.CameraDefinition);

            includes = new Regex(@"\w*#include MaterialDefinition;\w*");

            code = includes.Replace(code, Properties.Resources.MaterialDefinition);


            includes = new Regex(@"\w*#include TransformDefinition;\w*");

            code = includes.Replace(code, Properties.Resources.TransformDefinition);
            Console.Clear();

            Console.Write(code);

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


        public AShader()
        {
            Create();
        }


        public AShader( string vshader, string fshader, bool fromFile=true)
        {

            Create();

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
            BindAttributes();
            BindUniforms();
    
        }

    }
}
