using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

namespace UnfoldGeometry.Serialization
{
    public record class SymmetricVFoldDef : IStructureDef
    {
        public StructureId Id { get; set; }
        public AxisDef Axis { get; set; }
        public double DistA { get; set; }
        public double DistD { get; set; }
        public double Theta { get; set; }
        public double Psi { get; set; }


        private IStructure? _structure;
        public IStructure GetStructure(IStructureDefCollection coll)
        {
            if (_structure == null)
            {
                _structure = new SymmetricVFoldStructure(coll, this);
            }
            return _structure;
        }
    }
}
