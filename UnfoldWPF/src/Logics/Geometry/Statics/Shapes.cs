using System;
using System.Windows.Media.Media3D;

namespace Unfold.UnfoldGeometry
{
    static internal class Shapes
    {
        static Geometry3D? _cylinder = null;
        public static Geometry3D Cylinder
        {
            get
            {
                {
                    if (_cylinder == null)
                    {
                        var geometry = new MeshGeometry3D();
                        for (var i = (double)0; i < Math.PI * 2; i += Math.PI / 4)
                        {
                            var deg = i;
                            var deg2 = i + 1;
                            geometry.Positions.Add(new Point3D(Math.Sin(deg), Math.Cos(deg), 1));
                            geometry.Positions.Add(new Point3D(Math.Sin(deg2), Math.Cos(deg2), 1));
                            geometry.Positions.Add(new Point3D(Math.Sin(deg2), Math.Cos(deg2), -1));
                            geometry.Positions.Add(new Point3D(Math.Sin(deg2), Math.Cos(deg2), -1));
                            geometry.Positions.Add(new Point3D(Math.Sin(deg), Math.Cos(deg), -1));
                            geometry.Positions.Add(new Point3D(Math.Sin(deg), Math.Cos(deg), 1));

                        }

                        _cylinder = geometry;
                    }
                    return _cylinder;
                }
            }
        }
    }
}
