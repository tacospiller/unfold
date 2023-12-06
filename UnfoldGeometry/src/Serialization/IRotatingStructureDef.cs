namespace Unfold.Serialization
{
    public interface IRotatingStructureDef : IStructureDef
    {
        AxisDef Axis { get; }
        AxisDescriptor[] GetAllAxisDescriptors();
        
        public static class AxisDescriptors
        {
            public static AxisDescriptor BaseAxis = new AxisDescriptor("RotatingStructure.BaseAxis");
        }

    }
}
