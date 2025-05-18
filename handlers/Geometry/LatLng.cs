namespace Maui.MapLibre.Handlers.Geometry;

public class LatLng(double latitude, double longitude)
{
    public double Latitude { get; set; } = latitude;
    public double Longitude { get; set; } = longitude;

    public object? ToPlatform()
    {
#if ANDROID
        return new Org.Maplibre.Android.Geometry.LatLng(Latitude, Longitude) ;
#else
            return null;
#endif
    }
}