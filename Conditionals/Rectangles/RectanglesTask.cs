using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;

namespace Rectangles
{
    public static class RectanglesTask
    {
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            var r1Points = MakePointsList(r1);
            var r2Points = MakePointsList(r2);
            /*       return Math.Min(r1.Top + r1.Height, r2.Top + r2.Height) >= Math.Max(r1.Top, r2.Top) &&
                          Math.Min(r1.Left + r1.Width, r2.Left + r2.Width) >= Math.Max(r1.Left, r2.Left);*/
            //return !(r1.Right < r2.Left || r1.Left > r2.Right || r1.Top > r2.Bottom || r1.Bottom < r2.Top);
            return r1Points.Intersect(r2Points).Any();
        }

        public static List<Point> MakePointsList(Rectangle rect)
        {
            var list = new List<Point>();
            for (var x = rect.Left; x <= rect.Right; x++)
                for (var y = rect.Top; y <= rect.Bottom; y++)
                    list.Add(new Point { X = x, Y = y });
            return list;
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            var areaOfIntersaction = 0;
            if (AreIntersected(r1, r2))
                areaOfIntersaction = Math.Abs((Math.Min(r1.Right, r2.Right) - Math.Max(r1.Left, r2.Left)) *
                                              (Math.Min(r1.Bottom, r2.Bottom) - Math.Max(r1.Top, r2.Top)));
            return areaOfIntersaction;
        }

        private static int FindRelativePosition(Rectangle r1, Rectangle r2)
        {
            var leftSideComparison = r1.Left.CompareTo(r2.Left);
            var rightSideComparison = (r1.Left + r1.Width).CompareTo(r2.Left + r2.Width);
            var topComparison = r1.Top.CompareTo(r2.Top);
            var bottomComparison = r1.Bottom.CompareTo(r2.Bottom);

            if (leftSideComparison >= 0 && rightSideComparison <= 0 && topComparison > 0 && bottomComparison <= 0)
                return 0;
            if (leftSideComparison <= 0 && rightSideComparison >= 0 && topComparison <= 0 && bottomComparison > 0)
                return 1;
            return -1;
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            return FindRelativePosition(r1, r2);
        }
    }
}