using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public class SymmetricParallelStructure : RotatingStructure
    {
        // segment AD and BE are attached to base. C and F are free floating point.

        public double Width { get; init; } = 0.8;
        public double Height { get; init; } = 1;
        public double DistFromAxis { get; init; } = 1;

        public Vector3 A => new Vector3((float)DistFromAxis, (float)Height, 0);
        public Vector3 B => new Vector3((float)(DistFromAxis * Math.Cos(Axis.Angle)), (float)Height, (float)(DistFromAxis * Math.Sin(Axis.Angle)));
        public Vector3 CInitial => new Vector3((float)(DistFromAxis + Width), (float)Height, 0);
        public Vector3 C
        {
            get
            {
                var N = Vector3.Normalize(new Vector3((float)(DistFromAxis * Math.Cos(Axis.Angle / 2)), 0, (float)(DistFromAxis * Math.Sin(Axis.Angle / 2))));
                var a = DistFromAxis;
                var r = Width;
                var h = Math.Sqrt((r * r) - (a * a * Math.Sin(Axis.Angle / 2) * Math.Sin(Axis.Angle / 2))) + (a * Math.Cos(Axis.Angle / 2));
                N *= (float)h;
                return new Vector3(N.X, (float)Height, N.Z);
            }
        }
        public Vector3 D => A + new Vector3(0, (float)-Height, 0);
        public Vector3 E => B + new Vector3(0, (float)-Height, 0);
        public Vector3 F => C + new Vector3(0, (float)-Height, 0);


        public IAxis AOuterFold => new DynamicAxis(() => Matrix4x4.CreateTranslation((float)DistFromAxis, 0, 0) * Axis.Transform, () =>  UnfoldMath.GetAngle(D, F - D) );
        public IAxis BOuterFold => new DynamicAxis(() => Matrix4x4.CreateTranslation((float)DistFromAxis, 0, 0) * Matrix4x4.CreateRotationZ((float)Axis.Angle) * Axis.Transform, () => UnfoldMath.GetAngle(F, F - E));

        protected override Vector3[] CalculateUntransformedFaces()
        {
            return new Vector3[] { A, C, F, A, F, D, C, B, E, C, E, F };
        }

        public SymmetricParallelStructure(IAxis axis) : base(axis) { }
    }
}
