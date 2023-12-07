using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Win32;
using Unfold.Serialization;
using UnfoldWPF.Objects;
using UnfoldWPF.UserControls;

namespace UnfoldWPF.Windows
{
    public partial class MainWindow : Window
    {
        private List<SelectionChangedEventHandler> _selectionUpdaters = new List<SelectionChangedEventHandler>();
        public MainWindow()
        {
            InitializeComponent();
            ActiveFile.Static.FileLoaded += (o, e) =>
            {
                foreach (var handler in _selectionUpdaters)
                {
                    Structures.SelectionChanged -= handler;
                }
                _selectionUpdaters.Clear();

                foreach (var (key, val) in ActiveFile.Static.Collection.Children)
                {
                    object control = null;
                    switch (val.Def)
                    {
                        case BaseCardDef bcd:
                            control = new BaseCardListItem { Pair = val };
                            break;
                        case SymmetricVFoldDef svd:
                            control = new SymmetricVFoldListItem { Pair = val };
                            break;
                    }

                    if (control == null)
                    {
                        continue;
                    }

                    Structures.Items.Add(control);

                    var del = new SelectionChangedEventHandler((object o, SelectionChangedEventArgs e) =>
                    {
                        val.Selected = Structures.SelectedItem == control;
                    });
                    _selectionUpdaters.Add(del);
                    Structures.SelectionChanged += del;
                }
            };
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
