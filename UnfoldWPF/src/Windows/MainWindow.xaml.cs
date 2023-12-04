using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using UnfoldWPF.src.Objects;

namespace UnfoldWPF.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddSliders();
            ActiveFile.Static.FileLoaded += (object? sender, ActiveFileLoadedArguments e) =>
            {
                AddSliders();
            };
        }

        private void AddSliders()
        {
            var manualAxes = ActiveFile.Static.FileContent?.GetManualAxes();
            if (manualAxes != null)
            {
                foreach (var axis in manualAxes)
                {
                    var slider = new Slider
                    {
                        Maximum = 180,
                        Orientation = Orientation.Horizontal,
                        Value = 0,
                        Minimum = 0,
                        SmallChange = 10,
                        LargeChange = 30,
                        TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight,
                        TickFrequency = 10,
                        AutoToolTipPlacement = System.Windows.Controls.Primitives.AutoToolTipPlacement.BottomRight,
                    };
                    slider.ValueChanged += (object sender, RoutedPropertyChangedEventArgs<double> e) =>
                    {
                        axis.SetAngle(e.NewValue / 180 * Math.PI);
                        ActiveFile.Static.InvokeStructureUpdated();
                    };
                    Sliders.Children.Add(slider);
                }
            }

        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Trace.WriteLine("Save");
        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Trace.WriteLine("Save");
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Trace.WriteLine("Save");
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Unfold Files|*.pop;*.json";
            var result = dialog.ShowDialog();
            if (result == true)
            {
                ActiveFile.Static.Load(dialog.FileName);
            }
        }
    }
}
