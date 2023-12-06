using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public class DynamicAxis : IAxis
    {
        public Matrix4x4 Transform => _transformGetter();
        public double Angle => _angleGetter();

        public bool Valid => _validGetter();

        private Func<Matrix4x4> _transformGetter;
        private Func<double> _angleGetter;
        private Func<bool> _validGetter;

        public DynamicAxis(Func<Matrix4x4> transformGetter, Func<double> angleGetter, Func<bool> validGetter)
        {
            _transformGetter = transformGetter;
            _angleGetter = angleGetter;
            _validGetter = validGetter;
        }
    }
}
