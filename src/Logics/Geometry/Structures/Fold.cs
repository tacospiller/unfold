using System;
using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    // TODO: need a trasform matrix
    public struct Fold
    {
        public Func<Matrix4x4> RotationMatrix { get; }
        public Func<double> FoldAngle { get; }

        public Fold(Func<Matrix4x4> rot, Func<double> foldAngle)
        {
            RotationMatrix = rot;
            FoldAngle = foldAngle;
        }
    }
}
