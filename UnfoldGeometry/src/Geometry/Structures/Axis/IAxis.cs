using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public interface IAxis
    {
        Matrix4x4 Transform { get; }
        double Angle { get; }
        bool Valid { get; }

        IAxis OffsetY(double offsetY)
        {
            return new OffsetAxis(offsetY, this);
        }
    }

    public class OffsetAxis : IAxis
    {
        private readonly double _offsetY;
        private readonly IAxis _parent;

        public bool Valid => _parent.Valid;

        public OffsetAxis(double offsetY, IAxis parent)
        {
            _offsetY = offsetY;
            _parent = parent;
        }

        public Matrix4x4 Transform => Matrix4x4.Multiply(Matrix4x4.CreateTranslation(0, (float)_offsetY, 0), _parent.Transform);
        public double Angle => _parent.Angle;
    }
}
