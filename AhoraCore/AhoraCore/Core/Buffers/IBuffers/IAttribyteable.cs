using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.Buffers.IBuffers
{
     
        public static class VericesAttribytes
        {
        public static int V_POSITION = 0;
        public static int V_UVS = 1;
        public static int V_NORMAL = 2;
        public static int V_TANGENT = 4;
        public static int V_BITANGENT = 8;
        public static int V_BONES = 16;
        public static int V_BONES_WEIGHTS = 32;
        public static int V_COLOR_RGB = 64;
        public static int V_COLOR_RGBA = 128;
        }

        public interface IAttribyteable
        {
            void MarkBufferAttributePointer(int attrType, int attribID, int sampleSize, int from);
            void MarkBufferAttributePointers(int VericesAttribytesMap);
            void EnableAttribytes();
            void DisableAttribytes();

        }
   
}
