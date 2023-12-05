using Unfold.Serialization;

namespace Unfold.UnfoldGeometry
{
    public record class DefStructurePair
    {
        public IStructureDef Def { get; set; }
        public IStructure? Structure { get; set; }
        public IStructure? GetStructure(DefStructurePairCollection coll)
        {
            if (Structure == null)
            {
                Structure = Def.CreateStructure(coll);
            }
            return Structure;
        }

        public void RecreateStructure(DefStructurePairCollection coll)
        {
            Structure = Def.CreateStructure(coll);
        }
    }

    public class DefStructurePairCollection
    {
        protected virtual IDictionary<StructureId, DefStructurePair> _children { get; } = new Dictionary<StructureId, DefStructurePair>();

        public IEnumerable<IStructureDef> GetDefs()
        {
            return _children.Values.Select(x => x.Def);
        }

        public void AddRange(IEnumerable<DefStructurePair> defs)
        {
            foreach (var def in defs)
            {
                _children.Add(def.Def.Id, def);
            }
        }

        public DefStructurePair GetChild(StructureId id)
        {
            return _children[id];
        }
    }
}
