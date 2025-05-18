namespace Maui.MapLibre.Handlers.Maps;

public class Map
{
    private object? _platform;
    public Map(object? platform)
    {
        _platform = platform;
        #if ANDROID
        #else
            return;
        #endif
    }
}