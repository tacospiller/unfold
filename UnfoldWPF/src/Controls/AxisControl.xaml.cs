using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Unfold.Serialization;
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
                Unfold.Serialization.AxisDef.AxisTypes.Manual,
                Unfold.Serialization.AxisDef.AxisTypes.Dependant,
            };
            Loaded += (obj, e) =>
            {
                AxisType.SelectedItem = AxisDef.Type;
            };
        }

        public static readonly DependencyProperty AxisDefProperty =
            DependencyProperty.Register("AxisDef", typeof(Unfold.Serialization.AxisDef), typeof(AxisControl), new UIPropertyMetadata(null));

        public Unfold.Serialization.AxisDef AxisDef
        {
            get { return (Unfold.Serialization.AxisDef)GetValue(AxisDefProperty); }
            set { SetValue(AxisDefProperty, value); }
        }

        private void AxisType_Selected(object sender, RoutedEventArgs e)
        {
            AxisDef.Type = (Unfold.Serialization.AxisDef.AxisTypes)AxisType.SelectedItem;
            if (AxisDef.Type == AxisDef.AxisTypes.Manual)
            {
                ManualSlider.Visibility = Visibility.Visible;
            } else
            {
                ManualSlider.Visibility = Visibility.Hidden;
            }
            Trace.WriteLine(AxisDef);
        }

        private void ManualSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ActiveFile.Static.Collection.UpdateManualSlider(AxisDef.Id, Unfold.UnfoldGeometry.UnfoldMath.DegToRad(ManualSlider.Value));
        }
    }
}
