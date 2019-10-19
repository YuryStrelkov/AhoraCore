using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.GUI.CharSets
{
    class Character
    {
        private int id;
        private float xTextureCoord;
        private float yTextureCoord;
        private float xTextureWidth;
        private float yTextureHeight;
        private float xOffset;
        private float yOffset;
        private int page;
        private int chnl;
        private float xAdvance;

        public int Id
        {
            get
            {
                return id;
            }
        }

        public float XTextureCoord
        {
            get
            {
                return xTextureCoord;
            }
        }

        public float YTextureCoord
        {
            get
            {
                return yTextureCoord;
            }

        }

        public float XTextureWidth
        {
            get
            {
                return xTextureWidth;
            }

        }

        public float YTextureHeight
        {
            get
            {
                return yTextureHeight;
            }

        }

        public float XOffset
        {
            get
            {
                return xOffset;
            }

        }

        public float YOffset
        {
            get
            {
                return yOffset;
            }
        }

        public int Page
        {
            get
            {
                return page;
            }
        }

        public int Chnl
        {
            get
            {
                return chnl;
            }
        }

        public float XAdvance
        {
            get
            {
                return xAdvance;
            }

        }

        public Character(
            int id,
            float x, float y, float width, float height,
            float xoffset, float yoffset,
            float xadvance, int page, int chnl)
        {
            this.id = id;
            this.xTextureCoord = x;
            this.yTextureCoord = y;
            this.xOffset = xoffset;
            this.yOffset = yoffset;
            this.page = page;
            this.chnl = chnl;
            this.xTextureWidth = width;
            this.yTextureHeight = height;
            this.xAdvance = xadvance;
        }
    }
}
