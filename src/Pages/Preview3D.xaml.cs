using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Unfold.UnfoldGeometry;

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
            var p = new Point3D(1,2,5);
            var o = new Point3D(0, 0, 0);
            var camera = new PerspectiveCamera(p, o - p, new Vector3D(0,1,0), 60 );
      
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

            var vfold = new StructureMesh();
            group.Children.Add(vfold.Model3D);

            var inv = false;
            CompositionTarget.Rendering += (_, _) =>
            {
                vfold.Structure.FoldAngle += inv ? -0.05 : 0.05;
                vfold.Recalculate();
                if (vfold.Structure.FoldAngle < 0.2)
                {
                    inv = false;
                }
                if (vfold.Structure.FoldAngle > vfold.Structure.MaxAngle - 0.2)
                {
                    inv = true;
                }
            };

            return group;
        }

        private static void AddAxis(Model3DGroup group)
        {
            var axisR = 0.005;
            var xAxis = new GeometryModel3D()
            {
                Geometry = Shapes.Cylinder,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Red)),
                Transform = new ScaleTransform3D(new Vector3D(axisR, axisR, 100))
            };

            var yAxis = new GeometryModel3D()
            {
                Geometry = Shapes.Cylinder,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Blue)),
                Transform = new Transform3DGroup
                {
                    Children = new Transform3DCollection(new Transform3D[] {
                    new ScaleTransform3D(new Vector3D(axisR, axisR, 100)),
                    new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90))
                })
                }
            };

            var zAxis = new GeometryModel3D()
            {
                Geometry = Shapes.Cylinder,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Green)),
                Transform = new Transform3DGroup
                {
                    Children = new Transform3DCollection(new Transform3D[] {
                    new ScaleTransform3D(new Vector3D(axisR, axisR, 100)),
                    new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90))
                })
                }
            };


            group.Children.Add(xAxis);
            group.Children.Add(yAxis);
            group.Children.Add(zAxis);
        }
    }
}
