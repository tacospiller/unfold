using System.Numerics;
using Unfold.UnfoldGeometry;
using static Unfold.UnfoldGeometry.UnfoldMath;

namespace Tests
{
    public class MathTest
    {
        [Fact]
        public void Trilaterate_xy_triangle_flat_point()
        {
            var point = UnfoldMath.Trilaterate(new Vector3(0, 0, 0), new Vector3(2, 0, 0), new Vector3(1, 1, 0), 1, 1, 1);
            Assert.Equal(new Vector3(1, 0, 0), point);
        }

        [Fact]
        public void Trilaterate_xy_triangle_convex_point()
        {
            var point = UnfoldMath.Trilaterate(new Vector3(0, 0, 0), new Vector3(2, 0, 0), new Vector3(1, 1, 0), Math.Sqrt(2), Math.Sqrt(2), Math.Sqrt(2), true);
            Assert.Equal(new Vector3(1, 0, 1), point);
        }

        [Fact]
        public void Trilaterate_xy_triangle_concave_point()
        {
            var point = UnfoldMath.Trilaterate(new Vector3(0, 0, 0), new Vector3(2, 0, 0), new Vector3(1, 1, 0), Math.Sqrt(2), Math.Sqrt(2), Math.Sqrt(2), false);
            Assert.Equal(new Vector3(1, 0, -1), point);
        }

        [Fact]
        public void Trilaterate_floating_triangle_complex()
        {
            var point = UnfoldMath.Trilaterate(new Vector3(1, 1, 1), new Vector3(2, 1, 0), new Vector3(-1, -1, -1), Math.Sqrt(3), Math.Sqrt(5), Math.Sqrt(3), true);
            GeometryAssert.AlmostEqual(new Vector3(0, 0, 0), point);
        }

        [Fact]
        public void Trilaterate_floating_triangle_simple()
        {
            var point = UnfoldMath.Trilaterate(new Vector3(1, 1, 1), new Vector3(1, 1, 3), new Vector3(1, 2, 2), 1, 1, 1, true);
            GeometryAssert.AlmostEqual(new Vector3(1, 1, 2), point);
        }

        [Fact]
        public void Trilaterate_not_triangle()
        {
            var point = UnfoldMath.Trilaterate(new Vector3(2, 0, 0), new Vector3(2, 0, 0), new Vector3(0, 0, 0), 1, 1, 1);
            Assert.Equal(new Vector3(1, 0, 0), point);
        }

        [Fact]
        public void Trilaterate_not_triangle_vertical()
        {
            var point = UnfoldMath.Trilaterate(new Vector3(0, 2, 0), new Vector3(0, 2, 0), new Vector3(0, 0, 0), 1, 1, 1);
            GeometryAssert.AlmostEqual(new Vector3(0, 1, 0), point);
        }

        [Fact]
        public void Trilaterate_impossible()
        {
            Assert.Throws<MathException>(() =>
            {
                var _ = UnfoldMath.Trilaterate(new Vector3(0, 0, 0), new Vector3(2, 0, 0), new Vector3(1, 1, 0), 1, 1, 100);
            });
        }

        [Fact]
        public void Trilaterate_infinite()
        {
            Assert.Throws<MathException>(() =>
            {
                var _ = UnfoldMath.Trilaterate(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1, 1, 1);
            });
        }
    }
}