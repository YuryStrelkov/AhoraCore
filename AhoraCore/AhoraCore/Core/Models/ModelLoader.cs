using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Transformations;
using Assimp;
using Assimp.Configs;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AhoraCore.Core.Models
{
    public static class ModelLoader
    {

        private delegate void VertDataSetter(Assimp.Mesh a_mesh, ref FloatBuffer buffer,int vIdx);

        private static int[]  ReadMeshIndeces(Assimp.Mesh m)
        {
            int[] indeces = new int[m.FaceCount * 3];
            int ind = 0;
            for (int i = 0; i < m.FaceCount; i++)
            {
                indeces[ind] = m.Faces[i].Indices[1];
                ind++;
                indeces[ind] = m.Faces[i].Indices[0];
                ind++;
                indeces[ind] = m.Faces[i].Indices[2];
                ind++;
            }
            return indeces;
       }

        private static float[] ReadMeshVertices(out int attrMask, Assimp.Mesh m)
        {
            int attrCount, attrByteCount;

            GetAttributesMask(m, out attrMask, out attrCount, out attrByteCount);

            float[] vertices = new float[m.VertexCount * attrByteCount];
            
            int ind = 0;

            for (int i = 0; i < m.VertexCount; i++)
            {
                if (m.HasVertices)
                {
                    vertices[ind] = m.Vertices[i].X;
                    ind++;
                    vertices[ind] = m.Vertices[i].Y;
                    ind++;
                    vertices[ind] = m.Vertices[i].Z;
                    ind++;
                }

                if (m.HasTextureCoords(0))
                {
                    vertices[ind] = m.TextureCoordinateChannels[0][i].X;
                    ind++;
                    vertices[ind] = m.TextureCoordinateChannels[0][i].Y;
                    ind++;
                }


                if (m.HasNormals)
                {
                    vertices[ind] = m.Normals[i].X;
                    ind++;
                    vertices[ind] = m.Normals[i].Y;
                    ind++;
                    vertices[ind] = m.Normals[i].Z;
                    ind++;
                }

                if (m.HasTangentBasis)
                {
                    vertices[ind] = m.Tangents[i].X;
                    ind++;
                    vertices[ind] = m.Tangents[i].Y;
                    ind++;
                    vertices[ind] = m.Tangents[i].Z;
                    ind++;

                    vertices[ind] = m.BiTangents[i].X;
                    ind++;
                    vertices[ind] = m.BiTangents[i].Y;
                    ind++;
                    vertices[ind] = m.BiTangents[i].Z;
                    ind++;
                }

                if (m.HasVertexColors(0))
                {
                    vertices[ind] = m.VertexColorChannels[0][i].R;
                    ind++;
                    vertices[ind] = m.VertexColorChannels[0][i].G;
                    ind++;
                    vertices[ind] = m.VertexColorChannels[0][i].B;
                    ind++;
                }
                if (m.HasBones)
                {
                    vertices[ind] = 0;
                    ind++;
                    vertices[ind] = 0;
                    ind++;
                    vertices[ind] = 0;
                    ind++;
                    vertices[ind] = 0;
                    ind++;
                }
                
            }

            ///TODO
           /* if (m.HasBones)
            {
               List<float>[] wiegths = new List<float>[m.VertexCount];

                for (int i = 0; i < m.Bones.Count; i++)
                {
                    for (int k=0;k < m.Bones[i].VertexWeights.Count; k++)
                    {
                        wiegths[m.Bones[i].VertexWeights[k].VertexID].Add(m.Bones[i].VertexWeights[k].Weight);
                    }
                }

            }
*/
                return vertices;
        }

        private static void GetAttributesMask(Assimp.Mesh a_mesh, out int attrMask, out int attrCount, out int attrByteCount)
        {
            attrMask = 256;
            attrCount = 0;
            attrByteCount = 0;
            /// int offset = 0;

            if (a_mesh.HasVertices)
            {
                attrMask |= (byte)VericesAttribytes.V_POSITION;
                attrCount += 1;
                attrByteCount += 3;
            }

            if (a_mesh.HasTextureCoords(0))
            {
                attrMask |= (byte)VericesAttribytes.V_UVS;
                attrCount += 1;
                attrByteCount += 2;
            }


            if (a_mesh.HasNormals)
            {
                attrMask |= (byte)VericesAttribytes.V_NORMAL;
                attrCount += 1;
                attrByteCount += 3;
            }

            if (a_mesh.HasTangentBasis)
            {
                attrMask |= (byte)VericesAttribytes.V_TANGENT;
                attrMask |= (byte)VericesAttribytes.V_BITANGENT;
                attrCount += 2;
                attrByteCount += 6;
            }
            if (a_mesh.HasVertexColors(0))
            {
                attrMask |= (byte)VericesAttribytes.V_COLOR_RGB;
                attrCount += 1;
                attrByteCount += 3;
            }
            if (a_mesh.HasBones)
            {
                attrMask |= (byte)VericesAttribytes.V_BONES;
                attrMask |= (byte)VericesAttribytes.V_BONES_WEIGHTS;
                attrCount += 2;
                attrByteCount += 8;
            }
        }

        static void LoadAllMeshes(Scene scn, out Dictionary<int, List<string>> names, out Dictionary<int, List<float[]>> vertices, out Dictionary<int, List<int[]>> indeces)
        {

            names = new Dictionary<int, List<string>>();

            indeces = new Dictionary<int, List<int[]>>();

            vertices = new Dictionary<int, List<float[]>>();

            int[] tmp_i;

            float[] tmp_v;

            int mask = -1;

            foreach (Mesh m in scn.Meshes)
            {
                tmp_v = ReadMeshVertices(out mask, m);

                tmp_i = ReadMeshIndeces(m);

                if (names.ContainsKey(mask))
                {
                    indeces[mask].Add(tmp_i);
                    vertices[mask].Add(tmp_v);
                    names[mask].Add(m.Name);

                    continue;
                }

                indeces.Add(mask, new List<int[]>());
                vertices.Add(mask, new List<float[]>());
                names.Add(mask, new List<string>());

                names[mask].Add(m.Name);
                indeces[mask].Add(tmp_i);
                vertices[mask].Add(tmp_v);
            }
        }

        public static void LoadSceneModels(string fileName)
        {
            AssimpContext importer = new AssimpContext();

            importer.SetConfig(new NormalSmoothingAngleConfig(66.666f));
            Scene scn = null;
            try
            { 
                  scn = importer.ImportFile(fileName, PostProcessSteps.Triangulate |
                                                          PostProcessSteps.CalculateTangentSpace |
                                                          PostProcessSteps.FlipUVs |
                                                          PostProcessSteps.LimitBoneWeights);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Unnable to open or found file : " + fileName);
            }

            Dictionary<int, List<string>>  MasksPerModelIDS = null;
            Dictionary<int, List<float[]>> Vertices;
            Dictionary<int, List<int[]>> Indeces;

            if (scn!=null)
            {
                LoadAllMeshes(scn, out MasksPerModelIDS, out Vertices, out Indeces);

                foreach (int attrMask in MasksPerModelIDS.Keys)
                {
                    for(int i = 0; i < MasksPerModelIDS[attrMask].Count; i++)
                    {
                        GeometryStorageManager.Data.AddGeometry(attrMask, MasksPerModelIDS[attrMask][i], Vertices[attrMask][i], Indeces[attrMask][i]);
                    }
                }

            }
           
        }


        //////////obsolete or not in use
        //////////
        //////////
        //////////

        public static void LoadModel(string filename, out int[] AttribsMasks, out FloatBuffer[] Models, out IntegerBuffer[] ModelsIndeces)
        {

            AttribsMasks = null; Models = null; ModelsIndeces = null;

            AssimpContext importer = new AssimpContext();

            importer.SetConfig(new NormalSmoothingAngleConfig(66.666f));

            try
            {
                Scene scn = importer.ImportFile(filename, PostProcessSteps.Triangulate |
                                                          PostProcessSteps.CalculateTangentSpace |
                                                          PostProcessSteps.FlipUVs |
                                                          PostProcessSteps.LimitBoneWeights);
             //   string[] splittedPath = filename.Split('/');

               /// splittedPath = splittedPath[splittedPath.Length - 1].Split('.');



          
                ///***///
                //Dictionary<int, List<string>> MasksPerModelIDS;
                //Dictionary<int, List<FloatBuffer>> Vertices;
                //Dictionary<int, List<IntegerBuffer>> Indeces;
                //Dictionary<int, List<int>> MaterialIDs;
                /////***///
                //int modelsNumber = 0;
                /////***///
                //LoadSceneGeometry(ref scn, out MasksPerModelIDS,
                //                           out Vertices,
                //                           out Indeces,
                //                           out MaterialIDs);


                //foreach (int key in MasksPerModelIDS.Keys)
                //{
                //    modelsNumber += MasksPerModelIDS[key].Count;
                //}

                //AttribsMasks = new int[modelsNumber];
                //Models = new FloatBuffer[modelsNumber];
                //ModelsIndeces = new IntegerBuffer[modelsNumber];

                //int idx = 0;
                //foreach (int key in MasksPerModelIDS.Keys)
                //{
                //    for(int i=0; i< Vertices[key].Count; i++ )
                //    {
                //        AttribsMasks[idx] = key;
                //        Models[idx] = Vertices[key][i];
                //        ModelsIndeces[idx] = Indeces[key][i];
                //        idx += 1;
                //    }
                //}

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Unnable to open or found file : "+ filename);
            }

        
        }
        

        public static Scene LoadScene(string filename)
        {
            AssimpContext importer = new AssimpContext();

            Scene a_scene = null;

            importer.SetConfig(new NormalSmoothingAngleConfig(66.666f));

            try
            {
                a_scene = importer.ImportFile(filename, PostProcessSteps.Triangulate |
                                                        PostProcessSteps.CalculateTangentSpace |
                                                        PostProcessSteps.FlipUVs |
                                                        PostProcessSteps.LimitBoneWeights);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Unnable to open or found file : " + filename);
            }
            return a_scene;

        }


        public static void LoadSceneGeometry(ref Scene a_scene, out Dictionary<int, List<string>> MasksPerModelIDS,
                                                                out Dictionary<int, List<FloatBuffer>> Vertices,
                                                                out Dictionary<int, List<IntegerBuffer>> Indeces, 
                                                                out Dictionary<int, List<int>> MaterialIDs)
        {

            MasksPerModelIDS = new Dictionary<int, List<string>>();
            Vertices = new Dictionary<int, List<FloatBuffer>>();
            Indeces = new Dictionary<int, List<IntegerBuffer>>();
            MaterialIDs = new Dictionary<int, List<int>>();

            FloatBuffer Verts; IntegerBuffer Inds; int  AttribsMask;

            string Mname;

            foreach (Mesh m in a_scene.Meshes)
            {

                AttribsMask = 256;
                Mname = AssipPostProcess(m, out AttribsMask, out Verts, out Inds);
                if (AttribsMask==256)
                {
                    continue;
                }

                if (Vertices.ContainsKey(AttribsMask))
                {
                    MasksPerModelIDS[AttribsMask].Add(Mname);
                    Vertices[AttribsMask].Add(Verts);
                    Indeces[AttribsMask].Add(Inds);
                    MaterialIDs[AttribsMask].Add(m.MaterialIndex);
                }
                else
                {
                    Vertices.Add(AttribsMask, new List<FloatBuffer>());
                    Indeces.Add(AttribsMask,new List<IntegerBuffer>());
                    MaterialIDs.Add(AttribsMask, new List<int>());
                    MasksPerModelIDS.Add(AttribsMask, new List<string>());

                    MasksPerModelIDS[AttribsMask].Add(Mname);
                    Vertices[AttribsMask].Add(Verts);
                    Indeces[AttribsMask].Add(Inds);
                    MaterialIDs[AttribsMask].Add(m.MaterialIndex);

                }


            }
        }

        public static void LoadHeirarhy( Assimp.Node rootNode,  GameEntityStorrage storrage)
        {
            //ProcessNodes(ref storrage, rootNode, Assimp.Matrix4x4.Identity, new Transform(0, 0, 0), "");
        }


        //private static void ProcessNodes(ref GameEntityStorrage storrage, Assimp.Node node, Assimp.Matrix4x4 parentTransform, Transform parent, string parent_name)
        //{
        //    GameEntity go = new GameEntity();
            

        //    Vector3D scale, translation;
        //    Assimp.Quaternion rotation;
        //    Vector3 angles;

        //    Matrix4x4 matrix = node.Transform * parentTransform;
        //    // Matrix4x4 unityMatrix = Matrix4x4. matrix.();
        //    matrix.Decompose(out scale, out rotation, out translation);

        //    ToEulerAngles(rotation, out angles);

        //    go.SetLocalRotation(angles);
        //    go.SetLocalTranslation(translation[0], translation[1], translation[2]);
        //    go.SetLocalScale(scale[0], scale[1], scale[2]);

        //    //go.transform.localScale = new Vector3(scale[0], scale[1], scale[2]);
        //    //go.transform.rotation = new Assimp.Quaternion(rotation.X, rotation.Y, rotation.Z, rotation.W);
        //    //go.transform.position = new Vector3(translation[0], translation[1], translation[2]);

        //    go.SetWorldRotation(parent.Rotation);
        //    go.SetWorldTranslation(parent.Position);
        //    go.SetWorldScale(parent.Scale);

        //    //go.transform.parent = parent;

        //    storrage.AddItem(parent_name, node.Name, go);

        //    if (node.HasChildren)
        //    {
        //        foreach (Assimp.Node child in node.Children)
        //        {
        //           // ProcessNodes(ref storrage, child, matrix, go.GetLocalTransform(), node.Name);
        //        }
        //    }
        //}

        public static void LoadSceneGeometryTransforms(Scene a_scene, out Dictionary<string, Transform>Transforms)
        {
            Transforms = null;
            ///a_scene.RootNode.
        }

        private static string AssipPostProcess(Mesh a_mesh, out int AttribsMask, out FloatBuffer Vertices, out IntegerBuffer Indeces)
        {
            ///прочитать иерархию нодов 
            List<VertDataSetter> VDataSetters;

            int AttribsCount, AttribsByteSize;

            GetAttributesMask(a_mesh, out VDataSetters, out AttribsMask, out AttribsCount, out AttribsByteSize);

            if (AttribsMask == 256)
            {
                Vertices = null;
                Indeces = null;
                return "";
            }

            Vertices = new   FloatBuffer();

            Indeces = new  IntegerBuffer();
         
           /// Dictionary<string, Material> Materials = new Dictionary<string, Material>();
 
            for (int v = 0; v < a_mesh.VertexCount; v++)
            {
                for (int attribs = 0; attribs < AttribsCount; attribs++)
                {
                            VDataSetters[attribs](a_mesh,ref  Vertices, v);
                }
           }
           for (int idx = 0; idx < a_mesh.FaceCount; idx++)
           {
                Indeces.Put(a_mesh.Faces[idx].Indices[0]);
                Indeces.Put(a_mesh.Faces[idx].Indices[1]);
                Indeces.Put(a_mesh.Faces[idx].Indices[2]);
           }

            return a_mesh.Name;
        }

        private static void AssipPostProcess(Scene a_scene, string sName,out int[] AttribsMasks,  out  List<FloatBuffer>[] Models, out List<IntegerBuffer>[] ModelsIndeces)
        {
            ///прочитать иерархию нодов 

            ///Словарь < маска атрибутов, Список Мешей с такими же параметрами>

            Dictionary<int, List<FloatBuffer>> AtrMaskPerMeshes = new Dictionary<int, List<FloatBuffer>>();

            Dictionary<int, List<IntegerBuffer>> AtrMaskPerIndeses = new Dictionary<int, List<IntegerBuffer>>();

            Dictionary<string, Material> Materials = new Dictionary<string, Material>();

            int[] AttribsMask = new int[a_scene.Meshes.Count];

            int[] AttribsCount = new int[a_scene.Meshes.Count];

            int[] AttribsByteSize = new int[a_scene.Meshes.Count];

            int i =0;

            List<VertDataSetter> VDataSetters;

            foreach (Assimp.Mesh a_mesh in a_scene.Meshes)
            {
                GetAttributesMask(a_mesh, out  VDataSetters, out AttribsMask[i], out AttribsCount[i],out AttribsByteSize[i]);
                
                if (AttribsMask[i] == 256)
                {
                    continue;
                }
                #region если уже есть mesh  с таким набором атрибутов, то : 
               if (AtrMaskPerMeshes.ContainsKey(AttribsMask[i]))
                {
                    FloatBuffer Vbuff = new FloatBuffer(AttribsByteSize[i] * a_mesh.Vertices.Count);

                    IntegerBuffer Ibuff = new IntegerBuffer(3 * a_mesh.FaceCount);

                    Vbuff.BufferID = sName + "_vertices_data_" + i.ToString();

                    Ibuff.BufferID = sName + "_indeces_data_" + i.ToString();

                    for (int v = 0; v < a_mesh.VertexCount; v++)
                    {
                        for (int attribs = 0; attribs < AttribsCount[i]; attribs++)
                        {
                            VDataSetters[attribs](a_mesh, ref Vbuff, v);
                        }
                    }

                    for (int idx = 0; idx < a_mesh.FaceCount; idx++)
                    {
                        Ibuff.Put(a_mesh.Faces[idx].Indices[0]);
                        Ibuff.Put(a_mesh.Faces[idx].Indices[1]);
                        Ibuff.Put(a_mesh.Faces[idx].Indices[2]);
                    }

                    AtrMaskPerMeshes[AttribsMask[i]].Add(Vbuff);

                    AtrMaskPerIndeses[AttribsMask[i]].Add(Ibuff);
                }
                #endregion
                else 
                #region если нет mesh  с таким набором атрибутов, то : 
                {
                    AtrMaskPerMeshes.Add(AttribsMask[i], new List<FloatBuffer>());

                    AtrMaskPerIndeses.Add(AttribsMask[i], new List<IntegerBuffer>());

                    FloatBuffer Vbuff = new FloatBuffer(AttribsByteSize[i] * a_mesh.Vertices.Count);

                    IntegerBuffer Ibuff = new IntegerBuffer(3 * a_mesh.FaceCount);

                    Vbuff.BufferID = sName+"_vertices_data_"+ i.ToString();

                    Ibuff.BufferID = sName + "_indeces_data_" + i.ToString();

                    for (int v = 0; v < a_mesh.VertexCount; v++)
                    {
                        for (int attribs = 0; attribs < AttribsCount[i]; attribs++)
                        {
                            VDataSetters[attribs](a_mesh, ref Vbuff, v);
                        }
                    }

                    for (int idx = 0; idx < a_mesh.FaceCount; idx++)
                    {
                        Ibuff.Put(a_mesh.Faces[idx].Indices[0]);
                        Ibuff.Put(a_mesh.Faces[idx].Indices[1]);
                        Ibuff.Put(a_mesh.Faces[idx].Indices[2]);
                    }

                    AtrMaskPerMeshes[AttribsMask[i]].Add(Vbuff);

                    AtrMaskPerIndeses[AttribsMask[i]].Add(Ibuff);

                }
                #endregion
                i += 1;
            }

            AttribsMasks =  AtrMaskPerMeshes.Keys.ToArray();
            Models =        AtrMaskPerMeshes.Values.ToArray();
            ModelsIndeces = AtrMaskPerIndeses.Values.ToArray();
        }

        private static void GetMaterials(Scene scn)
        {
            
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

        private static void ToEulerAngles(Assimp.Quaternion q, out Vector3 angles)
        {
             angles=new Vector3();

            // roll (x-axis rotation)
            double sinr_cosp = +2.0 * (q.W * q.X + q.Y * q.Z);
            double cosr_cosp = +1.0 - 2.0 * (q.X * q.X + q.Y * q.Y);
            angles.X = (float)Math.Atan2(sinr_cosp, cosr_cosp);

            // pitch (y-axis rotation)
            double sinp = +2.0 * (q.W * q.Y - q.Z * q.X);
            if (Math.Abs(sinp) >= 1)
                angles.Y = (float)(Math.Sign(sinp) * Math.Max(Math.PI / 2, Math.Abs(sinp))); // use 90 degrees if out of range
            else
                angles.Z = (float)Math.Asin(sinp);

            // yaw (z-axis rotation)
            double siny_cosp = +2.0 * (q.W * q.Z + q.X * q.Y);
            double cosy_cosp = +1.0 - 2.0 * (q.Y * q.Y + q.Z * q.Z);
            angles.Z = (float)Math.Atan2(siny_cosp, cosy_cosp);

        
        }
    }

}
