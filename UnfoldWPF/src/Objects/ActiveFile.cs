using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnfoldGeometry.Serialization;

namespace UnfoldWPF.src.Objects
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
        public StructureDefCollection? FileContent { get; private set; }
        public event EventHandler<ActiveFileLoadedArguments> FileLoaded = delegate { };
        public event EventHandler<StructureUpdatedArguments> StructureUpdated = delegate { };

        public void Load(string path)
        {
            Path = path;
            var fileName = Path;
            var filestream = File.OpenRead(fileName);
            var content = JsonSerializer.Deserialize<StructureDefCollection>(filestream);
            FileContent = content;
            FileLoaded(this, new ActiveFileLoadedArguments());
        }

        public void InvokeStructureUpdated()
        {
            StructureUpdated(this, new StructureUpdatedArguments());
        }
    }
}
