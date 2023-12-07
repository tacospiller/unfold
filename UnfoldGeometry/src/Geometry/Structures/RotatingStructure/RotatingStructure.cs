using System.Numerics;
using Unfold.Serialization;

namespace Unfold.UnfoldGeometry
{
    public abstract class RotatingStructure : IStructure
    {
        public virtual DataDrivenAxis Axis { get; private set; }

        private IRotatingStructureDef _def;

#pragma warning disable CS8618
        public RotatingStructure(IStructureCache coll, IRotatingStructureDef def)
#pragma warning restore CS8618
        {
            _def = def;
            Axis = new DataDrivenAxis(def.Axis, coll);
        }

        public void RebuildAxis(IStructureCache coll)
        {
            Axis.RebuildAxis(coll);
        }


        public virtual IAxis? GetAxis(AxisDescriptor axis)
        {
            if (axis == IRotatingStructureDef.AxisDescriptors.BaseAxis)
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
