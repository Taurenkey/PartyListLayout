using System.Diagnostics.CodeAnalysis;
using PartyListLayoutRevived.Config;
using PartyListLayoutRevived.Helper;
using System.Linq;
using System.Threading.Tasks;
using Dalamud.Game;
using Dalamud.Game.ClientState.Keys;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Plugin;
using Lumina.Excel.GeneratedSheets;
using Dalamud.Plugin.Services;
using ECommons;
using ECommons.DalamudServices;

namespace PartyListLayoutRevived {
    public class Plugin : IDalamudPlugin {
        public string Name => "Party List Layout";

        public PartyListLayoutRevived PartyListLayoutRevived;

        public PluginConfig Config;
        public ConfigWindow ConfigWindow;
        
        public static Plugin Instance { get; private set; }

        private bool isDisposed;

        public Plugin(DalamudPluginInterface pluginInterface)
        {
            ECommonsMain.Init(pluginInterface, this, Module.All);
            Instance = this;
            Init();
        }

        public void Dispose() {
            isDisposed = true;
            PartyListLayoutRevived?.Dispose();
            foreach (var hook in Common.HookList.Where(hook => !hook.IsDisposed)) {
                if (hook.IsEnabled) hook.Disable();
                hook.Dispose();
            }

            Svc.Commands.RemoveHandler("/playout");
            ConfigWindow?.Hide();
        }

        public void Init() {

            Config = (PluginConfig) Svc.PluginInterface.GetPluginConfig() ?? new PluginConfig();
            ConfigWindow = new ConfigWindow(this);
            ConfigWindow.SetupLayoutFlags();
#if DEBUG
            SimpleLog.SetupBuildPath();
#endif
            Svc.PluginInterface.UiBuilder.OpenConfigUi += OnConfig;

            PartyListLayoutRevived = new PartyListLayoutRevived(this);
            PartyListLayoutRevived.Enable();

            Svc.Commands.AddHandler("/playout", new CommandInfo(OnConfigCommandHandler) {
                ShowInHelp = true,
                HelpMessage = $"Open or close the {Name} config window."
            });

#if DEBUG
            ConfigWindow.Show();
#endif
        }

        public void OnConfigCommandHandler(string command, string args) {
            OnConfig();
        }

        public void OnConfig() {
            ConfigWindow?.Toggle();
        }
    }
}
