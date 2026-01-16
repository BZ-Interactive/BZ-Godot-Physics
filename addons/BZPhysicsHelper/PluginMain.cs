#if TOOLS
using Godot;

namespace BZ.Physics;

[Tool]
public partial class PluginMain : EditorPlugin
{
    public override void _EnterTree()
    {
        #if DEBUG   
        // This runs when the user enables the plugin
        GD.Print("BZ Physics Helper is installed.");
        #endif
    }

    public override void _ExitTree()
    {
        #if DEBUG
        // Clean-up logic goes here when disabled
        GD.Print("BZ Physics Helper is uninstalled.");
        #endif
    }
}
#endif