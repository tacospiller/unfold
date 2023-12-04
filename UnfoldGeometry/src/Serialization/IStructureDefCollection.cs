namespace UnfoldGeometry.Serialization
{
    public interface IStructureDefCollection
    {
        List<IStructureDef> ChildrenList { get; }
        IStructureDef? GetChild(StructureId id);
    }
}
