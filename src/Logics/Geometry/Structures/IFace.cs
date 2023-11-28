using System;
using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public interface IFace
    {
        Matrix4x4 Transform { get; }
        IFace Offset(double x, double y, double z);
    }

    public class DynamicFace : IFace
    {
        private Func<Matrix4x4> _transform;
        public DynamicFace(Func<Matrix4x4> transform)
        {
            _transform = transform;
        }

        public IFace Offset(double x, double y, double z)
        {
            var tf = _transform;
            return new DynamicFace(() =>
            {
                return Matrix4x4.CreateTranslation((float)x, (float)y, (float)z) * _transform();
            });
        }

        public Matrix4x4 Transform => _transform();
    }
}
