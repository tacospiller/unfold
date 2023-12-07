using System.Numerics;
using Unfold.Serialization;

namespace Unfold.UnfoldGeometry
{
    public class DataDrivenAxis : IAxis
    {
        private IAxis _impl;
        private AxisDef _def;

        public Matrix4x4 Transform => _impl.Transform;
        public double Angle => _impl.Angle;

        public DataDrivenAxis(AxisDef def, IStructureCache cache) {
            _def = def;
            _impl = StructureBuilder.BuildAxis(cache, def);
        }

        public void RebuildAxis(IStructureCache cache)
        {
            _impl = StructureBuilder.BuildAxis(cache, _def);
        }

        public void SetManualAxis(double angle)
        {
            if (_impl is ManualAxis)
            {
                (_impl as ManualAxis).SetAngle(angle);
            }
        }
    }
}
