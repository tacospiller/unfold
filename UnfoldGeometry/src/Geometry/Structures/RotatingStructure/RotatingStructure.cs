using System.Numerics;
using UnfoldGeometry.Serialization;

namespace Unfold.UnfoldGeometry
{
    public abstract class RotatingStructure : IStructure
    {
        public virtual IAxis Axis { get; }

        public RotatingStructure(IAxis axis)
        {
            Axis = axis;
        }

        public static class AxisDescriptors
        {
            public static AxisDescriptor BaseAxis = new AxisDescriptor("RotatingStructure.BaseAxis");
        }

        public virtual IAxis? GetAxis(AxisDescriptor axis)
        {
            if (axis == AxisDescriptors.BaseAxis)
            {
                return Axis;
            }
            return null;
        }

        public Vector3[] CalculateFaces()
        {
            var faces = CalculateUntransformedFaces();
            return faces.Select(x => Vector3.Transform(x, Axis.Transform)).ToArray();
        }

        protected abstract Vector3[] CalculateUntransformedFaces();
    }
}
