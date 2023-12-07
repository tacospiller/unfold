using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public class OffsetAxis : IAxis
    {
        private readonly double _offsetY;
        private readonly IAxis _parent;

        public OffsetAxis(double offsetY, IAxis parent)
        {
            _offsetY = offsetY;
            _parent = parent;
        }

        public Matrix4x4 Transform => Matrix4x4.Multiply(Matrix4x4.CreateTranslation(0, (float)_offsetY, 0), _parent.Transform);
        public double Angle => _parent.Angle;
    }
}
