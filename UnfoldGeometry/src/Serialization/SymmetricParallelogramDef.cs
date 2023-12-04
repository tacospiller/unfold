using Unfold.UnfoldGeometry;

namespace UnfoldGeometry.Serialization
{
    public record class SymmetricParallelogramDef : IStructureDef
    {
        public StructureId Id { get; set; }
        public AxisDef Axis { get; set; }
        public double DistFromAxis { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        public SymmetricParallelogramDef(StructureId id, AxisDef axis, double distFromAxis, double height, double width)
        {
            Id = id;
            Axis = axis;
            DistFromAxis = distFromAxis;
            Height = height;
            Width = width;
        }

        private IStructure? _structure;
        public IStructure GetStructure(IStructureDefCollection coll)
        {
            if (_structure == null)
            {
                _structure = new SymmetricParallelStructure(coll, this);
            }
            return _structure;
        }
    }
}
