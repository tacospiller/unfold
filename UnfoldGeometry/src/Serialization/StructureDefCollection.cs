using System.Text.Json.Serialization;

namespace UnfoldGeometry.Serialization
{
    public class StructureDefCollection : IStructureDefCollection
    {
        [JsonIgnore]
        public IReadOnlyDictionary<StructureId, IStructureDef> Children { get; }
        public IEnumerable<IStructureDef> ChildrenList => Children.Values;

        [JsonConstructor]
        public StructureDefCollection(IEnumerable<IStructureDef> childrenList)
        {
            Children = childrenList.ToDictionary(x => x.Id);
        }
    }
}
