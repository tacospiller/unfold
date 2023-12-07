using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Unfold.Serialization;
using Unfold.UnfoldGeometry;

namespace UnfoldWPF.Objects
{
    public class ActiveFileLoadedArguments
    {

    }

    public class StructureUpdatedArguments
    {

    }

    public class ActiveFile
    {
        public static ActiveFile Static = new ActiveFile();

        public string? Path { get; private set; }
        public bool Modified { get; set; }
        public List<IStructureDef>? FileContent { get; private set; }
        public StructureMeshCollection Collection { get; } = new StructureMeshCollection();

        public event EventHandler<ActiveFileLoadedArguments> FileLoaded = delegate { };
        public event EventHandler<StructureUpdatedArguments> StructureUpdated = delegate { };
        public event EventHandler<StructureUpdatedArguments> AxisUpdated = delegate { };

        public void Load(string path)
        {
            Path = path;
            var fileName = Path;
            var filestream = File.OpenRead(fileName);
            var content = JsonSerializer.Deserialize<List<IStructureDef>>(filestream);
            FileContent = content;
            Collection.ReloadFromDefs(FileContent);
            FileLoaded(this, new ActiveFileLoadedArguments());
        }

        public void InvokeStructureUpdated()
        {
            StructureUpdated(this, new StructureUpdatedArguments());
        }

        public void InvokeAxisUpdated()
        {
            AxisUpdated(this, new StructureUpdatedArguments());
        }
    }
}
