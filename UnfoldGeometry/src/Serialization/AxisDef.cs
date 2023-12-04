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
            public StructureId ParentStructureId { get; set; }
            public AxisDescriptor AxisDescriptor { get; set; }
            public double OffsetY { get; set; }
        }

        public AxisTypes Type { get; set; }
        public DependantAxisProperties? DependantProperties { get; set; }


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
                var obj = coll.GetChild(DependantProperties.ParentStructureId);
                if (obj == null)
                {
                    return null;
                }
                var axis = obj.GetStructure(coll)?.GetAxis(DependantProperties.AxisDescriptor);
                if (DependantProperties.OffsetY != 0)
                {
                    return axis?.OffsetY(DependantProperties.OffsetY);
                }
                return axis;
            }
        }
    }
}
