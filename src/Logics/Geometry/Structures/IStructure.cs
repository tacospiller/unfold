using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public interface IStructure
    {
        public IAxis Axis { get; }
        double MaxAngle { get; }
        Vector3[] CalculateFaces();
    }
}
