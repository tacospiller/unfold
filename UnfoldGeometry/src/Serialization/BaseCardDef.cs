using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

namespace Unfold.Serialization
{
    public record class BaseCardDef : IStructureDef
    {
        public StructureId Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public AxisDef Axis { get; set; }


        public IStructure CreateStructure(DefStructurePairCollection coll)
        {
            return new BaseCardStructure(coll, this);
        }
    }
}
