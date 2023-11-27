using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Unfold.UnfoldGeometry
{
    public class StructureMesh
    {
        public IStructure Structure { get; init; }
        public GeometryModel3D Model3D { get; init; }

        private MeshGeometry3D _mesh;

        public StructureMesh()
        {
            Structure = Structure ?? new SymmetricParallelStructure();
            _mesh = new MeshGeometry3D();
            Model3D = Model3D ?? new GeometryModel3D
            {
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.DeepPink)),
                BackMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.DeepSkyBlue)),
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
