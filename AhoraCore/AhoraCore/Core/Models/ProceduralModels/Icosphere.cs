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

        private static Dictionary<long, int> _middlePointIndexCache;

        public static void Create(int recursionLevel, out FloatBuffer vertsData, out IntegerBuffer indecesData )
        {
             vertsData = new FloatBuffer(60);

             indecesData = new IntegerBuffer(60);

            _middlePointIndexCache = new Dictionary<long, int>();

            _index = 0;

            v_offset = 0;

            var t = (float)((1.0 + Math.Sqrt(5.0)) / 2.0);
            var s = 1;
            #region init base mesh

            AddVertex(ref vertsData,-s, t, 0);
            AddVertex(ref vertsData, s, t, 0);
            AddVertex(ref vertsData,-s, -t, 0);
            AddVertex(ref vertsData,s, -t, 0);

            AddVertex(ref vertsData,0, -s, t);
            AddVertex(ref vertsData, 0, s, t);
            AddVertex(ref vertsData, 0, -s, -t);
            AddVertex(ref vertsData, 0, s, -t);

            AddVertex(ref vertsData,t, 0, -s);
            AddVertex(ref vertsData,t, 0, s);
            AddVertex(ref vertsData,-t, 0, -s);
            AddVertex(ref vertsData,-t, 0, s);

            // 5 faces around point 0
            AddFace(ref indecesData, 0, 11, 5);
            AddFace(ref indecesData, 0, 5,  1);
            AddFace(ref indecesData, 0, 1,  7);
            AddFace(ref indecesData, 0, 7,  10);
            AddFace(ref indecesData, 0, 10, 11);

            // 5 adjacent faces 
            AddFace(ref indecesData, 1,  5, 9);
            AddFace(ref indecesData, 5, 11, 4);
            AddFace(ref indecesData, 11, 10, 2);
            AddFace(ref indecesData, 10,  7, 6);
            AddFace(ref indecesData, 7,  1, 8);

            // 5 faces around point 3
            AddFace(ref indecesData,3, 9, 4);
            AddFace(ref indecesData,3, 4, 2);
            AddFace(ref indecesData,3, 2, 6);
            AddFace(ref indecesData,3, 6, 8);
            AddFace(ref indecesData,3, 8, 9);

            // 5 adjacent faces 
            AddFace(ref indecesData,4, 9, 5);
            AddFace(ref indecesData,2, 4, 11);
            AddFace(ref indecesData,6, 2, 10);
            AddFace(ref indecesData,8, 6, 7);
            AddFace(ref indecesData,9, 8, 1);
            #endregion

            // refine triangles
            //for (int i = 0; i < recursionLevel; i++)
            //{
            //    var indecesData2 = new IntegerBuffer();

            //    for (int k = 0; k < indecesData.Fillnes-3; k+=3)
            //    {
            //        // replace triangle by 4 triangles
            //        int a = GetMiddlePoint(ref vertsData, indecesData.Pop(k),     indecesData.Pop(k + 1))-1;//(tri.V1, tri.V2);v_offset/5 - 1
            //        int b = GetMiddlePoint(ref vertsData, indecesData.Pop(k + 1), indecesData.Pop(k + 2))-1;// tri.V2, tri.V3);v_offset/5 
            //        int c = GetMiddlePoint(ref vertsData, indecesData.Pop(k + 2), indecesData.Pop(k ))-1;// tri.V3, tri.V1);v_offset/5 + 1
            //        Console.WriteLine(a + " " + b + " " +c);
            //        AddFace(ref indecesData2, indecesData.Pop(k), a, c);
            //        AddFace(ref indecesData2, indecesData.Pop(k + 1), b, a);
            //        AddFace(ref indecesData2, indecesData.Pop(k + 2), c, b);
            //        AddFace(ref indecesData2, a, b, c);

            //    }
            //    indecesData = indecesData2;
            //}


            // done, now add triangles to mesh


            for (int i = 0; i < indecesData.Fillnes - 3; i += 3)
            {
                GetSphereCoord(ref vertsData, indecesData.Pop(i));

                GetSphereCoord(ref vertsData, indecesData.Pop(i + 1));

                GetSphereCoord(ref vertsData, indecesData.Pop(i + 2));


                //Console.WriteLine(indecesData.Pop(i)+" "+
                //                  indecesData.Pop((i + 1) )+ " " +
                //                  indecesData.Pop((i + 2) ));

                FixColorStrip(ref vertsData, indecesData.Pop(i) ,
                                             indecesData.Pop(i + 1) ,
                                             indecesData.Pop(i + 2));
            }

            // return vertices.ToArray();
        }

        private static void FixColorStrip(ref FloatBuffer buffer, int uv1, int uv2, int uv3)
        {
            uv1 = uv1 * 5;

            uv2 = uv2 * 5;

            uv3 = uv3 * 5;

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
            point = point * 5;
            var len =Math.Sqrt( buffer.Pop(point)* buffer.Pop(point)
                              + buffer.Pop(point + 1)* buffer.Pop(point + 1)
                              + buffer.Pop(point + 2)* buffer.Pop(point + 2));

            buffer.PutDirect(point + 3, -(float)((Math.Atan2(buffer.Pop(point + 2), buffer.Pop(point)) / Math.PI + 1.0f) * 0.5f));

            buffer.PutDirect(point + 4, (float)(Math.Acos(buffer.Pop(point + 1) / len) / Math.PI));
        }

        private static void AddVertex(ref FloatBuffer buffer, float x,float y, float z)
        {
            buffer.PutDirect(v_offset, x); buffer.PutDirect(v_offset + 1, y); buffer.PutDirect(v_offset + 2,z);
            _index++;
            v_offset += 5;
          
        }

        private static void AddFace(ref IntegerBuffer buffer, int i, int j, int k)
        {
            buffer.Put(i); buffer.Put(j); buffer.Put(k);
            ///      return _index++;
        }

        // return index of point in the middle of p1 and p2
        private static int GetMiddlePoint(ref FloatBuffer Data, long i1, long i2)
        {
            //long i1 = _points.IndexOf(point1);
            //long i2 = _points.IndexOf(point2);
            // first check if we have it already
            var firstIsSmaller = i1 < i2;
            long smallerIndex = firstIsSmaller ? i1 : i2;
            long greaterIndex = firstIsSmaller ? i2 : i1;
            long key = (smallerIndex << 32) + greaterIndex;

            int ret;

            if (_middlePointIndexCache.TryGetValue(key, out ret))
            {
                return ret ;
            }

            // not in cache, calculate it
            AddVertex(ref Data, 0.5f * (Data.Pop((int)i1*5    ) + Data.Pop((int)i2*5    )),
                                0.5f * (Data.Pop((int)i1*5 + 1) + Data.Pop((int)i2*5 + 1)),
                                0.5f * (Data.Pop((int)i1*5 + 2) + Data.Pop((int)i2*5 + 2)));
          
            _middlePointIndexCache.Add(key, _index);

            return _index;
        }

    }
}
