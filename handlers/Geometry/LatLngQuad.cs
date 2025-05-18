namespace Maui.MapLibre.Handlers.Geometry;

public class LatLngQuad
{
    public LatLng TopRight { get; set; }
    public LatLng TopLeft { get; set; }
    public LatLng BottomRight { get; set; }
    public LatLng BottomLeft { get; set; }

    public object? ToPlatform()
    {
#if ANDROID
        var topRight = (Org.Maplibre.Android.Geometry.LatLng?) TopRight.ToPlatform();
        var topLeft = (Org.Maplibre.Android.Geometry.LatLng?) TopLeft.ToPlatform();
        var bottomRight = (Org.Maplibre.Android.Geometry.LatLng?) BottomRight.ToPlatform();
        var bottomLeft = (Org.Maplibre.Android.Geometry.LatLng?) BottomLeft.ToPlatform();
        if (topRight == null || topLeft == null || bottomRight == null || bottomLeft == null) return null;
        return new Org.Maplibre.Android.Geometry.LatLngQuad(topLeft, topRight, bottomRight, bottomLeft);
#else
            return null;
#endif
    }
}