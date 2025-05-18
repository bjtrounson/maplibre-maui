using Style = Maui.MapLibre.Handlers.Maps.Style;

namespace Maui.MapLibre.Handlers.EventArgs;

public class StyleLoadedEventArgs : System.EventArgs
{
    public Style Style { get; set; }
}