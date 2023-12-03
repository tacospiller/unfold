using System;
using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public class DynamicAxis : IAxis
    {
        public Matrix4x4 Transform => _transformGetter();
        public double Angle => _angleGetter();

        private Func<Matrix4x4> _transformGetter;
        private Func<double> _angleGetter;

        public DynamicAxis(Func<Matrix4x4> transformGetter, Func<double> angleGetter)
        {
            _transformGetter = transformGetter;
            _angleGetter = angleGetter;
        }
    }
}
