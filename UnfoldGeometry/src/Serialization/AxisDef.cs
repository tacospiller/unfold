using Unfold.UnfoldGeometry;

namespace UnfoldGeometry.Serialization
{
    public class AxisDef
    {
        public StructureDefKey? ParentStructure { get; set; }
        public AxisDescriptor? ParentAxisDescriptor { get; set; }
        public bool Manual { get; set; }
        public double ManualInitialAngle { get; set; } = 0;
        public double OffsetY { get; set; } = 0;
        public AxisDef()
        { }

        public IAxis ToAxis(IStructureDefCollection coll)
        {
            if (Manual)
            {
                return new ManualAxis(ManualInitialAngle);
            } else if (ParentStructure != null && ParentAxisDescriptor != null)
            {
                var strct = coll.Children.GetValueOrDefault((StructureDefKey)ParentStructure);
                if (strct == null)
                {
                    throw new SerializationException("invalid axisdef");
                }
                var axis = strct.GetStructure(coll).GetAxis((AxisDescriptor)ParentAxisDescriptor);
                if (axis == null)
                {
                    throw new SerializationException("invalid axisdef");
                }
                return OffsetY == 0 ? axis : axis.OffsetY(OffsetY);
            } else
            {
                throw new SerializationException("invalid axisdef");
            }
        }
    }
}
