namespace Maui.MapLibre.Handlers.Maps;

public class Source
{
    private object _platform;
    public string Attribution { get; set; }
    public string Id { get; set; }

    public Source(object platform)
    {
        _platform = platform;
#if ANDROID
        var source = (Org.Maplibre.Android.Style.Sources.Source)platform;
        Attribution = source.Attribution;
        Id = source.Id;
#else
        return;
#endif
    }
}