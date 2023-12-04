using System.Numerics;
using UnfoldGeometry.Serialization;

namespace Unfold.UnfoldGeometry
{
    public class FaceStructure : IStructure
    {
        private IFace _parent;

        public double Width { get; init; }
        public double Height { get; init; }
        private Vector3[] _faces => new Vector3[] { new Vector3(0, 0, 0), new Vector3((float)Width, 0, 0), new Vector3((float)Width, (float)Height, 0), new Vector3(0, 0, 0), new Vector3((float)Width, (float)Height, 0), new Vector3(0, (float)Height, 0) };

        public Vector3[] CalculateFaces()
        {
            return _faces.Select(x => Vector3.Transform(x, _parent.Transform)).ToArray();
        }

        public IAxis? GetAxis(AxisDescriptor desc)
        {
            return null;
        }

        public FaceStructure(IFace face)
        {
            _parent = face;
        }
    }
}
