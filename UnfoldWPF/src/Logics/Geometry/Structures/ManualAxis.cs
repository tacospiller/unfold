using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public class ManualAxis : IAxis
    {
        public Matrix4x4 Transform { get; }
        public double Angle { get; private set; }

        public ManualAxis()
        {
            Transform = Matrix4x4.Identity;
            Angle = 0;
        }
        public ManualAxis(double initialAngle = 0, Matrix4x4? transform = null)
        {
            Transform = transform ?? Matrix4x4.Identity;
            Angle = initialAngle;
        }

        public void SetAngle(double angle)
        {
            Angle = angle;
        }
    }
}
