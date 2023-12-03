﻿using System;
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
            var p = new Point3D(2,2,5);
            var o = new Point3D(0, 1, 0);
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

            var baseAxis = new ManualAxis();
            var baseCard = new BaseCardStructure(baseAxis) { Height = 2, Width = 1.5 };
            var vfold = new SymmetricVFoldStructure(((IAxis)baseAxis).OffsetY(0.6)) { Theta = Angles.Deg45, Psi = Angles.Deg45 };
            var par = new SymmetricParallelStructure(vfold.AOuterAxis.OffsetY(0.5)) { DistFromAxis = 0.3, Width = 0.3, Height = 0.4 };
            var vfold2 = new SymmetricVFoldStructure(vfold.COuterAxis.OffsetY(0.3)) { Theta = Angles.Deg30, Psi = Angles.Deg60, DistA = 0.5, DistD = 0.5 };
            var vfold3 = new SymmetricVFoldStructure(vfold2.COuterAxis.OffsetY(0.1)) { Theta = Angles.Deg30, Psi = Angles.Deg30, DistA = 0.3, DistD = 0.3 };
            var vfold4 = new SymmetricVFoldStructure(par.AOuterFold) { Theta = Angles.Deg45, Psi = Angles.Deg45, DistA = 0.4, DistD = 0.4 };
            var face = new FaceStructure(vfold4.FaceCBD.Offset(0.1, 0, 0.001)) { Width = 0.3, Height = 0.3 };

            var baseCardModel = new StructureMesh(baseCard, Colors.DeepPink, Colors.DeepSkyBlue);
            var vfoldModel = new StructureMesh(vfold, Colors.DarkViolet, Colors.DarkTurquoise);
            var parlModel = new StructureMesh(par, Colors.Beige, Colors.DarkGray);
            var vfoldModel2 = new StructureMesh(vfold2, Colors.LightSteelBlue, Colors.DarkOrange);
            var vfoldModel3 = new StructureMesh(vfold3, Colors.ForestGreen, Colors.RosyBrown);
            var vfoldModel4 = new StructureMesh(vfold4, Colors.DarkGray, Colors.DeepPink);
            var faceModel = new StructureMesh(face, Colors.IndianRed, Colors.LightGreen);

            group.Children.Add(baseCardModel.Model3D);
            group.Children.Add(vfoldModel.Model3D);
            group.Children.Add(parlModel.Model3D);
            group.Children.Add(vfoldModel2.Model3D);
            group.Children.Add(vfoldModel3.Model3D);
            group.Children.Add(vfoldModel4.Model3D);
            group.Children.Add(faceModel.Model3D);

            var inv = false;
            CompositionTarget.Rendering += (_, _) =>
            {
                baseAxis.SetAngle(baseAxis.Angle + (!inv ? 0.02 : -0.02));
                if (baseAxis.Angle < 0.2)
                {
                    inv = false;
                }
                if (baseAxis.Angle > Math.PI - 0.2)
                {
                    inv = true;
                }

                baseCardModel.Recalculate();
                vfoldModel.Recalculate();
                parlModel.Recalculate();
                vfoldModel2.Recalculate();
                vfoldModel3.Recalculate();
                vfoldModel4.Recalculate();
                faceModel.Recalculate();
            };

            return group;
        }

        private static void AddAxis(Model3DGroup group)
        {
            var axisR = 0.005;
            var xAxis = new GeometryModel3D()
            {
                Geometry = Shapes.Cylinder(10).ToGeometry3D(),
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Red)),
                Transform = new Transform3DGroup
                {
                    Children = new Transform3DCollection(new Transform3D[] {
                        new ScaleTransform3D(new Vector3D(axisR, axisR, 100)),
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
                    new ScaleTransform3D(new Vector3D(axisR, axisR, 100)),
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
                    new ScaleTransform3D(new Vector3D(axisR, axisR, 100)),
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