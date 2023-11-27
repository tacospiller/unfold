using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public interface IStructure
    {
        double FoldAngle { get; set; }
        double MaxAngle { get; }
        Vector3[] Faces { get; }
    }
}
