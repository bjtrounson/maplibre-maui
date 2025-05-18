namespace Maui.MapLibre.Handlers.Maps;

public class Style
{
    private object _platform;
    public bool IsFullyLoaded { get; set; }
    public string Uri { get; set; }
    public string Json { get; set; }
    public IList<Source> Sources { get; set; }
    public IList<Layer> Layers { get; set; }
    
    public Style(object platform)
    {
        _platform = platform;
#if ANDROID
        var style = (Org.Maplibre.Android.Maps.Style)platform;
        IsFullyLoaded = style.IsFullyLoaded;
        Uri = style.Uri;
        Json = style.Json;
        Layers = style.Layers.Select(l => new Layer(l)).ToList();
        Sources = style.Sources.Select(s => new Source(s)).ToList();
#else
        return;
#endif
    }
}