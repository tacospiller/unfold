using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

namespace UnfoldGeometry.Serialization
{
    public record class BaseCardDef : IStructureDef
    {
        public StructureId Id { get; }
        public double Width { get; }
        public double Height { get; }
        public AxisDef Axis { get; }

        [JsonConstructor]
        public BaseCardDef(StructureId id, double width, double height, AxisDef axis)
        {
            Id = id;
            Width = width;
            Height = height;
            Axis = axis;
        }

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
