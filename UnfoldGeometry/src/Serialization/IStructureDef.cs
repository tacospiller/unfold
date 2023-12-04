using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

namespace UnfoldGeometry.Serialization
{
    [JsonDerivedType(typeof(BaseCardDef), "BaseCard")]
    [JsonDerivedType(typeof(SymmetricParallelogramDef), "SymmetricParallelogram")]
    [JsonDerivedType(typeof(SymmetricVFoldDef), "SymmetricVFold")]
    public interface IStructureDef
    {
        public StructureId Id { get; }
        public IStructure GetStructure(IStructureDefCollection coll);
    }
}
