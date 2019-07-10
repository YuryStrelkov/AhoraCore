using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.StandartBuffers;
using Assimp;
using Assimp.Configs;
using System.Collections.Generic;

namespace AhoraCore.Core.Models
{
    public static class ModelLoader
    {

        private delegate void VertDataSetter(Assimp.Mesh a_mesh, ref FloatBuffer buffer,int vIdx);

        private delegate void VertDataIndexSetter(Assimp.Mesh a_mesh, ref IntegerBuffer buffer);

        public static void LoadModel(string filename)
        {
            AssimpContext importer = new AssimpContext();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.6f));
            Scene m_model = importer.ImportFile(filename, PostProcessSteps.Triangulate |
                                                               PostProcessSteps.CalculateTangentSpace |
                                                               PostProcessSteps.FlipUVs |
                                                               PostProcessSteps.LimitBoneWeights);
            string[] splittedPath= filename.Split('/');

            splittedPath = splittedPath[splittedPath.Length-1].Split('.');

            AssipPostProcess(m_model, splittedPath[0]);
        }

        private static void AssipPostProcess(Scene a_scene, string sName)
        {
            ///прочитать иерархию нодов 

            ///Словарь < маска атрибутов, Список Мешей с такими же параметрами>

            Dictionary<int, FloatBuffer> AtrMaskPerMeshes = new Dictionary<int, FloatBuffer>();

            Dictionary<int, IntegerBuffer> AtrMaskPerIndeses = new Dictionary<int, IntegerBuffer>();

            //   Dictionary<string, Material> Materials = new Dictionary<string, Material>();

            int[] AttribsMask = new int[a_scene.Meshes.Count];

            int[] AttribsCount = new int[a_scene.Meshes.Count];

            int[] AttribsByteSize = new int[a_scene.Meshes.Count];

            int i =0;

            List<VertDataSetter> VDataSetters;

            foreach (Assimp.Mesh a_mesh in a_scene.Meshes)
            {
                GetAttributesMask(a_mesh,out  VDataSetters, out AttribsMask[i], out AttribsCount[i],out AttribsByteSize[i]);

                i += 1;
                
                if (AttribsMask[i] == 256)
                {
                    continue;
                }
                #region если уже есть mesh  с таким набором атрибутов, то : 
                if (AtrMaskPerMeshes.ContainsKey(AttribsMask[i]))
                {
                    for (int v=0; v < a_mesh.VertexCount; v++)
                    {
                        FloatBuffer b = AtrMaskPerMeshes[AttribsMask[i]];

                        for (int attribs=0; attribs< AttribsCount[i]; attribs++)
                        {
                            VDataSetters[attribs](a_mesh, ref b, v);
                        }

                        AtrMaskPerMeshes[AttribsMask[i]] = b;
                    }
                    for (int idx = 0; idx < a_mesh.FaceCount; idx++)
                    {
                        AtrMaskPerIndeses[AttribsMask[i]].Put(a_mesh.Faces[idx].Indices[0]);
                        AtrMaskPerIndeses[AttribsMask[i]].Put(a_mesh.Faces[idx].Indices[1]);
                        AtrMaskPerIndeses[AttribsMask[i]].Put(a_mesh.Faces[idx].Indices[2]);
                    }
                }
                #endregion
                else
                #region если нет mesh  с таким набором атрибутов, то : 
                {
                    AtrMaskPerMeshes.Add(AttribsMask[i], new FloatBuffer(AttribsByteSize[AttribsByteSize[i]] * a_mesh.Vertices.Count));

                    AtrMaskPerIndeses.Add(AttribsMask[i], new IntegerBuffer(3 * a_mesh.FaceCount));

                    AtrMaskPerMeshes[AttribsMask[i]].BufferID = sName+"_vertices_data_"+ i.ToString();

                    AtrMaskPerIndeses[AttribsMask[i]].BufferID = sName + "_indeces_data_" + i.ToString();

                    for (int v = 0; v < a_mesh.VertexCount; v++)
                    {
                        FloatBuffer b = AtrMaskPerMeshes[AttribsMask[i]];

                        for (int attribs = 0; attribs < AttribsCount[i]; attribs++)
                        {
                            VDataSetters[attribs](a_mesh, ref b, v);
                        }

                        AtrMaskPerMeshes[AttribsMask[i]] = b;
                    }
                    for (int idx = 0; idx < a_mesh.FaceCount; idx++)
                    {
                        AtrMaskPerIndeses[AttribsMask[i]].Put(a_mesh.Faces[idx].Indices[0]);
                        AtrMaskPerIndeses[AttribsMask[i]].Put(a_mesh.Faces[idx].Indices[1]);
                        AtrMaskPerIndeses[AttribsMask[i]].Put(a_mesh.Faces[idx].Indices[2]);
                    }
                }
                #endregion
            }

        }

        private static void GetAttributesMask(Assimp.Mesh a_mesh, out List<VertDataSetter> VDataSetters, out int attrMask, out int attrCount, out int attrByteCount)
        {
            VDataSetters = new List<VertDataSetter>();

            attrMask = 256;
            attrCount = 0;
            attrByteCount = 0;
           /// int offset = 0;

            if (a_mesh.HasVertices)
            {
                attrMask |= (byte)VericesAttribytes.V_POSITION;
                attrCount += 1;
                attrByteCount += 3;
                VDataSetters.Add(
                    (Assimp.Mesh m, ref FloatBuffer b, int vIdx) =>
                    {
                        b.Put(m.Vertices[vIdx].X);
                        b.Put(m.Vertices[vIdx].Y);
                        b.Put(m.Vertices[vIdx].Z);
                    }
                    );
            }

            if (a_mesh.HasTextureCoords(0))
            {
                attrMask |= (byte)VericesAttribytes.V_UVS;
                attrCount += 1;
                attrByteCount += 2;
                VDataSetters.Add(
                (Assimp.Mesh m, ref FloatBuffer b, int vIdx) =>
                {
                    b.Put(m.TextureCoordinateChannels[0][vIdx].X);
                    b.Put(m.TextureCoordinateChannels[0][vIdx].Y);
                }
                );
            }


            if (a_mesh.HasNormals)
            {
                attrMask |= (byte)VericesAttribytes.V_NORMAL;
                attrCount += 1;
                attrByteCount += 3;
                VDataSetters.Add(
                 (Assimp.Mesh m, ref FloatBuffer b, int vIdx) =>
                 {
                     b.Put(m.Normals[vIdx].X);
                     b.Put(m.Normals[vIdx].Y);
                     b.Put(m.Normals[vIdx].Z);
                 }
                 );

            }

            if (a_mesh.HasTangentBasis)
            {
                attrMask |= (byte)VericesAttribytes.V_TANGENT;
                attrMask |= (byte)VericesAttribytes.V_BITANGENT;
                attrCount += 2;
                attrByteCount += 6;
                VDataSetters.Add(
                (Assimp.Mesh m, ref FloatBuffer b, int vIdx) =>
                {
                    b.Put(m.Tangents[vIdx].X);
                    b.Put(m.Tangents[vIdx].Y);
                    b.Put(m.Tangents[vIdx].Z);
                }
                );
                VDataSetters.Add(
               (Assimp.Mesh m, ref FloatBuffer b, int vIdx) =>
               {
                   b.Put(m.BiTangents[vIdx].X);
                   b.Put(m.BiTangents[vIdx].Y);
                   b.Put(m.BiTangents[vIdx].Z);
               }
               );
            }
            if (a_mesh.HasVertexColors(0))
            {
                attrMask |= (byte)VericesAttribytes.V_COLOR_RGB;
                attrCount += 1;
                attrByteCount += 3;
                VDataSetters.Add(
                (Assimp.Mesh m, ref FloatBuffer b, int vIdx) =>
                {
                    b.Put(m.VertexColorChannels[0][vIdx].R);
                    b.Put(m.VertexColorChannels[0][vIdx].G);
                    b.Put(m.VertexColorChannels[0][vIdx].B);
                }
                );

            }
            if (a_mesh.HasBones)
            {
                attrMask |= (byte)VericesAttribytes.V_BONES;
                attrMask |= (byte)VericesAttribytes.V_BONES_WEIGHTS;
                VDataSetters.Add(
                 (Assimp.Mesh m, ref FloatBuffer b, int vIdx) =>
                 {
                     b.Put(m.Bones[vIdx].VertexWeights[0].VertexID);
                     b.Put(m.Bones[vIdx].VertexWeights[1].VertexID);
                     b.Put(m.Bones[vIdx].VertexWeights[2].VertexID);
                     b.Put(m.Bones[vIdx].VertexWeights[3].VertexID);
                 }
                 );
                VDataSetters.Add(
             (Assimp.Mesh m, ref FloatBuffer b, int vIdx) =>
             {
                 b.Put(m.Bones[vIdx].VertexWeights[0].Weight);
                 b.Put(m.Bones[vIdx].VertexWeights[1].Weight);
                 b.Put(m.Bones[vIdx].VertexWeights[2].Weight);
                 b.Put(m.Bones[vIdx].VertexWeights[3].Weight);
             }
             );
                attrCount += 2;
                attrByteCount += 8;
            }
        }



    }

}
