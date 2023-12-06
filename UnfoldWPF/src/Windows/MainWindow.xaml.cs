using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Win32;
using UnfoldWPF.Objects;

namespace UnfoldWPF.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
