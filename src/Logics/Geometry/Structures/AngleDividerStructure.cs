using System;
using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public class AngleDividerStructure : IStructure
    {
        public double FoldAngle { get; set; } = Angles.Deg0;

        public double MaxAngleAYC { get; init; } = Angles.Deg45;
        public double MaxAngleBYC { get; init; } = Angles.Deg90;

        public double MaxAngle => MaxAngleAYC + MaxAngleBYC;

        public double Width { get; init; } = 1;
        public double Height { get; init; } = 1.2;

        public Vector3 A => new Vector3((float)Width, (float)Height, 0);
        public Vector3 B => new Vector3((float)(Width * Math.Cos(FoldAngle)), (float)Height, (float)(Width * Math.Sin(FoldAngle)));
        public Vector3 C => new Vector3((float)(Width * Math.Cos(FoldAngle / MaxAngle * MaxAngleAYC)), (float)Height, (float)(Width * Math.Sin(FoldAngle / MaxAngle * MaxAngleAYC)));
        public Vector3 D => A + new Vector3(0, (float)-Height, 0);
        public Vector3 E => B + new Vector3(0, (float)-Height, 0);
        public Vector3 F => C + new Vector3(0, (float)-Height, 0);
        public Vector3 Y => new Vector3(0, (float)Height, 0);
        public Vector3 O => new Vector3(0, 0, 0);

        public Vector3[] Faces => new Vector3[] { 
            A, O, Y, A, D, O, 
            B, Y, O, B, O, E,
            C, Y, O, C, O, F};

    }
}
