using Unfold.UnfoldGeometry;

namespace Unfold.Serialization
{
    public record class SymmetricParallelogramDef : IStructureDef
    {
        public StructureId Id { get; set; }
        public AxisDef Axis { get; set; }
        public double DistFromAxis { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }


        public IStructure CreateStructure(DefStructurePairCollection coll)
        {
            return new SymmetricParallelStructure(coll, this);
        }
    }
}
