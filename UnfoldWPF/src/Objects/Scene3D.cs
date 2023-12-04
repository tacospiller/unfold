using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Unfold.UnfoldGeometry;

namespace Unfold.UnfoldWPF
{
    public class Scene3D
    {
        public Viewport3D Viewport { get; set; }
        public Camera Camera { get; set; }
        public GeometryModel3D XAxis { get; set; }
        public GeometryModel3D YAxis { get; set; }
        public GeometryModel3D ZAxis { get; set; }

        public Scene3D()
        {
            Viewport = new Viewport3D();
            Camera = GetDefaultCamera();

            Viewport.Camera = Camera;
            Viewport.Children.Add(GetDefaultVisual3D());
        }

        private Camera GetDefaultCamera()
        {
            var p = new Point3D(200, 200, 500);
            var o = new Point3D(0, 100, 0);
            var camera = new PerspectiveCamera(p, o - p, new Vector3D(0, 1, 0), 60);

            return camera;
        }

        private ModelVisual3D GetDefaultVisual3D()
        {
            var group = GetDefault3DGroup();

            var visual = new ModelVisual3D();
            visual.Content = group;
            return visual;
        }

        private Model3DGroup GetDefault3DGroup()
        {
            var group = new Model3DGroup();
            group.Children.Add(new AmbientLight(Colors.Gray));
            group.Children.Add(new DirectionalLight(Colors.White, new Vector3D(0, -1, -1)));

            AddAxis(group);
            return group;
        }

        private void AddAxis(Model3DGroup group)
        {
            var axisR = 0.5;
            XAxis = new GeometryModel3D()
            {
                Geometry = Shapes.Cylinder(10).ToGeometry3D(),
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Red)),
                Transform = new Transform3DGroup
                {
                    Children = new Transform3DCollection(new Transform3D[] {
                        new ScaleTransform3D(new Vector3D(axisR, axisR, 300)),
                        new TranslateTransform3D(0, 0, -50)
                    })
                }
            };

            YAxis = new GeometryModel3D()
            {
                Geometry = Shapes.Cylinder(10).ToGeometry3D(),
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Blue)),
                Transform = new Transform3DGroup
                {
                    Children = new Transform3DCollection(new Transform3D[] {
                    new ScaleTransform3D(new Vector3D(axisR, axisR, 300)),
                    new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)),
                    new TranslateTransform3D(0, 50, 0)
                })
                }
            };

            ZAxis = new GeometryModel3D()
            {
                Geometry = Shapes.Cylinder(10).ToGeometry3D(),
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Green)),
                Transform = new Transform3DGroup
                {
                    Children = new Transform3DCollection(new Transform3D[] {
                    new ScaleTransform3D(new Vector3D(axisR, axisR, 300)),
                    new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90)),
                    new TranslateTransform3D(-50, 0, 0)
                })
                }
            };


            group.Children.Add(XAxis);
            group.Children.Add(YAxis);
            group.Children.Add(ZAxis);
        }
    }
}
