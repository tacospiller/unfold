namespace UnfoldGeometry.Serialization
{
    public interface IStructureDefCollection
    {
        public IReadOnlyDictionary<StructureId, IStructureDef> Children { get; }
    }
}
