using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.StandartBuffers;
using OpenTK;
using System;
using System.Collections.Generic;


namespace AhoraCore.Core.Models.ProceduralModels
{
   public static class Icosphere
    {
        private static int _index;
        
      

        private static int v_offset;

        private static int v_offset_stp;

        private static int v_offset_iterations;

        private static Dictionary<long, int> _middlePointIndexCache;

        private static FloatBuffer vBufferChache;


        public static void Create(int recursionLevel, out FloatBuffer vertsData, out IntegerBuffer indecesData)
        {
            Create(recursionLevel,0, out vertsData, out indecesData);
        }
        public static void Create(int recursionLevel, int mode, out FloatBuffer vertsData, out IntegerBuffer indecesData)
        {
            ////mode == 0 - P_x/P_y/P_z/Ux/Uy/
            ////mode == 1 - P_x/P_y/P_z/Ux/Uy/N_x/N_y/N_z
            ////mode == 2 - P_x/P_y/P_z/Ux/Uy/N_x/N_y/N_z/T_x/T_y/T_z
            if (mode == 0)
            {
                v_offset_stp = 5;
            }
            else if (mode == 1)
            {
                v_offset_stp = 8;
            }
            else if (mode == 2)
            {
                v_offset_stp = 11;
            }
            else
            {
                mode = 0;
                v_offset_stp = 5;
            }


            v_offset_iterations = 0;

            vBufferChache = new FloatBuffer(30* v_offset_stp);

            _middlePointIndexCache = new Dictionary<long, int>();

            _index = -1;

            v_offset = 0;

            var t = (float)((1.0 + Math.Sqrt(5.0)) / 2.0);
            var s = 1;
            #region init base mesh

            AddVertex(ref vBufferChache, -s, t, 0);
            AddVertex(ref vBufferChache, s, t, 0);
            AddVertex(ref vBufferChache, -s, -t, 0);
            AddVertex(ref vBufferChache, s, -t, 0);

            AddVertex(ref vBufferChache, 0, -s, t);
            AddVertex(ref vBufferChache, 0, s, t);
            AddVertex(ref vBufferChache, 0, -s, -t);
            AddVertex(ref vBufferChache, 0, s, -t);

            AddVertex(ref vBufferChache, t, 0, -s);
            AddVertex(ref vBufferChache, t, 0, s);
            AddVertex(ref vBufferChache, -t, 0, -s);
            AddVertex(ref vBufferChache, -t, 0, s);


            indecesData = new IntegerBuffer(60);

           /// 5 faces around point 0
            AddFace(ref indecesData, 0, 11, 5);
            AddFace(ref indecesData, 0, 5, 1);
            AddFace(ref indecesData, 0, 1, 7);
            AddFace(ref indecesData, 0, 7, 10);
            AddFace(ref indecesData, 0, 10, 11);

            // 5 adjacent faces 
            AddFace(ref indecesData, 1, 5, 9);
            AddFace(ref indecesData, 5, 11, 4);
            AddFace(ref indecesData, 11, 10, 2);
            AddFace(ref indecesData, 10, 7, 6);
            AddFace(ref indecesData, 7, 1, 8);

          ////  5 faces around point 3
            AddFace(ref indecesData, 3, 9, 4);
            AddFace(ref indecesData, 3, 4, 2);
            AddFace(ref indecesData, 3, 2, 6);
            AddFace(ref indecesData, 3, 6, 8);
            AddFace(ref indecesData, 3, 8, 9);

            // 5 adjacent faces 
            AddFace(ref indecesData, 4, 9, 5);
            AddFace(ref indecesData, 2, 4, 11);
            AddFace(ref indecesData, 6, 2, 10);
            AddFace(ref indecesData, 8, 6, 7);
            AddFace(ref indecesData, 9, 8, 1);
            #endregion

            // refine triangles

            Console.WriteLine(indecesData.Fillnes);

            for (int i = 0; i < recursionLevel; i++)
            {
                var indecesData2 = new IntegerBuffer();

                v_offset_iterations = 0;

                for (int k = 0; k < indecesData.Capacity ; k += 3)
                {
                   /// Console.WriteLine(indecesData2.Capacity);
                    // replace triangle by 4 triangles
                    int a = GetMiddlePoint(ref vBufferChache, indecesData.Pop(k), indecesData.Pop(k + 1));//(tri.V1, tri.V2);v_offset/5 - 1

                    int b = GetMiddlePoint(ref vBufferChache, indecesData.Pop(k + 1), indecesData.Pop(k + 2));// tri.V2, tri.V3);v_offset/5 

                    int c = GetMiddlePoint(ref vBufferChache, indecesData.Pop(k + 2), indecesData.Pop(k));// tri.V3, tri.V1);v_offset/5 + 1

                    //Console.WriteLine(a + " " + b + " " + c);

                    AddFace(ref indecesData2, indecesData.Pop(k), a, c);

                    AddFace(ref indecesData2, indecesData.Pop(k + 1), b, a);

                    AddFace(ref indecesData2, indecesData.Pop(k + 2), c, b);

                    AddFace(ref indecesData2, a, b, c);

                  
                }
                indecesData = indecesData2;
            }
           vertsData = new FloatBuffer(5);

            // done, now add triangles to mesh


            for (int i = 0; i < indecesData.Capacity - 3; i += 3)
            {
                GetSphereCoord(ref vBufferChache, indecesData.Pop(i));

                GetSphereCoord(ref vBufferChache, indecesData.Pop(i + 1));

                GetSphereCoord(ref vBufferChache, indecesData.Pop(i + 2));

                FixColorStrip(ref vBufferChache, indecesData.Pop(i) ,
                                                 indecesData.Pop(i + 1) ,
                                                 indecesData.Pop(i + 2));

                for (int k = 0; k < 3; k++)
                {
                    
                    vertsData.PutDirect(indecesData.Pop(i + k) * v_offset_stp,     vBufferChache.Pop(indecesData.Pop(i + k) * v_offset_stp));
                    vertsData.PutDirect(indecesData.Pop(i + k) * v_offset_stp + 1, vBufferChache.Pop(indecesData.Pop(i + k) * v_offset_stp + 1));
                    vertsData.PutDirect(indecesData.Pop(i + k) * v_offset_stp + 2, vBufferChache.Pop(indecesData.Pop(i + k) * v_offset_stp + 2));
                    vertsData.PutDirect(indecesData.Pop(i + k) * v_offset_stp + 3, vBufferChache.Pop(indecesData.Pop(i + k) * v_offset_stp + 3));
                    vertsData.PutDirect(indecesData.Pop(i + k) * v_offset_stp + 4, vBufferChache.Pop(indecesData.Pop(i + k) * v_offset_stp + 4));
                    vertsData.EnhanceBuffer(vertsData.Capacity + v_offset_stp);
                }


            }

   }

        private static void FixColorStrip(ref FloatBuffer buffer, int uv1, int uv2, int uv3)
        {
            uv1 = uv1 * v_offset_stp;

            uv2 = uv2 * v_offset_stp;

            uv3 = uv3 * v_offset_stp;

            if ((buffer.Pop(uv1 + 3) - buffer.Pop(uv2 + 3)) >= 0.8f)
            {
                buffer.PutDirect(uv1 + 3, buffer.Pop(uv1 + 3) - 1);//  uv1.X -= 1;
            }
            if ((buffer.Pop(uv2 + 3) - buffer.Pop(uv3 + 3)) >= 0.8f)
            {
                buffer.PutDirect(uv2 + 3, buffer.Pop(uv2 + 3) - 1);///uv2.X -= 1;
            }
            if ((buffer.Pop(uv3 + 3) - buffer.Pop(uv1 + 3)) >= 0.8f)
            {
                buffer.PutDirect(uv3 + 3, buffer.Pop(uv3 + 3) - 1);////uv3.X -= 1;
            }
            //if ((uv1.X - uv2.X) >= 0.8f)
            //    uv1.X -= 1;
            //if ((uv2.X - uv3.X) >= 0.8f)
            //    uv2.X -= 1;
            //if ((uv3.X - uv1.X) >= 0.8f)
            //    uv3.X -= 1;
        }

        public static void GetSphereCoord(ref FloatBuffer buffer, int point)//Vector3 i)
        {
            point =   point * v_offset_stp;
            float len = (float)Math.Sqrt( buffer.Pop(point)* buffer.Pop(point)
                              + buffer.Pop(point + 1)* buffer.Pop(point + 1)
                              + buffer.Pop(point + 2)* buffer.Pop(point + 2));
            
            buffer.PutDirect(point + 3, -(float)((Math.Atan2(buffer.Pop(point + 2), buffer.Pop(point)) / Math.PI + 1.0f) * 0.5f));

            buffer.PutDirect(point + 4, (float)(Math.Acos(buffer.Pop(point + 1) / len) / Math.PI));

            buffer.PutDirect(point + 0, buffer.Pop(point) / len);

            buffer.PutDirect(point + 1, buffer.Pop(point + 1) / len);

            buffer.PutDirect(point + 2, buffer.Pop(point + 2) / len);


        }

        private static void AddVertex(ref FloatBuffer buffer, float x,float y, float z)
        {
            buffer.EnhanceBuffer(v_offset+ v_offset_stp);
            buffer.PutDirect(v_offset, x); buffer.PutDirect(v_offset + 1, y); buffer.PutDirect(v_offset + 2,z);
            _index++;
            v_offset += v_offset_stp;
          
        }

        private static void AddFace(ref IntegerBuffer buffer, int i, int j, int k)
        {
            buffer.Put(i); buffer.Put(j); buffer.Put(k);
        }

        // return index of point in the middle of p1 and p2
        private static int GetMiddlePoint(ref FloatBuffer Data, int i1, int i2)
        {
            var firstIsSmaller = i1 < i2;
            int smallerIndex = firstIsSmaller ? i1 : i2;
            int greaterIndex = firstIsSmaller ? i2 : i1;
            long key = (smallerIndex << 16) + greaterIndex;

            int ret;

            if (_middlePointIndexCache.TryGetValue(key, out ret))
            {
                Console.WriteLine("key " + key + " ret " + ret);
                return ret;
            }

            // not in cache, calculate it
            AddVertex(ref Data, 0.5f * (Data.Pop(i1* v_offset_stp) + Data.Pop(i2* v_offset_stp)),
                                0.5f * (Data.Pop(i1* v_offset_stp + 1) + Data.Pop(i2* v_offset_stp + 1)),
                                0.5f * (Data.Pop(i1* v_offset_stp + 2) + Data.Pop(i2* v_offset_stp + 2)));

            _middlePointIndexCache.Add(key, _index);
            v_offset_iterations++;
            return _index;
        }

    }
}
