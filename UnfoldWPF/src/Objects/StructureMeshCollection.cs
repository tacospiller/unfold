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
        public IStructure Structure { get; set; }
        public bool Selected { get; set; } = false;
        public StructureMesh Mesh { get; set; }
    }

    public class StructureMeshCollection : IStructureCache
    {
        public Dictionary<StructureId, DefStructureVisiblePair> Children { get; } = new Dictionary<StructureId, DefStructureVisiblePair>();

        public DefStructureVisiblePair[] Freeze()
        {
            return Children.Values.ToArray();
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
                var structure = StructureBuilder.BuildStructure(this, pair.Value.Def);
                pair.Value.Mesh = new StructureMesh(structure, DisplayHelper.RandomColor(), DisplayHelper.RandomColor());
            }
        }

        public void UpdateManualSlider(StructureId id, double value)
        {
            var child = Children[id];
            var rot = (child.Structure as RotatingStructure);
            if (rot != null)
            {
                var manualAxis = rot.Axis as ManualAxis;
                manualAxis?.SetAngle(value);
            }
            Recalculate();
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
