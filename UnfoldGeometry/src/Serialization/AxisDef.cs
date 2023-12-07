using System.Numerics;
using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

namespace Unfold.Serialization
{
    public record class AxisDef
    {
        [JsonConverter(typeof(JsonStringEnumConverter<AxisTypes>))]
        public enum AxisTypes
        {
            Manual,
            Dependant
        }

        public record class DependantAxisProperties
        {
            public StructureId ParentStructureId { get; set; }
            public AxisDescriptor AxisDescriptor { get; set; }
            public double OffsetY { get; set; }
        }

        public StructureId Id { get; set; }
        public AxisTypes Type { get; set; }
        public DependantAxisProperties? DependantProperties { get; set; }
    }
}
