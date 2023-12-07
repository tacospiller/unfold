using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Unfold.UnfoldGeometry;
using Unfold.UnfoldWPF;

namespace UnfoldWPF.Objects
{
    public class StructureMesh
    {
        public IStructure Structure { get; }
        public GeometryModel3D Model3D { get; }

        public MeshGeometry3D Mesh { get; }

        public StructureMesh(IStructure structure, Color face, Color back)
        {
            Structure = structure;
            Mesh = new MeshGeometry3D();
            Model3D = Model3D ?? new GeometryModel3D
            {
                Material = new DiffuseMaterial(new SolidColorBrush(face)),
                BackMaterial = new DiffuseMaterial(new SolidColorBrush(back)),
            };
            Model3D.Geometry = Mesh;

            Recalculate();
        }

        public void Recalculate()
        {
            Mesh.Positions = new Point3DCollection(Structure.CalculateFaces().Select(x => x.ToPoint3D()));
        }

        public void UpdateColor(Color face, Color back)
        {
            ((Model3D.Material as DiffuseMaterial).Brush as SolidColorBrush).Color = face;
            ((Model3D.BackMaterial as DiffuseMaterial).Brush as SolidColorBrush).Color = back;
        }
    }
}
