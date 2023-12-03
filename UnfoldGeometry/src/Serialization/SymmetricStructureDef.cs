using Unfold.UnfoldGeometry;
using UnfoldGeometry.Serialization;

namespace UnfoldGeometry.src.Serialization
{
    public class SymmetricStructureDef : IStructureDef
    {
        public StructureDefKey Id { get; set; }
        public AxisDef? Axis { get; set; }
        public double DistFromAxis { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        private IStructure? _structure;
        public IStructure GetStructure(IStructureDefCollection coll)
        {
            if (_structure == null)
            {
                _structure = new SymmetricParallelStructure(Axis?.ToAxis(coll) ?? new ManualAxis())
                {
                    DistFromAxis = DistFromAxis,
                    Height = Height,
                    Width = Width
                };
            }
            return _structure;
        }
    }
}
