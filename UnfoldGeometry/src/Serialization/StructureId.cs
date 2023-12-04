using System.Text.Json.Serialization;

namespace UnfoldGeometry.Serialization
{
    public record struct StructureId
    {
        public string Id { get; }
        [JsonConstructor]
        public StructureId(string id) { Id = id; }
    }
}
