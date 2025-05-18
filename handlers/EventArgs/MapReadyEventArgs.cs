using Map = Maui.MapLibre.Handlers.Maps.Map;

namespace Maui.MapLibre.Handlers.EventArgs;

public class MapReadyEventArgs : System.EventArgs
{
    public Map Map { get; set; }
}