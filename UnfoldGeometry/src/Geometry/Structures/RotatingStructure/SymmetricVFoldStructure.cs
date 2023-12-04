using System.Numerics;
using UnfoldGeometry.Serialization;

namespace Unfold.UnfoldGeometry
{
    public class SymmetricVFoldStructure : RotatingStructure
    {
        // B is the origin point. segment AB and CB are attached to base. D is a free floating(calculated) point. 
        // Triangle ABD and CBD are moving faces of the v-fold.
        private readonly SymmetricVFoldDef _def;
        public double Theta => UnfoldMath.DegToRad(_def.Theta);
        public double Psi => UnfoldMath.DegToRad(_def.Psi);
        public double DistA => _def.DistA;
        public double DistD => _def.DistD;
        public double AngleAYC => Axis.Angle;

        public Vector3 A => new Vector3((float)(DistA * Math.Sin(Theta)), (float)(DistA * Math.Cos(Theta)), 0);
        public Vector3 C => new Vector3((float)(DistA * Math.Sin(Theta) * Math.Cos(AngleAYC)), (float)(DistA * Math.Cos(Theta)), (float)(DistA * Math.Sin(Theta) * Math.Sin(AngleAYC)));
        public Vector3 B => new Vector3(0, 0, 0);
        public Vector3 DInitial => new Vector3((float)(DistD * Math.Sin(Theta + Psi)), (float)(DistD * Math.Cos(Theta + Psi)), 0);
        public Vector3 DFinal => new Vector3((float)(DistD * Math.Sin(Theta + Psi) * Math.Cos(AngleAYC)), (float)(DistD * Math.Cos(Theta + Psi)), (float)(DistD * Math.Sin(Theta + Psi) * Math.Sin(AngleAYC)));
        public double DistAD => (DInitial - A).Length();
        public Vector3 D => UnfoldMath.Trilaterate(B, A, C, DistD, DistAD, DistAD);

        protected override Vector3[] CalculateUntransformedFaces()
        {
            return new Vector3[] { B, A, D, C, B, D };
        }

        public new static class AxisDescriptors
        {
            public static AxisDescriptor AOuter = new AxisDescriptor("SymmetricVFold.AOuter");
            public static AxisDescriptor COuter => new AxisDescriptor("SymmetricVFold.COuter");
        }
        public override IAxis? GetAxis(AxisDescriptor desc)
        {
            switch (desc)
            {
                case var d when d == AxisDescriptors.AOuter: return AOuterAxis;
                case var d when d == AxisDescriptors.COuter: return COuterAxis;
                default: return base.GetAxis(desc);
            }
        }

        public IAxis AOuterAxis
        {
            get
            {
                return new DynamicAxis(
                    () => { return Matrix4x4.CreateRotationZ((float)-Theta) * Axis.Transform; },
                    () => { return UnfoldMath.GetAngle(Plane.CreateFromVertices(A, B, D).Normal, Plane.CreateFromVertices(A, B, DInitial).Normal); }
                    );
            }
        }
        public IAxis COuterAxis
        {
            get
            {
                return new DynamicAxis(() =>
                {
                    var rot = Matrix4x4.CreateRotationY((float)-AngleAYC);
                    var rot2 = Matrix4x4.CreateRotationZ((float)-Theta);
                    var flip = Matrix4x4.CreateReflection(Plane.CreateFromVertices(B, C, DFinal));
                    return rot2 * rot * flip * Axis.Transform;
                }, () =>
                {
                    return UnfoldMath.GetAngle(Plane.CreateFromVertices(C, B, D).Normal, Plane.CreateFromVertices(C, B, DFinal).Normal);
                });
            }
        }

        public IFace FaceABD
        {
            get
            {
                return new DynamicFace(() =>
                {
                    var face = Plane.CreateFromVertices(A, B, D);
                    var rot = UnfoldMath.GetRotationMatrix(Vector3.UnitZ, face.Normal);
                    return rot * Axis.Transform;
                });
            }
        }

        public IFace FaceCBD
        {
            get
            {
                return new DynamicFace(() =>
                {
                    var face = Plane.CreateFromVertices(C, B, D);
                    var rot = UnfoldMath.GetRotationMatrix(Vector3.UnitZ, face.Normal);
                    var rot2 = UnfoldMath.GetRotationMatrix(Vector3.Transform(Vector3.UnitY, rot), Vector3.Normalize(C));

                    return rot * rot2 * Axis.Transform;
                });
            }
        }

        public SymmetricVFoldStructure(IStructureDefCollection coll, SymmetricVFoldDef def) : base(def.Axis.ToAxis(coll) ?? new ManualAxis())
        {
            _def = def;
        }
    }
}
