﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AhoraCore.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AhoraCore.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на layout(std140) uniform Camera
        ///{
        ///	mat4 ViewM;
        ///	mat4 ProjectionM;
        ///	vec3 PositionM;
        ///	vec3 LookAtV;
        ///	float FOV;
        ///	float Aspect;
        ///};.
        /// </summary>
        internal static string CameraDefinition {
            get {
                return ResourceManager.GetString("CameraDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap checkerboard_rainbow {
            get {
                object obj = ResourceManager.GetObject("checkerboard_rainbow", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 330
        ///
        ///in vec3 v_Colour;
        ///in vec2 v_TexCoord;
        ///
        ///
        ///out vec4 out_Color;
        ///
        ///#include MaterialDefinition;
        ///
        ///uniform sampler2D defTexture;
        ///
        ///void main(void){
        ///
        ///	out_Color = vec4(v_Colour*texture(defTexture,v_TexCoord).xyz,1);///vec4(v_Colour*texture(defTexture,v_TexCoord).xyz,1);///texture(modelTexture,pass_textureCoordinates);
        ///
        ///}.
        /// </summary>
        internal static string FSdefault {
            get {
                return ResourceManager.GetString("FSdefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на ////Textures
        ///uniform sampler2D   DiffuseMap;
        ///uniform sampler2D   NormalMap;
        ///uniform sampler2D   SpecularMap;
        ///uniform sampler2D   HeightMap;
        ///uniform sampler2D   ReflectGlossMap;
        ///uniform sampler2D   TransparencyMap;
        ///
        ///const int   DiffuseID = 0,
        ///	    	NormalID = 1,
        ///     		SpecularID = 2,
        ///			HeightID = 3,
        ///			ReflectGlossID = 4,
        ///			TransparencyID =5;
        ///
        ///struct  Channel
        ///{
        ///vec2 tileUV;
        ///vec2 offsetUV;
        ///vec4 multRGBA;
        ///};
        ///
        ///layout(std140) uniform MaterialData
        ///{
        ///vec4 AlbedoColor;
        ///
        ///vec4 AmbientColo [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string MaterialDefinition {
            get {
                return ResourceManager.GetString("MaterialDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 330
        ///
        ///layout (location = 0) in vec3 p_position;
        ///layout (location = 1) in vec2 p_texcoord;
        ///layout (location = 2) in vec3 p_normal;
        ///
        ///out vec3 v_Colour;
        ///out vec2 v_TexCoord;
        ///out vec3 v_normal;
        ///
        ///uniform mat4 transformationMatrix;
        ///uniform mat4 projectionMatrix;
        ///uniform mat4 viewMatrix;
        ///
        ///void main(){
        ///
        ///    mat4 viewTransform=viewMatrix * transformationMatrix ;
        ///	
        ///	gl_Position = projectionMatrix * viewTransform* vec4(p_position,1.0);
        ///
        ///	v_normal = (viewTransform*vec4(p_normal,1.0)).xyz;
        ///        /// [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string VSdefault {
            get {
                return ResourceManager.GetString("VSdefault", resourceCulture);
            }
        }
    }
}
