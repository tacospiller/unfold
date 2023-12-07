using Unfold.UnfoldGeometry;

namespace Unfold.Serialization
{
    public record class SymmetricParallelogramDef : IRotatingStructureDef
    {
        public StructureId Id { get; set; }
        public AxisDef Axis { get; set; }
        public double DistFromAxis { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        public static class AxisDescriptors
        {
            public static AxisDescriptor AOuter = new AxisDescriptor("SymmetricParalellogram.AOuter");
            public static AxisDescriptor BOuter => new AxisDescriptor("SymmetricParalellogram.BOuter");
        }

        public AxisDescriptor[] GetAllAxisDescriptors()
        {
            return new AxisDescriptor[] { 
                IRotatingStructureDef.AxisDescriptors.BaseAxis, 
                AxisDescriptors.AOuter, 
                AxisDescriptors.BOuter 
            };
        }
    }
}
