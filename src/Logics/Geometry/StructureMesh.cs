using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Unfold.UnfoldGeometry
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
            _mesh.Positions = new Point3DCollection(Structure.Faces.Select(x => x.ToPoint3D()));
        }
    }
}
