using System.Windows.Controls;
using Unfold.UnfoldWPF;

namespace Unfold.Pages
{
    public partial class Preview3D : Page
    {
        private Scene3D _scene;
        
        public Preview3D()
        {
            _scene = new Scene3D();
            Content = _scene.Viewport;
        }
    }
}
