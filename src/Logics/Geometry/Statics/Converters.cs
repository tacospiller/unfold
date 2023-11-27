using System.Numerics;
using System.Windows.Media.Media3D;

namespace Unfold.UnfoldGeometry
{
    internal static class Converters
    {
        static public Point3D ToPoint3D(this Vector3 vector)
        {
            return new Point3D(vector.X, vector.Y, vector.Z);
        }
    }
}
