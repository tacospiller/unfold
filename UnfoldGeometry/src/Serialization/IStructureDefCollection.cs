namespace UnfoldGeometry.Serialization
{
    public interface IStructureDefCollection
    {
        public IReadOnlyDictionary<StructureDefKey, IStructureDef> Children { get; }
    }
}
