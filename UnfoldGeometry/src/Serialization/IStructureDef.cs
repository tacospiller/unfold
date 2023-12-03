using Unfold.UnfoldGeometry;

namespace UnfoldGeometry.Serialization
{
    public interface IStructureDef
    {
        public StructureDefKey Id { get; }
        public IStructure GetStructure(IStructureDefCollection coll);
    }
}
