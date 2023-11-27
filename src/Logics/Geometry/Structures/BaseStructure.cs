using System.Linq;
using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public abstract class BaseStructure : IStructure
    {
        public virtual IAxis Axis { get; }
        public virtual double MaxAngle { get; }

        public BaseStructure(IAxis axis)
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
