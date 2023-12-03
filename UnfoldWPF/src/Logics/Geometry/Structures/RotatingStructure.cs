using System.Linq;
using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public abstract class RotatingStructure : IStructure
    {
        protected virtual IAxis Axis { get; }

        public RotatingStructure(IAxis axis)
        {
            Axis = axis;
        }

        public Vector3[] CalculateFaces()
        {
            var faces = CalculateUntransformedFaces();
            return faces.Select(x => Vector3.Transform(x, Axis.Transform)).ToArray();
        }

        protected abstract Vector3[] CalculateUntransformedFaces();
    }
}
