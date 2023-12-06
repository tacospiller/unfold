using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            DataContext = this;
            AxisTypeComboBox.ItemsSource = new object[] {
                AxisDef.AxisTypes.Manual,
                AxisDef.AxisTypes.Dependant,
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
            var type = (Pair?.Def as IRotatingStructureDef)?.Axis?.Type;
            if (type == AxisDef.AxisTypes.Manual)
            {
                ManualSlider.Visibility = Visibility.Visible;
                ParentStructure.Visibility = Visibility.Hidden;
                ParentStructureTag.Visibility = Visibility.Hidden;
                AxisDesc.Visibility = Visibility.Hidden;
                AxisDescTag.Visibility = Visibility.Hidden;

            } else if (type == AxisDef.AxisTypes.Dependant)
            {
                ManualSlider.Visibility = Visibility.Hidden;
                ParentStructure.Visibility = Visibility.Visible;
                ParentStructureTag.Visibility = Visibility.Visible;
                AxisDesc.Visibility = Visibility.Visible;
                AxisDescTag.Visibility = Visibility.Visible;

                (Pair.Def as IRotatingStructureDef).Axis.DependantProperties ??= new AxisDef.DependantAxisProperties();
                var dep = (Pair.Def as IRotatingStructureDef).Axis.DependantProperties;
                ParentStructure.ItemsSource = ActiveFile.Static.Collection.Children.Values.Select(x => x.Def.Id).ToArray();
                AxisDesc.ItemsSource = (ActiveFile.Static.Collection.Children[dep.ParentStructureId].Def as IRotatingStructureDef).GetAllAxisDescriptors();
            } else
            {
                ManualSlider.Visibility = Visibility.Hidden;
                ParentStructure.Visibility = Visibility.Hidden;
                ParentStructureTag.Visibility = Visibility.Hidden;
                AxisDesc.Visibility = Visibility.Hidden;
                AxisDescTag.Visibility = Visibility.Hidden;
            }
        }

        private void ManualSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var axis = ((Pair?.Structure as RotatingStructure)?.Axis as ManualAxis);
            axis?.SetAngle(UnfoldMath.DegToRad(ManualSlider.Value));
            ActiveFile.Static.InvokeStructureUpdated();
        }

        private void ParentStructure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dep = (Pair.Def as IRotatingStructureDef).Axis.DependantProperties;
            AxisDesc.ItemsSource = (ActiveFile.Static.Collection.Children[dep.ParentStructureId].Def as IRotatingStructureDef).GetAllAxisDescriptors();
            ActiveFile.Static.InvokeAxisUpdated();
        }

        private void AxisDesc_Selected(object sender, RoutedEventArgs e)
        {
            ActiveFile.Static.InvokeAxisUpdated();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActiveFile.Static.InvokeAxisUpdated();
        }
    }
}
