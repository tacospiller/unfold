namespace Unfold.Serialization
{
    public interface IRotatingStructureDef : IStructureDef
    {
        AxisDef Axis { get; }
        public static class AxisDescriptors
        {
            public static AxisDescriptor BaseAxis = new AxisDescriptor("RotatingStructure.BaseAxis");
        }

    }
}
