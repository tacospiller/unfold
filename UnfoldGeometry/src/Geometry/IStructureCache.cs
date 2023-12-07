using Unfold.Serialization;

namespace Unfold.UnfoldGeometry
{
    public interface IStructureCache
    {
        IStructure GetStructure(StructureId structureId);
    }
}
