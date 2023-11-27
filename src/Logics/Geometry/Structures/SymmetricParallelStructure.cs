using System;
using System.Numerics;
using System.Windows.Media.Media3D;

namespace Unfold.UnfoldGeometry
{
    public class SymmetricParallelStructure : IStructure
    {
        // segment AD and BE are attached to base. C and F are free floating point.
        public double FoldAngle { get; set; } = Angles.Deg0;

        public double Width { get; init; } = 0.8;
        public double Height { get; init; } = 1;
        public double DistFromAxis { get; init; } = 1;

        public Vector3 A => new Vector3((float)DistFromAxis, (float)Height, 0);
        public Vector3 B => new Vector3((float)(DistFromAxis * Math.Cos(FoldAngle)), (float)Height, (float)(DistFromAxis * Math.Sin(FoldAngle)));
        public Vector3 CInitial => new Vector3((float)(DistFromAxis + Width), (float)Height, 0);
        public Vector3 C
        {
            get
            {
                var N = Vector3.Normalize(new Vector3((float)(DistFromAxis * Math.Cos(FoldAngle / 2)), 0, (float)(DistFromAxis * Math.Sin(FoldAngle / 2))));
                var a = DistFromAxis;
                var r = Width;
                var h = Math.Sqrt((r * r) - (a * a * Math.Sin(FoldAngle / 2) * Math.Sin(FoldAngle / 2))) + (a * Math.Cos(FoldAngle / 2));
                N *= (float)h;
                return new Vector3(N.X, (float)Height, N.Z);
            }
        }
        public Vector3 D => A + new Vector3(0, (float)-Height, 0);
        public Vector3 E => B + new Vector3(0, (float)-Height, 0);
        public Vector3 F => C + new Vector3(0, (float)-Height, 0);

        public double MaxAngle => 2 * Math.Asin(Width / DistFromAxis);
        public Vector3[] Faces => new Vector3[] { A, C, F,   A, F, D,   C, B, E,   C, E, F };
    }
}
