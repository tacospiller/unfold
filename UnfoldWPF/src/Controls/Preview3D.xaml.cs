using System.Windows.Controls;
using UnfoldWPF.Objects;

namespace UnfoldWPF.UserControls
{
    public partial class Preview3D : UserControl
    {
        private Scene3D _scene;
        
        public Preview3D()
        {
            _scene = new Scene3D();
            Content = _scene.Viewport;

            ActiveFile.Static.FileLoaded += OnFileLoaded;
            ActiveFile.Static.StructureUpdated += OnStructureUpdated;
        }

        public void OnFileLoaded(object? sender, ActiveFileLoadedArguments args)
        {
            LoadFile();
        }

        public void OnStructureUpdated(object? sender, StructureUpdatedArguments args)
        {
            RedrawComponents();
        }

        private void LoadFile()
        {
            _scene.LoadComponents(ActiveFile.Static.Collection);
        }

        private void RedrawComponents()
        {
            ActiveFile.Static.Collection.Recalculate();
        }
    }
}
