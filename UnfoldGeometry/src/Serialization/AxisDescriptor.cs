namespace UnfoldGeometry.Serialization
{
    public record struct AxisDescriptor
    {
        public string Id { get; }
        public AxisDescriptor(string id) { Id = id; }
    }
}
