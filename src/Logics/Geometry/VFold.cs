using System;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Media.Media3D;

namespace Unfold.UnfoldGeometry
{
    public class VFold
    {
        private double _theta = Angles.Deg30;
        private double _psi = Angles.Deg45;
        private double _alpha = Angles.Deg0;
        private double _ao = 1;
        private double _co = 1;
        private Matrix3D _transform = Matrix3D.Identity;

        public double Theta
        {
            get => _theta;
            set
            {
                if (value == 0)
                {
                    throw new Exception("Theta cannot be 0");
                }
                _theta = value;
                CalculateGeometry();
            }
        } // angle between glue line and Y axis 
        public double Psi
        {
            get => _psi;
            set
            {
                if (value == 0)
                {
                    throw new Exception("Psi cannot be 0, use Parallelogram for the purpose");
                }
                _psi = value;
                CalculateGeometry();
            }
        } // angle AOC = angle of the triangle
        public double Alpha { get => _alpha; set { _alpha = value; CalculateGeometry(); } } // angle AOB = folding angle
        public double AO { get => _ao; set { _ao = value; CalculateGeometry(); } } // distance AO = BO
        public double CO { get => _co; set { _co = value; CalculateGeometry(); } } // distance CO
        public Matrix3D Transform { get => _transform; set { _transform = value; CalculateGeometry(); } }


        private readonly MeshGeometry3D _geo;
        public Geometry3D Geometry => _geo;

        public VFold()
        {
            _geo = new MeshGeometry3D();
            CalculateGeometry();
        }

        public double TempAngle()
        {
            Plane plane = new Plane(new Vector3(0,0,1), 0);
            var normal = plane.Normal;

            Plane plane2 = Plane.CreateFromVertices(ToVector(C()), ToVector(A()), ToVector(O()));
            var normal2 = plane2.Normal;

            var angle = Math.Acos(Vector3.Dot(normal, normal2));
            Trace.WriteLine($"Alpha: {Alpha} Angle: {angle}");
            return angle;
        }

        private void CalculateGeometry()
        {
            _geo.Positions = new Point3DCollection(new Point3D[]
            {
                Transform.Transform(C()), Transform.Transform(O()), Transform.Transform(A()),
                Transform.Transform(B()), Transform.Transform(O()), Transform.Transform(C())
            });

        }

        private Point3D A()
        {
            return new Point3D(AO * Math.Sin(Theta), AO * Math.Cos(Theta), 0);
        }

        private Point3D B()
        {
            return new Point3D(AO * Math.Sin(Theta) * Math.Cos(Alpha), AO * Math.Cos(Theta), AO * Math.Sin(Theta) * Math.Sin(Alpha));
        }

        private Point3D C()
        {
            var a = A();
            var b = B();
            var acsq = ((new Point3D(CO * Math.Sin(Theta + Psi), CO * Math.Cos(Theta + Psi), 0) - a).LengthSquared);

            var rot = RotateToPlane();
            var a1 = rot.Transform(a);
            var b1 = rot.Transform(b);
            var rot2 = RotateToAxis(a1);
            var a2 = rot2.Transform(a1);
            var b2 = rot2.Transform(b1);

            var p = Trilaterate(a2, b2, CO * CO, acsq, acsq);

            rot.Invert();
            rot2.Invert();

            var p1 = rot.Transform(rot2.Transform(p));
            return p1;
        }

        private Point3D O()
        {
            return new Point3D(0, 0, 0);
        }

        private Matrix3D RotateToPlane()
        {
            if (Alpha == 0)
            {
                return Matrix3D.Identity;
            }
            Plane plane = Plane.CreateFromVertices(ToVector(A()), ToVector(B()), ToVector(O()));
            var normal = plane.Normal;
            var z = new Vector3(0, 0, 1);

            var rotationAxis = Vector3.Normalize(Vector3.Cross(normal, z));
            var angle = Math.Acos(Vector3.Dot(normal, z));
            var quat = new System.Windows.Media.Media3D.Quaternion(ToVector3D(rotationAxis), angle * 180 / Math.PI);

            var mat = Matrix3D.Identity;
            mat.Rotate(quat);
            return mat;
        }

        private Vector3 ToVector(Point3D p)
        {
            return new Vector3((float)p.X, (float)p.Y, (float)p.Z);
        }
        private Vector3D ToVector3D(Vector3 p)
        {
            return new Vector3D(p.X, p.Y, p.Z);
        }

        private Matrix3D RotateToAxis(Point3D p)
        {
            var angle = Math.Atan2(p.Y, p.X);
            var quat = new System.Windows.Media.Media3D.Quaternion(new Vector3D(0,0,1), -angle * 180 / Math.PI);

            var mat = Matrix3D.Identity;
            mat.Rotate(quat);
            return mat;
        }

        // https://en.wikipedia.org/wiki/True-range_multilateration
        private Point3D Trilaterate(Point3D a, Point3D b, double r1sq, double r2sq, double r3sq)
        {
            var u = a.X;
            var vx = b.X;
            var vy = b.Y;

            var x = (r1sq - r2sq + (u * u)) / (2 * u);
            var y = vy < 0.0001 ? -Math.Sqrt((r1sq - (x*x)) / 2) : (r1sq - r3sq + (vx * vx) + (vy * vy) - (2 * vx * x)) / (2 * vy);
            var z = r1sq - (x * x) - (y * y) < 0 ? 0 :  Math.Sqrt(r1sq - (x * x) - (y * y));


            return new Point3D(x, y, z);
        }

    }
}
