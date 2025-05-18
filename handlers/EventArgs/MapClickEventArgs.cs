using Maui.MapLibre.Handlers.Geometry;

namespace Maui.MapLibre.Handlers.EventArgs;

public class MapClickEventArgs : System.EventArgs
{
    public LatLng LatLng { get; set; }
}