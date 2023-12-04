using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Unfold.UnfoldGeometry;
using UnfoldGeometry.Serialization;

namespace Unfold.UnfoldWPF
{
    public class StructureMesh
    {
        public IStructure Structure { get; }
        public GeometryModel3D Model3D { get; }

        private MeshGeometry3D _mesh;

        public StructureMesh(IStructure structure, Color face, Color back)
        {
            Structure = structure;
            _mesh = new MeshGeometry3D();
            Model3D = Model3D ?? new GeometryModel3D
            {
                Material = new DiffuseMaterial(new SolidColorBrush(face)),
                BackMaterial = new DiffuseMaterial(new SolidColorBrush(back)),
            };
            Model3D.Geometry = _mesh;

            Recalculate();
        }

        public void Recalculate()
        {
            _mesh.Positions = new Point3DCollection(Structure.CalculateFaces().Select(x => x.ToPoint3D()));
        }
    }

    public class StructureMeshCollection
    {
        public List<StructureMesh> Children { get; } = new List<StructureMesh>();

        public static StructureMeshCollection CreateFromJson(string json)
        {
            var coll = JsonSerializer.Deserialize<StructureDefCollection>(json);
            var meshes = new StructureMeshCollection();
            meshes.Children.AddRange(coll.ChildrenList.Select(x => new StructureMesh(x.GetStructure(coll), Colors.FloralWhite, Colors.DimGray)));
            return meshes;
        }
    }
}
