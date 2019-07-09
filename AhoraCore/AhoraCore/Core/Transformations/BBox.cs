using OpenTK;
using System;

namespace AhoraCore.Core.Transformations
{
    public enum splitPlane
    {
        XY = 0,
        XZ = 1,
        YZ = 2,
        NONE = -1
    }
    public enum boxAxis
    {
        X = 0,
        Y = 1,
        Z = 2,
        NONE = -1
    }
    public class BBox
    {

        public Vector4 BBmax { get; protected set; }

        public Vector4 BBmin { get; protected set; }

        public Vector4 HalfBBoxArea { get; protected set; }

        public Vector4 XYZlength { get; protected set; }

        public boxAxis LogestAxis { get; protected set; }

        public float Volume { get; protected set; }

        public float SplitCoordinate { get; protected set; }

        public static void splitBBox(BBox b_box, boxAxis plane, ref float splitPoint, out BBox left, out BBox right)
        {
            b_box.splitBBox(plane, ref splitPoint, out left, out right);
        }

        public void splitBBox(boxAxis plane, ref float splitPoint, out BBox left, out BBox right)
        {
            left = new BBox(BBmax.Xyz, BBmin.Xyz);

            right = new BBox(BBmax.Xyz, BBmin.Xyz);

            SplitCoordinate = splitPoint;

            LogestAxis = plane;

            Vector4 Vleft = left.BBmax, Vright = right.BBmax;

            switch (plane)
            {
                
                case boxAxis.X:
                    Vleft.X = splitPoint;
                    Vright.X = splitPoint;
                    break;
                case boxAxis.Y:
                    Vleft.Y = splitPoint;
                    Vright.Y = splitPoint;
                    break;
                case boxAxis.Z:
                    Vleft.Z = splitPoint;
                    Vright.Z = splitPoint;
                    break;
            }
            left.BBmax = Vleft;

            right.BBmax = Vright;

            left.RecalcBBoxArea();

            right.RecalcBBoxArea();
        }

        public bool RayIntersectsBbox(Vector3 rayStartPos, Vector3 rayDirection, out float tmin, out float tmax)
        {
            float lo = (BBmin.X - rayStartPos.X) / rayDirection.X;

            float hi = (BBmax.X - rayStartPos.X) / rayDirection.X;

            tmin = Math.Min(lo, hi);

            tmax = Math.Max(lo, hi);

            float lo1 = (BBmin.Y - rayStartPos.Y) / rayDirection.Y;

            float hi1 = (BBmax.Y - rayStartPos.Y) / rayDirection.Y;

            tmin = Math.Max(tmin, Math.Min(lo1, hi1));

            tmax = Math.Min(tmax, Math.Max(lo1, hi1));

            float lo2 = (BBmin.Z - rayStartPos.Z) / rayDirection.Z;

            float hi2 = (BBmax.Z - rayStartPos.Z) / rayDirection.Z;

            tmin = Math.Max(tmin, Math.Min(lo2, hi2));

            tmax = Math.Min(tmax, Math.Max(lo2, hi2));

            return (tmin <= tmax) && (tmax > 0.0f);

        }

        public bool RayIntersectsBbox(Vector4 rayStartPos, Vector4 rayDirection, out float tmin, out float tmax)
        {
            float lo = (BBmin.X - rayStartPos.X) / rayDirection.X;

            float hi = (BBmax.X - rayStartPos.X) / rayDirection.X;

            tmin = Math.Min(lo, hi);

            tmax = Math.Max(lo, hi);

            float lo1 = (BBmin.Y - rayStartPos.Y) / rayDirection.Y;

            float hi1 = (BBmax.Y - rayStartPos.Y) / rayDirection.Y;

            tmin = Math.Max(tmin, Math.Min(lo1, hi1));

            tmax = Math.Min(tmax, Math.Max(lo1, hi1));

            float lo2 = (BBmin.Z - rayStartPos.Z) / rayDirection.Z;

            float hi2 = (BBmax.Z - rayStartPos.Z) / rayDirection.Z;

            tmin = Math.Max(tmin, Math.Min(lo2, hi2));

            tmax = Math.Min(tmax, Math.Max(lo2, hi2));

            return (tmin <= tmax) && (tmax > 0.0f);

        }
  
        public boxAxis FindLongestAxis()
        {
            boxAxis s = boxAxis.X;

            if ((XYZlength.Y > XYZlength.X))
            {
                s = boxAxis.Y;
            }

            if ((XYZlength.Z > XYZlength.Y))
            {
                s = boxAxis.Z;
            }

            return s;

        }

        public BBox(Vector3 BBmax, Vector3 BBmin)
        {
            this.BBmax = new Vector4(BBmax.X, BBmax.Y, BBmax.Z, 0);
            this.BBmin = new Vector4(BBmin.X, BBmin.Y, BBmin.Z, 0);

            // bboxModel = new Box(BBmin,BBmax);

            XYZlength = this.BBmax - this.BBmin;
            ///  Console.WriteLine(" bBox Length = " + XYZlength.asString());

            float sx = XYZlength.X * XYZlength.Y;
            float sy = XYZlength.X * XYZlength.Z;
            float sz = XYZlength.Y * XYZlength.Z;

            HalfBBoxArea = new Vector4(sx,sy,sz,sx+sy+sz);

            Volume = HalfBBoxArea.X * XYZlength.Z;
            
            LogestAxis = boxAxis.NONE;

            SplitCoordinate = 0;

            LogestAxis = FindLongestAxis();

            
        }

        public void RecalcBBoxArea()
        {
            XYZlength = BBmax - BBmin;

            float sx = XYZlength.X * XYZlength.Y;
            float sy = XYZlength.X * XYZlength.Z;
            float sz = XYZlength.Y * XYZlength.Z;

            HalfBBoxArea = new Vector4(sx, sy, sz, sx + sy + sz);

            Volume = HalfBBoxArea.X * XYZlength.Z;

            LogestAxis = FindLongestAxis();
        }
      
        private bool IsPointInside(Vector3 point)
        {
            if (point.X < BBmin.X || point.X > BBmax.X)
            {
                return false;
            }
            if (point.Y < BBmin.Y || point.Y > BBmax.Y)
            {
                return false;
            }
            if (point.Z < BBmin.Z || point.Z > BBmax.Z)
            {
                return false;
            }
            return true;
        }

        private bool IsPointInside(Vector4 point)
        {
            if (point.X < BBmin.X || point.X > BBmax.X)
            {
                return false;
            }
            if (point.Y < BBmin.Y || point.Y > BBmax.Y)
            {
                return false;
            }
            if (point.Z < BBmin.Z || point.Z > BBmax.Z)
            {
                return false;
            }
            return true;
        }
        
        public string BBoxAsSTring()
        {
            return "max_b : x = " + BBmax.X + " y = " + BBmax.Y + " z = " + BBmax.Z + "\n" +
                   "min_b : x = " + BBmin.X + " y = " + BBmin.Y + " z = " + BBmin.Z + "\n" +
                   "Barea : Sx = " + HalfBBoxArea.X + " Sy = " + HalfBBoxArea.Y + " Sz = " + HalfBBoxArea.Z + " S = " + HalfBBoxArea.W + "\n" +
                   "volume = " + Volume;
        }

    }
}
