using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

namespace Unfold.Serialization
{
    public record class SymmetricVFoldDef : IStructureDef
    {
        public StructureId Id { get; set; }
        public AxisDef Axis { get; set; }
        public double DistA { get; set; }
        public double DistD { get; set; }
        public double Theta { get; set; }
        public double Psi { get; set; }

        public IStructure CreateStructure(DefStructurePairCollection coll)
        {
            return new SymmetricVFoldStructure(coll, this);
        }
    }
}
