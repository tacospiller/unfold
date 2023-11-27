using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Unfold.UnfoldGeometry
{
    internal static class UnfoldMath
    {
        static private double Precision = 1e-6;
        static public Vector3 Trilaterate(Vector3 p1, Vector3 p2, Vector3 p3, double r1, double r2, double r3)
        {
            if (!IsTriangle(p1, p2, p3))
            {
                if (p1 == p2 && p2 == p3)
                {
                    throw new Exception("No");
                }
                if (p1 == p2)
                {
                    return Bilaterate(p1, p3, r1, r3);
                }
                return Bilaterate(p1, p2, r1, r2);
            }

            // 1.First rotate points to lie on xy plane
            var plane123 = Plane.CreateFromVertices(p1, p2, p3);
            var mat1 = GetRotationMatrix(plane123.Normal, Vector3.UnitZ);

            var p1a = Vector3.Transform(p1, mat1);
            var p2a = Vector3.Transform(p2, mat1);
            var p3a = Vector3.Transform(p3, mat1);

            // 2.Translate p1 to match origin point
            var mat2 = GetTranslationMatrix(p1a, Vector3.Zero);

            var p2b = Vector3.Transform(p2a, mat2);
            var p3b = Vector3.Transform(p3a, mat2);

            // 3.Rotate again so p1p2 can align with X axis 

            var mat3 = GetRotationMatrix(p2b, Vector3.UnitX);

            var p2c = Vector3.Transform(p2b, mat3);
            var p3c = Vector3.Transform(p3b, mat3);

            // Apply simplified trilateration
            var q = SimpleTrilateration(p2c, p3c, r1, r2, r3);

            // inverse apply 1,2,3
            Matrix4x4.Invert(Matrix4x4.Multiply(Matrix4x4.Multiply(mat1, mat2), mat3), out var mati);
            var qa = Vector3.Transform(q, mati);
            if (float.IsNaN(qa.X) || float.IsNaN(qa.Y) || float.IsNaN(qa.Z))
            {
                throw new Exception("No");
            }
            return qa;
        }

        static public Vector3 Bilaterate(Vector3 p1, Vector3 p2, double r1, double r2)
        {
            var mat1 = GetTranslationMatrix(p1, Vector3.Zero);
            var p2a = Vector3.Transform(p2, mat1);
            var mat2 = GetRotationMatrix(p2a, Vector3.UnitX);
            var p2b = Vector3.Transform(p2a, mat2);

            var x = ((r1 * r1) - (r2 * r2) + (p2b.X * p2b.X)) / (2 * p2b.X);
            var y = Math.Sqrt((r1 * r1) - (x * x));

            var p = new Vector3((float)x, (float)y, 0);
            Matrix4x4.Invert(Matrix4x4.Multiply(mat1, mat2), out var mati);

            return Vector3.Transform(p, mati);
        }

        static public bool IsTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var v12 = Vector3.Normalize(p1 - p2);
            var v23 = Vector3.Normalize(p2 - p3);
            var v31 = Vector3.Normalize(p3 - p1);

            return p1 != p2 && p2 != p3 && p3 != p1 && v12 != v23 && v12 != -v23;
        }

        static private Vector3 SimpleTrilateration(Vector3 a, Vector3 b, double r1, double r2, double r3)
        {
            var u = a.X;
            var vx = b.X;
            var vy = b.Y;

            var r1sq = r1 * r1;
            var r2sq = r2 * r2;
            var r3sq = r3 * r3;

            var x = (r1sq - r2sq + (u * u)) / (2 * u);
            var y = Math.Abs(vy) < Precision ? -Math.Sqrt(r1sq - (x * x)) : (r1sq - r3sq + (vx * vx) + (vy * vy) - (2 * vx * x)) / (2 * vy);
            var z = r1sq - (x * x) - (y * y) < 0 ? 0 : Math.Sqrt(r1sq - (x * x) - (y * y));

            return new Vector3((float)x, (float)y, (float)z);
        }

        static public Matrix4x4 GetRotationMatrix(Vector3 from, Vector3 to)
        {
            var nfrom = Vector3.Normalize(from);
            var nto = Vector3.Normalize(to);
            var axis = Vector3.Normalize(Vector3.Cross(nfrom, nto));
            if (float.IsNaN(axis.X))
            {
                return Matrix4x4.Identity;
            }
            var angle = Math.Acos(Vector3.Dot(nfrom, nto));

            var quat = GetQuaternion(axis, angle);
            var mat = Matrix4x4.CreateFromQuaternion(quat);
            
            return mat;
        }

        static private System.Numerics.Quaternion GetQuaternion(Vector3 axis, double angle)
        {
            return System.Numerics.Quaternion.Normalize(new System.Numerics.Quaternion((float)(axis.X * Math.Sin(angle / 2)), (float)(axis.Y * Math.Sin(angle / 2)), (float)(axis.Z * Math.Sin(angle / 2)), (float)Math.Cos(angle / 2)));
        }

        static public Matrix4x4 GetScaleMatrix(Vector3 from, Vector3 to)
        {
            return Matrix4x4.CreateScale(to.Length() / from.Length());
        }

        static public Matrix4x4 GetTranslationMatrix(Vector3 from, Vector3 to)
        {
            var mat = Matrix4x4.CreateTranslation(to - from);
            return mat;
        }

        static public double GetAngle(Vector3 a, Vector3 b)
        {
            a = Vector3.Normalize(a);
            b = Vector3.Normalize(b);
            return Math.Acos(Vector3.Dot(a, b));
        }
    }
}
