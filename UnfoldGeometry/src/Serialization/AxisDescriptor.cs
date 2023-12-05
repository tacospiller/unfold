using System.Text.Json.Serialization;

namespace Unfold.Serialization
{
    public record struct AxisDescriptor
    {
        public string Id { get; }
        [JsonConstructor]
        public AxisDescriptor(string id) { Id = id; }
    }
}
