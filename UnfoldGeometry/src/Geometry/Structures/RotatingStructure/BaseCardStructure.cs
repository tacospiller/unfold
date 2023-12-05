using System.Numerics;
using Unfold.Serialization;

namespace Unfold.UnfoldGeometry
{
    public class BaseCardStructure : RotatingStructure
    {
        public readonly BaseCardDef _def;
        public double Width => _def.Width;
        public double Height { get => _def.Height; }

        public Vector3 A => new Vector3((float)Width, 0, 0);
        public Vector3 B => new Vector3((float)(Width * Math.Cos(Axis.Angle)), 0, (float)(Width * Math.Sin(Axis.Angle)));
        public Vector3 O => new Vector3(0, 0, 0);
        private Vector3 H => new Vector3(0, (float)Height, 0);

        protected override Vector3[] CalculateUntransformedFaces()
        {
            return new Vector3[] {
            A, O, O + H,
            A, O + H, A + H,
            O, B, B + H,
            O, B + H, O + H
            };
        }

        public BaseCardStructure(DefStructurePairCollection coll, BaseCardDef def) : base(def.Axis.ToAxis(coll) ?? new ManualAxis())
        {
            _def = def;
        }
    }
}
