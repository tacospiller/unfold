using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Unfold.UnfoldGeometry;
using Unfold.UnfoldWPF;

namespace Unfold.Pages
{
    public partial class Preview3D : Page
    {
        public Preview3D()
        {
            Content = InitializeViewport();
        }

        private Viewport3D InitializeViewport()
        {
            var viewport = new Viewport3D();
            viewport.Camera = GetDefaultCamera();
            viewport.Children.Add(GetDefaultVisual3D());
            return viewport;
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

            var collection = StructureMeshCollection.CreateFromJson("""
                {
                    "ChildrenList": [
                        {
                            "$type": "BaseCard",
                            "Id": { "Id": "baseCard" },
                            "Width": 148,
                            "Height": 210,
                            "Axis": {
                                "Type": "Manual"
                            }
                        }
                    ]
                }
                """);

            collection.Children.ForEach(x => group.Children.Add(x.Model3D));

            var inv = false;
            var angle = 0.0;
            CompositionTarget.Rendering += (_, _) =>
            {
                if (angle < 0.02)
                {
                    inv = false;

                }
                if (angle >= Math.PI - 0.02)
                {
                    inv = true;
                }

                angle += (inv ? -0.02 : 0.02);
                ((ManualAxis)((BaseCardStructure)collection.Children[0].Structure).Axis).SetAngle(angle);
                collection.Children.ForEach(x => x.Recalculate());

            };

            return group;
        }

        private static void AddAxis(Model3DGroup group)
        {
            var axisR = 0.5;
            var xAxis = new GeometryModel3D()
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

            var yAxis = new GeometryModel3D()
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

            var zAxis = new GeometryModel3D()
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


            group.Children.Add(xAxis);
            group.Children.Add(yAxis);
            group.Children.Add(zAxis);
        }
    }
}
