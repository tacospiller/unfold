using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

namespace UnfoldGeometry.Serialization
{
    public record class BaseCardDef : IStructureDef
    {
        public StructureId Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public AxisDef Axis { get; set; }


        private IStructure? _structure;
        public IStructure GetStructure(IStructureDefCollection coll)
        {
            if (_structure == null)
            {
                _structure = new BaseCardStructure(coll, this);
            }
            return _structure;
        }
    }
}
