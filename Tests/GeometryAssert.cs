using System.Numerics;
using Unfold.UnfoldGeometry;

namespace Tests
{
    public static class GeometryAssert
    {
        public static void AlmostEqual(Vector3 expected, Vector3 actual)
        {
            Assert.InRange((expected - actual).Length(), 0, UnfoldMath.EqualThreshold);
        }
    }
}