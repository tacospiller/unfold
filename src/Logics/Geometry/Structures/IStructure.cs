using System.Numerics;

namespace Unfold.UnfoldGeometry
{
    public interface IStructure
    {
        double FoldAngle { get; set; }
        Vector3[] Faces { get; }
    }
}
