namespace Maui.MapLibre.Handlers.Geometry;

public class LatLng
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public object? ToPlatform()
    {
#if ANDROID
        return new Org.Maplibre.Android.Geometry.LatLng(Latitude, Longitude) ;
#else
            return null;
#endif
    }
}