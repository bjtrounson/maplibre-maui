namespace Maui.MapLibre.Handlers.Maps;

public class Layer
{
    private object? _platform;
    public string Id { get; set; }
    public bool IsDetached { get; set; }
    public float MinZoom { get; set; }
    public float MaxZoom { get; set; }
    public bool Visibility { get; set; }

    public Layer(object platform)
    {
        _platform = platform;
#if ANDROID
        var layer = (Org.Maplibre.Android.Style.Layers.Layer)platform;
        Id = layer.Id;
        IsDetached = layer.IsDetached;
        MinZoom = layer.MinZoom;
        MaxZoom = layer.MaxZoom;
        Visibility = layer.Visibility.Equals(true);
#else
        return;
#endif
    }
}