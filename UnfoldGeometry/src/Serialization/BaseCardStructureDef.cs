using Unfold.UnfoldGeometry;

namespace UnfoldGeometry.Serialization
{
    public class BaseCardStructureDef: IStructureDef
    {
        public StructureDefKey Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public AxisDef? AxisDef { get; set; }

        private IStructure? _structure;
        public IStructure GetStructure(IStructureDefCollection coll)
        {
            if (_structure == null)
            {
                _structure = new BaseCardStructure(AxisDef?.ToAxis(coll) ?? new ManualAxis()) { Width = Width, Height = Height };
            }
            return _structure;
        }
    }
}
