using System.Numerics;
using ImGuiNET;

namespace PartyListLayoutRevived.Config {

    public class CastbarTextElementConfig : TextElementConfig {

        [SerializeKey(SerializeKey.CastbarTextShowTarget)]
        public bool ShowTarget = false;

        public override void Editor(string name, ref bool c, PartyListLayoutRevived l = null) {
            base.Editor(name, ref c, l);
            c |= ImGui.Checkbox("Show Target", ref ShowTarget);
        }
    }
}
