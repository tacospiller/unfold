using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public interface IAxis
    {
        Matrix4x4 Transform { get; }
        double Angle { get; }

        IAxis OffsetY(double y)
        {
            return new OffsetAxis(y, this);
        }
    }
}
