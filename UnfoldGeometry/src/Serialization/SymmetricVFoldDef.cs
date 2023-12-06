using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

namespace Unfold.Serialization
{
    public record class SymmetricVFoldDef : IRotatingStructureDef
    {
        public StructureId Id { get; set; }
        public AxisDef Axis { get; set; }
        public double DistA { get; set; }
        public double DistD { get; set; }
        public double Theta { get; set; }
        public double Psi { get; set; }
        public static class AxisDescriptors
        {
            public static AxisDescriptor AOuter = new AxisDescriptor("SymmetricVFold.AOuter");
            public static AxisDescriptor COuter => new AxisDescriptor("SymmetricVFold.COuter");
            public static AxisDescriptor AInner = new AxisDescriptor("SymmetricVFold.AInner");
            public static AxisDescriptor CInner = new AxisDescriptor("SymmetricVFold.CInner");
        }
    }
}
