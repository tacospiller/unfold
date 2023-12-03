using System.Numerics;
using UnfoldGeometry.Serialization;

namespace Unfold.UnfoldGeometry
{
    public interface IStructure
    {
        Vector3[] CalculateFaces();
        IAxis? GetAxis(AxisDescriptor desc);
    }
}
