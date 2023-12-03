
using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    static public class Shapes
    {
        public static Vector3[] Cylinder(int edges)
        {
            return Enumerable.Range(0, edges).SelectMany((i) =>
            {
                var angle1 = (float)i / edges * Math.PI * 2;
                var angle2 = (float)(i + 1) / edges * Math.PI * 2;
                return new Vector3[]
                {
                    new Vector3((float)Math.Cos(angle1), (float)Math.Sin(angle1), 0),
                    new Vector3((float)Math.Cos(angle1), (float)Math.Sin(angle1), 1),
                    new Vector3((float)Math.Cos(angle2), (float)Math.Sin(angle2), 1),
                    new Vector3((float)Math.Cos(angle2), (float)Math.Sin(angle2), 1),
                    new Vector3((float)Math.Cos(angle2), (float)Math.Sin(angle2), 0),
                    new Vector3((float)Math.Cos(angle1), (float)Math.Sin(angle1), 0),
                };
            }).ToArray();
        }
    }
}
