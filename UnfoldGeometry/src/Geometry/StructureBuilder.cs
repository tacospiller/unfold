using Unfold.Serialization;

namespace Unfold.UnfoldGeometry
{
    public static class StructureBuilder
    {
        public static IAxis? BuildAxis(IStructureCache cache, AxisDef def) {
            if (def.Type == AxisDef.AxisTypes.Manual || def.DependantProperties == null)
            {
                return new ManualAxis();
            }
            var str = cache.GetStructure(def.DependantProperties.ParentStructureId);
            var axis = str.GetAxis(def.DependantProperties.AxisDescriptor);
            return axis.OffsetY(def.DependantProperties.OffsetY);
        }

        public static IStructure? BuildStructure(IStructureCache cache, IStructureDef def)
        {
            switch(def)
            {
                case BaseCardDef bdef:
                    return new BaseCardStructure(cache, bdef);
                case SymmetricParallelogramDef spdef:
                    return new SymmetricParallelStructure(cache, spdef);
                case SymmetricVFoldDef svdef:
                    return new SymmetricVFoldStructure(cache, svdef);
                default:
                    return null;
            }
        }
    }
}
