using System.Numerics;
using Unfold.Serialization;

namespace Unfold.UnfoldGeometry
{
    public interface IStructure
    {
        Vector3[] CalculateFaces();
        IAxis? GetAxis(AxisDescriptor desc);
    }
}
