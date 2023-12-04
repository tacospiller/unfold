using System.Numerics;
using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

namespace UnfoldGeometry.Serialization
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
            public StructureId ParentStructureId { get; }
            public AxisDescriptor AxisDescriptor { get; }
            public Vector3 Offset { get; }

            [JsonConstructor]
            public DependantAxisProperties(StructureId parentStructureId, AxisDescriptor axisDescriptor, Vector3 offset)
            {
                ParentStructureId = parentStructureId;
                AxisDescriptor = axisDescriptor;
                Offset = offset;
            }
        }

        public AxisTypes Type { get; }
        public DependantAxisProperties? DependantProperties { get; }

        public AxisDef(AxisTypes type, DependantAxisProperties? dependantProperties)
        {
            Type = type;
            DependantProperties = dependantProperties;
        }

        public IAxis? ToAxis(IStructureDefCollection coll)
        {
            if (Type == AxisTypes.Manual)
            {
                return new ManualAxis();
            }
            else if (DependantProperties == null)
            {
                return null;
            }
            else
            {
                var obj = coll.Children.GetValueOrDefault(DependantProperties.ParentStructureId);
                if (obj == null)
                {
                    return null;
                }
                var axis = obj.GetStructure(coll)?.GetAxis(DependantProperties.AxisDescriptor);
                if (DependantProperties.Offset != Vector3.Zero)
                {
                    return axis?.OffsetY(DependantProperties.Offset.Y);
                }
                return axis;
            }
        }
    }
}
