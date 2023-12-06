using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Unfold.Serialization;
using Unfold.UnfoldGeometry;
using UnfoldWPF.Objects;

namespace UnfoldWPF.UserControls
{
    /// <summary>
    /// AxisControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AxisControl : UserControl
    {
        public AxisControl()
        {
            InitializeComponent();
            AxisType.ItemsSource = new object[] {
                AxisDef.AxisTypes.Manual,
                AxisDef.AxisTypes.Dependant,
            };
            Loaded += (obj, e) =>
            {
                AxisType.SelectedItem = (Pair?.Def as IRotatingStructureDef)?.Axis?.Type ?? AxisDef.AxisTypes.Manual;
            };
        }

        public static readonly DependencyProperty PairProperty =
            DependencyProperty.Register("Pair", typeof(DefStructureVisiblePair), typeof(AxisControl), new UIPropertyMetadata(null));

        public DefStructureVisiblePair Pair
        {
            get { return (DefStructureVisiblePair)GetValue(PairProperty); }
            set { SetValue(PairProperty, value); }
        }

        private void AxisType_Selected(object sender, RoutedEventArgs e)
        {
            if ((Pair?.Def as IRotatingStructureDef)?.Axis != null)
            {
                (Pair.Def as IRotatingStructureDef).Axis.Type = (AxisDef.AxisTypes)AxisType.SelectedItem;
                if ((AxisDef.AxisTypes)AxisType.SelectedItem == AxisDef.AxisTypes.Manual)
                {
                    ManualSlider.Visibility = Visibility.Visible;
                }
                else
                {
                    ManualSlider.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ManualSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var axis = ((Pair?.Structure as RotatingStructure)?.Axis as ManualAxis);
            axis?.SetAngle(UnfoldMath.DegToRad(ManualSlider.Value));
            ActiveFile.Static.InvokeStructureUpdated();
        }
    }
}
