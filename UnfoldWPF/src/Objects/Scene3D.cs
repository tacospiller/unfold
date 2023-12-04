using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Unfold.UnfoldGeometry;
using Unfold.UnfoldWPF;

namespace UnfoldWPF.Objects
{
    public class Scene3D
    {
        public Viewport3D Viewport { get; set; }
        public PerspectiveCamera Camera { get; set; }
        public GeometryModel3D XAxis { get; set; }
        public GeometryModel3D YAxis { get; set; }
        public GeometryModel3D ZAxis { get; set; }
        public Model3DGroup Group { get; set; }

        public StructureMeshCollection? Collection { get; set; }

        public Scene3D()
        {
            Viewport = new Viewport3D();
            Camera = GetDefaultCamera();
            Group = GetDefault3DGroup();

            Viewport.Camera = Camera;
            Viewport.Children.Add(GetDefaultVisual3D());

            SetScene(new Point3D(0, 0, 0), 10);
        }

        public void LoadComponents(StructureMeshCollection coll)
        {
            Collection?.Children.ForEach(x =>
            {
                Group.Children.Remove(x.Model3D);
            });
            Collection = coll;
            coll?.Children.ForEach(x => Group.Children.Add(x.Model3D));
            var (center, range) = coll.GetCenterAndRange();
            SetScene(center, range);
        }

        public void SetScene(Point3D center, double range)
        {
            var pos = new Point3D(center.X + (range * 2), center.Y + (range / 2), center.Z + (range * 3));
            Camera.Position = pos;
            Camera.LookDirection = center - pos;

            SetAxis(range);
        }

        private void SetAxis(double range)
        {
            var axisR = range / 100;

            XAxis.Transform = new Transform3DGroup
            {
                Children = new Transform3DCollection(new Transform3D[] {
                        new ScaleTransform3D(new Vector3D(axisR, axisR, range * 100)),
                        new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90)),
                    new TranslateTransform3D(range * -50, 0, 0)

                    })
            };

            YAxis.Transform = new Transform3DGroup
            {
                Children = new Transform3DCollection(new Transform3D[] {
                    new ScaleTransform3D(new Vector3D(axisR, axisR, range * 100)),
                    new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)),
                    new TranslateTransform3D(0, range * 50, 0)
                })
            };

            ZAxis.Transform = new Transform3DGroup
            {
                Children = new Transform3DCollection(new Transform3D[] {
                    new ScaleTransform3D(new Vector3D(axisR, axisR, range * 100)),
                        new TranslateTransform3D(0, 0, range * -50)
                })
            };
        }

        private PerspectiveCamera GetDefaultCamera()
        {
            var p = new Point3D(2, 2, 5);
            var o = new Point3D(0, 1, 0);
            var camera = new PerspectiveCamera(p, o - p, new Vector3D(0, 1, 0), 60);

            return camera;
        }

        private ModelVisual3D GetDefaultVisual3D()
        {
            var visual = new ModelVisual3D();
            visual.Content = Group;
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
