using Dalamud.Configuration;
using Newtonsoft.Json;

namespace PartyListLayoutRevived.Config {
    public class PluginConfig : IPluginConfiguration {


        [JsonIgnore] public bool PreviewMode;
        [JsonIgnore] public int PreviewCount = 8;

        public LayoutConfig CurrentLayout = new();
        public bool AutoSave = true;
        public int MaxAutoSave = 20;

        public int Version { get; set; } = 1;
    }
}
