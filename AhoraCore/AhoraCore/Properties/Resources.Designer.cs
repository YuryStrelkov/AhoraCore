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
        ///   Ищет локализованную строку, похожую на layout(std140) uniform CameraData
        ///{
        ///	mat4 viewMatrix;
        ///	mat4 projectionMatrix;
        ///	mat4 tiltMatix;
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
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Clouds {
            get {
                object obj = ResourceManager.GetObject("Clouds", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Clouds1 {
            get {
                object obj = ResourceManager.GetObject("Clouds1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 430
        ///
        ///out vec4 out_Color;
        ///
        ///in vec3 v_Colour;
        ///
        ///in vec2 v_TexCoord;
        ///
        ///#include MaterialDefinition;
        ///
        ///uniform sampler2D defTexture;
        ///
        ///void main(void){
        ///
        ///	out_Color = vec4(v_Colour*texture(defTexture,v_TexCoord).xyz+albedoColor.xyz*getNormal(v_TexCoord),1);///vec4(v_Colour*texture(defTexture,v_TexCoord).xyz,1);///texture(modelTexture,pass_textureCoordinates);
        ///
        ///}.
        /// </summary>
        internal static string FSdefault {
            get {
                return ResourceManager.GetString("FSdefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap grass {
            get {
                object obj = ResourceManager.GetObject("grass", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 330
        ///
        ///layout(location = 0) out vec4 outColor;
        ///
        ///in vec2 uvCoord;
        ///
        ///in vec3 normal;
        ///
        ///const vec3 direction = vec3(0.333,0.333,0.333);
        ///
        ///const float intensity = 1.2;
        ///
        ///uniform sampler2D grassMap;
        ///
        ///float diffuse(vec3 dir, vec3 n, float i)
        ///{
        ///	return max(0.2, dot(n,dir) * i);
        ///}
        ///
        ///void main()
        ///{
        ///	vec4 color = texture(grassMap,uvCoord);
        ///	if(color.a &lt; 0.02)
        ///	{
        ///	discard;
        ///	}
        ///
        ///
        ///	float diffuse = diffuse(direction, normal, intensity);
        ///
        ///	
        ///	outColor = vec4(color.rgb*diffuse,color.a);
        ///
        ///}.
        /// </summary>
        internal static string GrassFS {
            get {
                return ResourceManager.GetString("GrassFS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 330
        ///
        ///layout(location = 0)in vec3 p_position;
        ///layout(location = 1)in vec2 p_uv;
        ///layout(location = 2)in vec3 p_normal;
        ///
        ///uniform sampler2D heightMap;
        ///
        ///uniform mat4 LocTransMatrix;
        ///
        ///uniform mat4 WorldTransMatrix;
        ///
        ///uniform mat4 projectionMatrix;
        ///
        ///uniform mat4 viewMatrix;
        ///
        ///uniform  float ScaleY;
        ///
        ///uniform  float ScaleXZ;
        ///
        ///uniform  int lod;
        ///
        ///uniform  float gap;
        ///
        ///out vec2 uvCoord;
        ///
        ///out vec3 normal;
        ///
        ///
        ///void main()
        ///{	
        ///
        ///	uvCoord = p_uv;
        ///	
        ///	normal = p_normal.xzy;
        ///
        ///	
        ///    vec4  [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string GrassVS {
            get {
                return ResourceManager.GetString("GrassVS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap hm0 {
            get {
                object obj = ResourceManager.GetObject("hm0", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap hm1 {
            get {
                object obj = ResourceManager.GetObject("hm1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap hm2 {
            get {
                object obj = ResourceManager.GetObject("hm2", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на ////Textures
        ///const int   DiffuseID = 0,
        ///	    	NormalID = 1,
        ///     		SpecularID = 2,
        ///			HeightID = 3,
        ///			ReflectGlossID = 4,
        ///			TransparencyID =5;
        ///
        ///struct  channel
        ///{
        ///vec2 tileUV;
        ///vec2 offsetUV;
        ///vec4 multRGBA;
        ///};
        ///
        ///layout(std140)  uniform MaterialData
        ///{
        ///vec4 albedoColor;
        ///
        ///vec4 ambientColor;
        ///
        ///vec4 reflectionColor;
        ///
        ///float reflectivity, metallness, roughness, transparency;
        ///
        ///channel[8] matChannels;
        ///};
        ///
        ///uniform sampler2D   diffuseMap;
        ///uniform sampler2D   normalMap;
        ///uniform sampler2D   s [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string MaterialDefinition {
            get {
                return ResourceManager.GetString("MaterialDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 430 core
        ///
        ///layout (local_size_x = 16, local_size_y = 16) in;
        ///
        ///layout (binding = 0, rgba32f) uniform writeonly image2D normalmap;
        ///
        ///uniform sampler2D displacementmap;
        ///uniform int N;
        ///uniform float normalStrength;
        ///
        ///void main(void)
        ///{
        ///	// z0 -- z1 -- z2
        ///	// |	 |     |
        ///	// z3 -- h  -- z4
        ///	// |     |     |
        ///	// z5 -- z6 -- z7
        ///	
        ///	ivec2 x = ivec2(gl_GlobalInvocationID.xy);
        ///	
        ///	vec2 texCoord = gl_GlobalInvocationID.xy/float(N);
        ///	
        ///	float texelSize = 1.0/N;
        ///	
        ///	float z0 = texture(displacemen [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string NormalMapRenderer {
            get {
                return ResourceManager.GetString("NormalMapRenderer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 330 core
        ///
        ///#include MaterialDefinition;
        ///
        ///out vec4 out_Color;
        ///
        ///in vec3 v_Colour;
        ///in vec2 v_TexCoord;
        ///in vec3 v_normal;
        ///in vec3 skyColor;
        ///
        ///
        ///void main()
        ///{ 
        ///	vec4 color          =  getDiffuse(v_TexCoord);
        ///	
        ///	out_Color.rgb       =  mix(1.75*color.rgb,skyColor,0.5);
        ///					     
        ///    out_Color.a         =  color.r;
        ///}.
        /// </summary>
        internal static string SkyDomeFS {
            get {
                return ResourceManager.GetString("SkyDomeFS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 330
        ///
        ///layout (location = 0) in vec3 p_position;
        ///layout (location = 1) in vec2 p_texcoord;
        ///layout (location = 2) in vec3 p_normal;
        ///
        ///
        ///#include CameraDefinition;
        ///
        ///#include TransformDefinition;
        ///
        ///
        ///out vec3 v_Colour;
        ///out vec2 v_TexCoord;
        ///out vec3 v_normal;
        ///out vec3 skyColor;
        ///
        ///uniform vec4 DomeColor;
        /// 
        ///vec3 atmosphereDescend(vec3 position, vec4 atm_color)
        ///{
        ///return vec3(-0.00022*(position.y-2000)+ atm_color.x,
        ///			-0.00025*(position.y-2000)+ atm_color.y,
        ///			-0.00019*(position.y-2000)+  [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string SkyDomeVS {
            get {
                return ResourceManager.GetString("SkyDomeVS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 430
        ///
        ///layout(location = 0) out vec4 outColor;
        ///
        ///in vec2 mapCoord_FS;
        ///
        ///uniform sampler2D normalMap;
        ///
        ///uniform sampler2D heightMap;
        ///
        ///const vec3 direction = vec3(0.333,0.333,0.333);
        ///
        ///const float intensity = 1.2;
        ///
        ///float diffuse(vec3 dir, vec3 n, float i)
        ///{
        ///	return max(0.01, dot(n,dir) * i);
        ///}
        ///
        ///void main()
        ///{
        ///	///vec3 normal = ;
        ///
        ///	float diffuse = diffuse(direction, texture(normalMap, mapCoord_FS).rgb, intensity);
        ///
        ///	outColor = vec4(vec3(0.1,1.0,0.1)*(1-texture(heightMap, mapCoord_FS) [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string TerrainFS {
            get {
                return ResourceManager.GetString("TerrainFS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 430
        ///
        ///layout(triangles) in;
        ///
        ///
        /// ///layout( line_strip, max_vertices = 4 )out;
        ///layout( triangle_strip, max_vertices = 3 )out;
        ///
        ///in vec2 mapCoord_GS[];
        ///
        ///out vec2 mapCoord_FS;
        ///
        ///uniform mat4 projectionMatrix;
        ///
        ///uniform mat4 viewMatrix;
        ///
        ///void main()
        ///{
        ///	vec4 pos;
        ///	
        ///	mat4   vpm = projectionMatrix*viewMatrix;
        ///	
        ///	for (int i=0;i&lt;gl_in.length();i++)
        ///	{
        ///		pos = gl_in[i].gl_Position;
        ///		
        ///		gl_Position = vpm *pos;
        ///		
        ///		mapCoord_FS=mapCoord_GS[i];
        ///		
        ///		EmitVertex();
        ///	}
        ///  	
        ///   /*pos = g [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string TerrainGS {
            get {
                return ResourceManager.GetString("TerrainGS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #terrain settings
        ///
        ///ScaleXZ 6000
        ///
        ///ScaleY 600
        ///
        ///HeigthMap false hm1
        ///
        ///TessellationFactor 600
        ///
        ///TessellationSlope 0,9
        ///
        ///TessellationShift 0,3
        ///
        ///TBNRange 300
        ///
        ///LodRanges 1750 874 386 192 100 50 0 0
        ///
        ///texture grassDiffuse gassDiff.jpg
        ///texture grassNormal gassNorm.jpg
        ///texture grassDisplacemnt gassDisp.jpg
        ///
        ///texture rockDiffuse rockDiff.jpg
        ///texture rockNormal rockNorm.jpg
        ///texture rockDisplacemnt rockDisp.jpg
        ///
        ///texture groundDiffuse groundDiff.jpg
        ///texture groundNormal groundNorm.jpg
        ///texture groun [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string TerrainSettings {
            get {
                return ResourceManager.GetString("TerrainSettings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 430
        ///
        ///layout(vertices =  16) out;
        ///
        ///in vec2 mapCoord_TC[];
        ///
        ///out vec2 mapCoord_TE[];
        ///
        ///const int AB=2;
        ///
        ///const int BC=3;
        ///
        ///const int CD=0;
        ///
        ///const int DA=1;
        ///
        ///
        ///uniform float tessellationFactor;
        ///
        ///uniform float tessellationSlope;
        ///
        ///uniform float tessellationShift;
        ///
        ///const int Max_Tess_level=16;
        ///
        ///uniform vec3 cameraPosition;
        ///
        ///float LodFactor(float dist)
        ///{
        ///	return  max( 0.0, tessellationFactor/pow(dist,tessellationSlope) + tessellationShift);
        ///}
        ///
        ///
        ///void main()
        ///{
        ///	if (gl_Invocatio [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string TerrainTC {
            get {
                return ResourceManager.GetString("TerrainTC", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 430
        ///
        ///layout(quads, fractional_odd_spacing, cw) in;
        ///
        ///in vec2 mapCoord_TE[];
        ///
        ///out vec2 mapCoord_GS;
        ///
        ///uniform sampler2D heightMap;
        ///
        ///uniform  float ScaleY;
        ///
        ///void main()
        ///{
        ///	float u = gl_TessCoord.x;
        ///	
        ///	float v = gl_TessCoord.y;
        ///	
        ///	vec4 position =((1-u) * (1-v) * gl_in[12].gl_Position+
        ///					    u * (1-v) * gl_in[0].gl_Position+
        ///					    u * v     * gl_in[3].gl_Position+
        ///					(1-u) * v     * gl_in[15].gl_Position);
        ///	
        ///	vec2 mapCoord=((1-u) * (1-v) * mapCoord_TE[12]+
        ///					  u * (1-v) [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string TerrainTE {
            get {
                return ResourceManager.GetString("TerrainTE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 430
        ///
        ///layout(location = 0)in vec2 p_position;
        ///
        ///out vec2 mapCoord_TC;
        ///
        ///uniform mat4 LocTransMatrix;
        ///
        ///uniform mat4 WorldTransMatrix;
        ///
        ///uniform  float ScaleY;
        ///
        ///uniform int lod;
        ///
        ///uniform  float gap;
        ///
        ///uniform vec2 location;
        ///
        ///uniform vec2 index;
        ///
        ///uniform int lod_morph_area[8];
        ///
        ///uniform vec3 cameraPosition;
        ///
        ///uniform sampler2D heightMap;
        ///
        ///float morphLatitude(vec2 position) {
        ///	
        ///	vec2 frac = position - location;
        ///	
        ///	if (index == vec2(0,0)){
        ///		float morph = frac.x - frac.y;
        ///		if ( [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string TerrainVS {
            get {
                return ResourceManager.GetString("TerrainVS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на layout(std140)TransformData
        ///{
        ///mat4 localTransform;
        ///mat4 worldTransform;
        ///}ж.
        /// </summary>
        internal static string TransformDefinition {
            get {
                return ResourceManager.GetString("TransformDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #version 430
        ///layout (location = 0) in vec3 p_position;
        ///layout (location = 1) in vec2 p_texcoord;
        ///layout (location = 2) in vec3 p_normal;
        ///
        ///layout(std140) uniform TransformData
        ///{
        ///	mat4 localTransform;
        ///	mat4 worldTransform;
        ///};
        ///
        ///layout(std140) uniform ShaderData
        ///{
        ///    mat4 projectionMatrix;
        ///	mat4 viewMatrix;
        ///};
        ///
        ///out vec3 v_Colour;
        ///out vec2 v_TexCoord;
        ///out vec3 v_normal;
        ///
        ///
        ///
        ///void main(){
        ///
        ///    mat4 viewTransform = viewMatrix * worldTransform * localTransform ;
        ///	
        ///	gl_Position = projection [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string VSdefault {
            get {
                return ResourceManager.GetString("VSdefault", resourceCulture);
            }
        }
    }
}
