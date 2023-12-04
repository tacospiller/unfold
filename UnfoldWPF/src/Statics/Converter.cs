using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Unfold.UnfoldWPF
{
    public static class Converter
    {
        public static Point3D ToPoint3D(this Vector3 vector)
        {
            return new Point3D(vector.X, vector.Y, vector.Z);
        }

        public static Geometry3D ToGeometry3D(this IEnumerable<Vector3> vectors)
        {
            return new MeshGeometry3D
            {
                Positions = new Point3DCollection(vectors.Select(x => x.ToPoint3D()))
            };
        }
    }

    public static class DisplayHelper
    {
        public static Color RandomColor()
        {
            return Color.FromScRgb(1.0f, Random.Shared.NextSingle(), Random.Shared.NextSingle(), Random.Shared.NextSingle());
        }
    }
}
