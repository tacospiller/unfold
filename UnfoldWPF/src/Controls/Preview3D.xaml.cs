using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using Unfold.UnfoldGeometry;
using UnfoldGeometry.Serialization;
using UnfoldWPF.Objects;
using UnfoldWPF.src.Objects;

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
            var meshes = StructureMeshCollection.CreateFromDefs(ActiveFile.Static.FileContent);
            _scene.LoadComponents(meshes);
        }

        private void RedrawComponents()
        {
            _scene.Collection?.Recalculate();
        }
    }
}
