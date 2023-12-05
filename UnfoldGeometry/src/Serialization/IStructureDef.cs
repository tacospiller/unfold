using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

namespace Unfold.Serialization
{
    [JsonDerivedType(typeof(BaseCardDef), "BaseCard")]
    [JsonDerivedType(typeof(SymmetricParallelogramDef), "SymmetricParallelogram")]
    [JsonDerivedType(typeof(SymmetricVFoldDef), "SymmetricVFold")]
    public interface IStructureDef
    {
        public StructureId Id { get; }
        public IStructure CreateStructure(DefStructurePairCollection coll);
    }
}
