namespace UnfoldGeometry.Serialization
{
    public record struct StructureDefKey
    {
        public string Id { get; }
        public StructureDefKey(string id) { Id = id; }
    }
}
