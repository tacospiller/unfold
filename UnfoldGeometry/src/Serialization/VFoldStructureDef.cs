using Unfold.UnfoldGeometry;
using UnfoldGeometry.Serialization;

namespace UnfoldGeometry.src.Serialization
{
    public class VFoldStructureDef : IStructureDef
    {
        public StructureDefKey Id { get; set; }
        public AxisDef? Axis { get; set; }
        public double DistA { get; set; }
        public double DistD { get; set; }
        public double Theta { get; set; }
        public double Psi { get; set; }

        private IStructure? _structure;
        public IStructure GetStructure(IStructureDefCollection coll)
        {
            if (_structure == null)
            {
                _structure = new SymmetricVFoldStructure(Axis?.ToAxis(coll) ?? new ManualAxis())
                {
                    DistA = DistA,
                    DistD = DistD,
                    Theta = Theta,
                    Psi = Psi,
                };
            }
            return _structure;
        }
    }
}
