using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public class ChildStructure : IStructure
    {
        public IStructure Parent { get; }
        public IStructure Child { get; }
        public Fold Fold { get; }
        public double OffsetY { get; set; }

        public double FoldAngle
        {
            get
            {
                return Parent.FoldAngle;
            }
            set
            {
                //Parent.FoldAngle = value;
                //Child.FoldAngle = Fold.Angle();
            }
        }

        public double MaxAngle => Parent.MaxAngle; // todo
        public Vector3[] Faces
        {
            get
            {
                Child.FoldAngle = Fold.FoldAngle();
                var faces = Child.Faces;
                return faces.Select(x => Vector3.Transform(x + new Vector3(0, (float)OffsetY, 0), Fold.RotationMatrix())).ToArray();
            }
        }

        public ChildStructure(IStructure parent, IStructure child, Fold fold)
        {
            Parent = parent;
            Child = child;
            Fold = fold;
        }
    }
}
