using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnfoldWPF.Objects;

namespace UnfoldWPF.UserControls
{
    /// <summary>
    /// SymmetricVFoldListItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SymmetricVFoldListItem : UserControl
    {
        public SymmetricVFoldListItem()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty PairProperty =
            DependencyProperty.Register("Pair", typeof(DefStructureVisiblePair), typeof(SymmetricVFoldListItem), new UIPropertyMetadata(null));

        public DefStructureVisiblePair Pair
        {
            get { return (DefStructureVisiblePair)GetValue(PairProperty); }
            set { SetValue(PairProperty, value); }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActiveFile.Static.InvokeStructureUpdated();
        }
    }
}
