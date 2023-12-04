using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Unfold.UnfoldWPF;
using UnfoldGeometry.Serialization;

namespace UnfoldWPF.Objects
{
    public class StructureMeshCollection
    {
        public List<StructureMesh> Children { get; } = new List<StructureMesh>();

        public static StructureMeshCollection CreateFromJson(string json)
        {
            var coll = JsonSerializer.Deserialize<StructureDefCollection>(json);
            return CreateFromDefs(coll);
        }

        public static StructureMeshCollection CreateFromDefs(StructureDefCollection coll)
        {
            var meshes = new StructureMeshCollection();
            meshes.Children.AddRange(coll.ChildrenList.Select(x => new StructureMesh(x.GetStructure(coll), DisplayHelper.RandomColor(), DisplayHelper.RandomColor())));
            return meshes;
        }

        public (Point3D, double) GetCenterAndRange()
        {
            Point3D maxPoint = Children.SelectMany(x => x.Mesh.Positions).MaxBy(x => (x - new Point3D(0,0,0)).Length);
            Point3D minPoint = Children.SelectMany(x => x.Mesh.Positions).MaxBy(x => (x - maxPoint).Length);

            return ((Point3D)(((Vector3D)maxPoint + (Vector3D)minPoint) / 2), (maxPoint - minPoint).Length / 2);
        }

        public void Recalculate()
        {
            Children.ForEach(x => x.Recalculate());
        }
    }
}
