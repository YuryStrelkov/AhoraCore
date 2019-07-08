using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.Shaders
{
    public static class ShaderAttribytes
    {
        public static int V_POSITION = 0;
        public static int V_UVS = 1;
        public static int V_NORMAL = 2;
        public static int V_TANGENT = 3;
        public static int V_BITANGENT = 4;
        public static int V_BONES = 5;
        public static int V_BONES_WEIGHTS = 6;
        public static int V_COLOR_RGB = 7;
        public static int V_COLOR_RGBA = 8;
        public static int M_VIEW = 9;
        public static int M_PERSPECTIVE= 10;
        public static int M_MODEL = 11;
        public static int V_CAMERA_POSITION = 12;
        public static int CAMERA_ASPECT = 13;
        public static int CAMERA_FOV = 14;
        public static int V_CAMERA_LOOK_AT = 15;
        public static int MATERIAL = 16;
        public static int MATERIAL_BUFFER =17;
        public static int CAMERA_BUFFER = 18;
    }

    public static class Code
    {
        public static string UNIFORM = "uniform";
        public static string LAYLOCEQ = "layout (location = ";
        public static string OUT_V2 = "out vec2";
        public static string OUT_V3 = "out vec3";
        public static string OUT_V4 = "out vec4";
        public static string IN_V2 = "in vec2";
        public static string IN_V3 = "in vec3";
        public static string IN_V4 = "in vec4";
        public static string IN_MAT3 = "in mat3";
        public static string IN_MAT4 = "in mat4";
        public static string OUT_MAT3 = "out mat3";
        public static string OUT_MAT4 = "out mat4";

        public static string OUT_FLOAT = "out float";
        public static string IN_FLOAT = "in float";
        public static string FLOAT = "float";

        public static string V2 = "vec2";
        public static string V3 = "vec3";
        public static string V4 = "vec4";
        public static string MAT3 = "mat3";
        public static string MAT4 = "mat4";

        public static string EOL = ";\n";
        /*VERTEX_LAYOUTS_IN_OUT*/
        public static string V_IN_P = "V_IN_POSITION";
        public static string V_OUT_P = "V_OUT_POSITION";

        public static string V_IN_UVS = "VIN_UVS";
        public static string V_OUT_UVS = "V_OUT_UVS";

        public static string V_IN_N = "V_IN_NORMAL";
        public static string V_OUT_N = "V_OUT_NORMAL";

        public static string V_IN_T = "V_IN_TANGENT";
        public static string V_OUT_T = "V_OUT_TANGENT";

        public static string V_IN_BT = "V_IN_BITANGENT";
        public static string V_OUT_BT = "V_OUT_BITANGENT";

        public static string V_IN_B = "V_IN_BONES";
        public static string V_OUT_B = "V_OUT_BONES";

        public static string V_IN_B_W = "V_IN_BONES_WEIGHTS";
        public static string V_OUT_B_W = "V_OUT_BONES_WEIGHTS";

        public static string V_IN_RGB = "V_IN_COLOR_RGB";
        public static string V_OUT_RGB = "V_OUT_COLOR_RGB";

        public static string V_IN_RGBA = "V_IN_COLOR_RGBA";
        public static string V_OUT_RGBA = "V_OUT_COLOR_RGBA";

        /*VERTEX_CAMERA_IN_OUT*/

        public static string V_IN_CAM_PRJ = "M_IN_PERSPECTIVE";
        public static string V_OUT_CAM_PRJ = "M_OUT_PERSPECTIV;";

        public static string V_IN_CAM_VIEW = "M_IN_VIEW";
        public static string V_OUT_CAM_VIEW = "M_OUT_VIEW";

        public static string V_IN_CAM_POS = "V_IN_CAMERA_POSITION";
        public static string V_OUT_CAM_POS = "V_OUT_CAMERA_POSITION";

        public static string V_IN_CAM_A= "IN_CAMERA_ASPECT";
        public static string V_OUT_CAM_A= "OUT_CAMERA_ASPECT";

        public static string V_IN_CAM_FOV = "IN_CAMERA_FOV";
        public static string V_OUT_CAM_FOV = "OUT_CAMERA_FOV";

        public static string V_IN_CAM_L_AT = "VIN_CAMERA_LOOK_AT";
        public static string V_OUT_CAM_L_AT = "VOUT_CAMERA_LOOK_AT";

        public static string V_M_IN_M = "M_IN_MODEL";
        public static string V_M_OUT_M = "M_OUT_MODEL";

        public static string IN_MATERIAL = "IN_MATERIAL";
        public static string MATERIAL_BUFFER = "layout(std140) uniform MaterialData\n{\n";
        public static string CAMERA_BUFFER = "layout(std140) uniform Camera\n{\n";
        /*FRAGMENT_IN_OUT*/
    }

    public class ShaderBuilder
    {
        public void createShaderCode(int attribytes, string version)
        {
            string VertexCode = "#version " + version + " core\n";
            //*Инициализация входа - выхода вертексного буфера*//
             VertexCode += InitInOutVSvars(attribytes);
             VertexCode += InitCameraVSvars(attribytes);

            if ((attribytes & (byte)ShaderAttribytes.M_MODEL) == ShaderAttribytes.M_MODEL)
            {
                VertexCode = VertexCode + Code.UNIFORM + Code.MAT4 + Code.V_M_IN_M + Code.EOL;
            }

        }
        private string InitCameraVSvars(int attribytes)
        {
            String VertexCode = "";
            if ((attribytes & (byte)ShaderAttribytes.CAMERA_BUFFER) == ShaderAttribytes.CAMERA_BUFFER)
            {
                VertexCode += Code.CAMERA_BUFFER;
                
                if ((attribytes & (byte)ShaderAttribytes.M_PERSPECTIVE) == ShaderAttribytes.M_PERSPECTIVE)
                {
                    VertexCode = VertexCode + Code.MAT4 + Code.V_IN_CAM_PRJ + Code.EOL;/// "mat4 camProjection;\n";
                }
                else if ((attribytes & (byte)ShaderAttribytes.M_VIEW) == ShaderAttribytes.M_VIEW)
                {
                    VertexCode = VertexCode + Code.MAT4 + Code.V_IN_CAM_VIEW + Code.EOL; //"mat4 camView;\n";
                }
                else if ((attribytes & (byte)ShaderAttribytes.CAMERA_ASPECT) == ShaderAttribytes.CAMERA_ASPECT)
                {
                    VertexCode = VertexCode + Code.FLOAT + Code.V_IN_CAM_A + Code.EOL;/// "float aspect;\n";
                }
                else if ((attribytes & (byte)ShaderAttribytes.V_CAMERA_POSITION) == ShaderAttribytes.V_CAMERA_POSITION)
                {
                    VertexCode = VertexCode + Code.V3 + Code.V_IN_CAM_POS + Code.EOL;///"vec3 camPosition;\n";
                }
                else if ((attribytes & (byte)ShaderAttribytes.V_CAMERA_LOOK_AT) == ShaderAttribytes.V_CAMERA_LOOK_AT)
                {
                    VertexCode = VertexCode + Code.V3 + Code.V_IN_CAM_L_AT + Code.EOL;///"vec3 camLookAt;\n";
                }
                else if ((attribytes & (byte)ShaderAttribytes.CAMERA_FOV) == ShaderAttribytes.CAMERA_FOV)
                {
                    VertexCode = VertexCode + Code.FLOAT + Code.V_IN_CAM_FOV + Code.EOL;///"float FOV;\n";
                }
                VertexCode += "};\n";
            }
            else
            {

                if ((attribytes & (byte)ShaderAttribytes.M_PERSPECTIVE) == ShaderAttribytes.M_PERSPECTIVE)
                {
                    VertexCode = VertexCode + Code.UNIFORM + Code.MAT4 + Code.V_IN_CAM_PRJ + Code.EOL;/// "mat4 camProjection;\n";
                }
                else if ((attribytes & (byte)ShaderAttribytes.M_VIEW) == ShaderAttribytes.M_VIEW)
                {
                    VertexCode = VertexCode + Code.UNIFORM + Code.MAT4 + Code.V_IN_CAM_VIEW + Code.EOL; //"mat4 camView;\n";
                }
                else if ((attribytes & (byte)ShaderAttribytes.CAMERA_ASPECT) == ShaderAttribytes.CAMERA_ASPECT)
                {
                    VertexCode = VertexCode + Code.UNIFORM + Code.FLOAT + Code.V_IN_CAM_A + Code.EOL;/// "float aspect;\n";
                }
                else if ((attribytes & (byte)ShaderAttribytes.V_CAMERA_POSITION) == ShaderAttribytes.V_CAMERA_POSITION)
                {
                    VertexCode = VertexCode + Code.UNIFORM + Code.V3 + Code.V_IN_CAM_POS + Code.EOL;///"vec3 camPosition;\n";
                }
                else if ((attribytes & (byte)ShaderAttribytes.V_CAMERA_LOOK_AT) == ShaderAttribytes.V_CAMERA_LOOK_AT)
                {
                    VertexCode = VertexCode + Code.UNIFORM + Code.V3 + Code.V_IN_CAM_L_AT + Code.EOL;///"vec3 camLookAt;\n";
                }
                else if ((attribytes & (byte)ShaderAttribytes.CAMERA_FOV) == ShaderAttribytes.CAMERA_FOV)
                {
                    VertexCode = VertexCode + Code.UNIFORM + Code.FLOAT + Code.V_IN_CAM_FOV + Code.EOL;///"float FOV;\n";
                }

            }
            return VertexCode;
        }

        private string InitInOutVSvars(int attribytes)
        {
            String VertexCode = "", VertexOut = "";

            int nAttribs = 0;
              
            if ((attribytes & ShaderAttribytes.V_POSITION) == ShaderAttribytes.V_POSITION)
            {
                VertexCode = VertexCode + Code.LAYLOCEQ + nAttribs + ")"+Code.IN_V4+Code.V_IN_P+Code.EOL;
                VertexOut = VertexOut + Code.OUT_V4 + Code.V_OUT_P + Code.EOL;
                nAttribs += 1;
            }
            else if ((attribytes & (byte)ShaderAttribytes.V_UVS) == ShaderAttribytes.V_UVS)
            {
                VertexCode = VertexCode + Code.LAYLOCEQ + nAttribs + ")" + Code.IN_V2 + Code.V_IN_UVS + Code.EOL;
                VertexOut = VertexOut + Code.OUT_V2 + Code.V_OUT_UVS + Code.EOL;
                nAttribs += 1;
            }
            else if ((attribytes & (byte)ShaderAttribytes.V_NORMAL) == ShaderAttribytes.V_NORMAL)
            {
                VertexCode = VertexCode + Code.LAYLOCEQ + nAttribs + ")"+ Code.IN_V3 + Code.V_IN_N + Code.EOL;
                VertexOut = VertexOut + Code.OUT_V3 + Code.V_OUT_N + Code.EOL;
                nAttribs += 1;
            }
            else if ((attribytes & (byte)ShaderAttribytes.V_TANGENT) == ShaderAttribytes.V_TANGENT)
            {
                VertexCode = VertexCode + Code.LAYLOCEQ + nAttribs + ")" + Code.IN_V3 + Code.V_IN_T + Code.EOL;
                VertexOut = VertexOut + Code.OUT_V3 + Code.V_OUT_T + Code.EOL;
                nAttribs += 1;
            }
            else if ((attribytes & (byte)ShaderAttribytes.V_BITANGENT) == ShaderAttribytes.V_BITANGENT)
            {
                VertexCode = VertexCode + Code.LAYLOCEQ + nAttribs + ")" + Code.IN_V3 + Code.V_IN_BT + Code.EOL;
                VertexOut = VertexOut + Code.OUT_V3 + Code.V_OUT_BT + Code.EOL;
                nAttribs += 1;
            }
            else if ((attribytes & (byte)ShaderAttribytes.V_BONES) == ShaderAttribytes.V_BONES)
            {
                VertexCode = VertexCode + Code.LAYLOCEQ + nAttribs + ")" + Code.IN_V4 + Code.V_IN_B + Code.EOL;
                VertexOut = VertexOut + Code.OUT_V4 + Code.V_OUT_B + Code.EOL;
                nAttribs += 1;
            }
            else if ((attribytes & (byte)ShaderAttribytes.V_BONES_WEIGHTS) == ShaderAttribytes.V_BONES_WEIGHTS)
            {
                VertexCode = VertexCode + Code.LAYLOCEQ + nAttribs + ")" + Code.IN_V4 + Code.V_IN_B_W + Code.EOL;
                VertexOut = VertexOut + Code.OUT_V4 + Code.V_OUT_B_W + Code.EOL;
                nAttribs += 1;
            }
            else if ((attribytes & (byte)ShaderAttribytes.V_COLOR_RGB) == ShaderAttribytes.V_COLOR_RGB)
            {
                VertexCode = VertexCode + Code.LAYLOCEQ + nAttribs + ")" + Code.IN_V3 + Code.V_IN_RGB + Code.EOL;
                VertexOut = VertexOut + Code.OUT_V3 + Code.V_OUT_RGB + Code.EOL;
                nAttribs += 1;
            }
            else if ((attribytes & (byte)ShaderAttribytes.V_COLOR_RGBA) == ShaderAttribytes.V_COLOR_RGBA)
            {
                VertexCode = VertexCode + Code.LAYLOCEQ + nAttribs + ")" + Code.IN_V4 + Code.V_IN_RGBA + Code.EOL;
                VertexOut = VertexOut + Code.OUT_V4 + Code.V_OUT_RGBA + Code.EOL;
                nAttribs += 1;
            }

            VertexCode += VertexOut;

            return VertexCode;
        }

        //private string InitMainFuncVS(int attribytes)
        //{
        //    string mainCode = "";

        //    if ((attribytes & (byte)ShaderAttribytes.M_VIEW) == ShaderAttribytes.M_VIEW)
        //    {
        //        mainCode = mainCode + Code.V_OUT_CAM_VIEW + "=mat3(inverse(" + Code.V_IN_CAM_VIEW + ")" + Code.EOL; //"mat4 camView;\n";
        //    }
            
        //}
    }
}
