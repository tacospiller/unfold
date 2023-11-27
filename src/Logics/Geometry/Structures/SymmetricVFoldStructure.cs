using System;
using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public class SymmetricVFoldStructure : IStructure
    {
        // B is the origin point. segment AB and CB are attached to base. D is a free floating(calculated) point. 
        // Triangle ABD and CBD are moving faces of the v-fold.
        public double AngleABY { get; init; } = Angles.Deg60;
        public double AngleABD { get; init; } = Angles.Deg90;
        public double DistAB { get; init; } = 1;
        public double DistBD { get; init; } = 1;
        // "unfolding" angle
        public double FoldAngle { get; set; } = Angles.Deg0;
        public double AngleAYC => FoldAngle;

        public Vector3 A => new Vector3((float)(DistAB * Math.Sin(AngleABY)), (float)(DistAB * Math.Cos(AngleABY)), 0);
        public Vector3 C => new Vector3((float)(DistAB * Math.Sin(AngleABY) * Math.Cos(AngleAYC)), (float)(DistAB * Math.Cos(AngleABY)), (float)(DistAB * Math.Sin(AngleABY) * Math.Sin(AngleAYC)));
        public Vector3 B => new Vector3(0, 0, 0);
        public Vector3 DInitial => new Vector3((float)(DistBD * Math.Sin(AngleABY + AngleABD)), (float)(DistBD * Math.Cos(AngleABY + AngleABD)), 0);
        public Vector3 DFinal => new Vector3((float)(DistBD * Math.Sin(AngleABY + AngleABD) * Math.Cos(AngleAYC)), (float)(DistBD * Math.Cos(AngleABY + AngleABD)), (float)(DistBD * Math.Sin(AngleABY + AngleABD) * Math.Sin(AngleAYC)));
        public double DistAD => (DInitial - A).Length();
        public Vector3 D => UnfoldMath.Trilaterate(B, A, C, DistBD, DistAD, DistAD);

        public double MaxAngle => Angles.Deg90 * 2; // todo
        public Vector3[] Faces => new Vector3[] { B, A, D,   C, B, D };


        public Fold ABOuterFold
        {
            get
            {
                return new Fold(() =>
                {
                    return Matrix4x4.CreateRotationZ((float)-AngleABY);
                }, () =>
                {
                    return UnfoldMath.GetAngle(Plane.CreateFromVertices(A, B, D).Normal, Plane.CreateFromVertices(A, B, DInitial).Normal);
                });
            }
        }

        public Fold CBOuterFold
        {
            get
            {
                return new Fold(() =>
                {
                    var rot = Matrix4x4.CreateRotationY((float)-AngleAYC);
                    var rot2 = Matrix4x4.CreateRotationZ((float)-AngleABY);
                    var flip = Matrix4x4.CreateReflection(Plane.CreateFromVertices(B, C, DFinal));
                    return rot2 * rot * flip;
                }, () =>
                {
                    return UnfoldMath.GetAngle(Plane.CreateFromVertices(C, B, D).Normal, Plane.CreateFromVertices(C, B, DFinal).Normal);
                });
            }
        }


        public SymmetricVFoldStructure()
        { }
    }
}
