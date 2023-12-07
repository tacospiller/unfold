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
using Unfold.Serialization;
using UnfoldWPF.Objects;

namespace UnfoldWPF.UserControls
{
    /// <summary>
    /// BaseCardListItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StructureListItem : UserControl
    {
        public StructureListItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PairProperty =
            DependencyProperty.Register("Pair", typeof(DefStructureVisiblePair), typeof(StructureListItem), new UIPropertyMetadata(null));

        public DefStructureVisiblePair Pair
        {
            get { return (DefStructureVisiblePair)GetValue(PairProperty); }
            set { SetValue(PairProperty, value); }
        }

        public void BuildChildren()
        {

        }
    }
}
