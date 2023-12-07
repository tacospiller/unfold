using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Unfold.UnfoldWPF;
using Unfold.Serialization;
using Unfold.UnfoldGeometry;

namespace UnfoldWPF.Objects
{
    public record class DefStructureVisiblePair
    {
        public IStructureDef Def { get; set; }
        public IStructure? Structure { get; set; }
        private bool _selected = false;
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected != value)
                {
                    if (value)
                    {
                        Mesh.UpdateColor(Colors.IndianRed, Colors.DarkOrange);
                    } else
                    {
                        Mesh.UpdateColor(DisplayHelper.RandomColor(), DisplayHelper.RandomColor());
                    }
                }
                _selected = value;
            }
        }
        public StructureMesh? Mesh { get; set; }
        public DefStructureVisiblePair DeepCopy()
        {
            return new DefStructureVisiblePair
            {
                Def = Def,
                Structure = Structure,
                Mesh = Mesh,
                Selected = false
            };
        }
    }

    public class StructureMeshCollection : IStructureCache
    {
        public Dictionary<StructureId, DefStructureVisiblePair> Children { get; } = new Dictionary<StructureId, DefStructureVisiblePair>();

        public DefStructureVisiblePair[] Freeze()
        {
            return Children.Values.Select(x => x.DeepCopy()).ToArray();
        }

        public IStructure GetStructure(StructureId structureId)
        {
            var pair = Children[structureId];
            if (pair != null)
            {
                pair.Structure ??= StructureBuilder.BuildStructure(this, pair.Def);
                return pair.Structure;
            }
            return null;
        }

        public void ReloadFromDefs(List<IStructureDef> defs)
        {
            Children.Clear();
            foreach (var def in defs)
            {
                Children.Add(def.Id, new DefStructureVisiblePair { Def = def });
            }

            RecreateAllMeshes();
        }

        public void RecreateAllMeshes()
        {
            foreach (var pair in Children)
            {
                pair.Value.Structure = null;
                pair.Value.Mesh = null;
            }
            foreach (var pair in Children)
            {
                var structure = GetStructure(pair.Value.Def.Id);
                pair.Value.Mesh = new StructureMesh(structure, DisplayHelper.RandomColor(), DisplayHelper.RandomColor());
            }
        }

        public (Point3D, double) GetCenterAndRange()
        {
            Point3D maxPoint = Children.SelectMany(x => x.Value.Mesh.Mesh.Positions).MaxBy(x => (x - new Point3D(0,0,0)).Length);
            Point3D minPoint = Children.SelectMany(x => x.Value.Mesh.Mesh.Positions).MaxBy(x => (x - maxPoint).Length);

            return ((Point3D)(((Vector3D)maxPoint + (Vector3D)minPoint) / 2), (maxPoint - minPoint).Length / 2);
        }

        public void Recalculate()
        {
            foreach (var child in Children)
            {
                child.Value.Mesh.Recalculate();
            }
        }
    }
}
