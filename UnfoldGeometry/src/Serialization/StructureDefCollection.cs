using System.Text.Json.Serialization;
using Unfold.UnfoldGeometry;

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

        public IEnumerable<ManualAxis> GetManualAxes()
        {
            // bad impl
            var list = new List<ManualAxis>();
            foreach (var child in ChildrenList)
            {
                var rot = child.GetStructure(this) as RotatingStructure;
                if (rot != null)
                {
                    var manualAxis = rot.Axis as ManualAxis;
                    if (manualAxis != null)
                    {
                        list.Add(manualAxis);
                    }
                }
            }

            return list;
        }
    }
}
