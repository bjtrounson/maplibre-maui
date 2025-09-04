using Maui.MapLibre.Handlers.Geometry;
using Org.Maplibre.MaciOS;
using Map = Maui.MapLibre.Handlers.Maps.Map;
using Style = Maui.MapLibre.Handlers.Maps.Style;

namespace Maui.MapLibre.Handlers;

public class MapLibreMapController : IMapLibreMapController
{
    public void SetCameraTargetBounds(LatLngBounds bounds)
    {
        throw new NotImplementedException();
    }

    public void SetCompassEnabled(bool compassEnabled)
    {
        throw new NotImplementedException();
    }

    public void SetStyleString(string styleString)
    {
        throw new NotImplementedException();
    }

    public void SetMinMaxZoomPreference(double? min, double? max)
    {
        throw new NotImplementedException();
    }

    public void SetRotateGesturesEnabled(bool rotateGesturesEnabled)
    {
        throw new NotImplementedException();
    }

    public void SetScrollGesturesEnabled(bool scrollGesturesEnabled)
    {
        throw new NotImplementedException();
    }

    public void SetTiltGesturesEnabled(bool tiltGesturesEnabled)
    {
        throw new NotImplementedException();
    }

    public void SetTrackCameraPosition(bool trackCameraPosition)
    {
        throw new NotImplementedException();
    }

    public void SetZoomGesturesEnabled(bool zoomGesturesEnabled)
    {
        throw new NotImplementedException();
    }

    public void SetMyLocationEnabled(bool myLocationEnabled)
    {
        throw new NotImplementedException();
    }

    public void SetMyLocationTrackingMode(int myLocationTrackingMode)
    {
        throw new NotImplementedException();
    }

    public void SetMyLocationRenderMode(int myLocationRenderMode)
    {
        throw new NotImplementedException();
    }

    public void SetLogoViewMargins(int x, int y)
    {
        throw new NotImplementedException();
    }

    public void SetCompassGravity(int gravity)
    {
        throw new NotImplementedException();
    }

    public void SetCompassViewMargins(int x, int y)
    {
        throw new NotImplementedException();
    }

    public void SetAttributionButtonGravity(int gravity)
    {
        throw new NotImplementedException();
    }

    public void SetAttributionButtonMargins(int x, int y)
    {
        throw new NotImplementedException();
    }

    public event Action<Map>? OnMapReadyReceived;
    public event Action? OnDidBecomeIdleReceived;
    public event Action<int>? OnCameraMoveStartedReceived;
    public event Action? OnCameraMoveReceived;
    public event Action? OnCameraIdleReceived;
    public event Action<int>? OnCameraTrackingChangedReceived;
    public event Action? OnCameraTrackingDismissedReceived;
    public event Func<LatLng, bool>? OnMapClickReceived;
    public event Func<LatLng, bool>? OnMapLongClickReceived;
    public event Action<Style>? OnStyleLoadedReceived;
    public event Action<Location>? OnUserLocationUpdateReceived;
    public void AddGeoJsonSource(string sourceName, string source)
    {
        throw new NotImplementedException();
    }

    public void AddRasterSource(string sourceName, string? tileUrl, string[]? tileUrlTemplates, int tileSize, int minZoom,
        int maxZoom)
    {
        throw new NotImplementedException();
    }

    public void AddRasterDemSource(string sourceName, string? tileUrl, string[]? tileUrlTemplates, int tileSize, int minZoom,
        int maxZoom)
    {
        throw new NotImplementedException();
    }

    public void AddVectorSource(string sourceName, string? tileUrl, string[]? tileUrlTemplates, int minZoom, int maxZoom)
    {
        throw new NotImplementedException();
    }

    public void AddImageSource(string sourceName, string url, LatLngQuad? coordinates)
    {
        throw new NotImplementedException();
    }

    public void SetGeoJsonSource(string sourceName, string source)
    {
        throw new NotImplementedException();
    }

    public void SetGeoJsonFeature(string sourceName, string geojsonFeature)
    {
        throw new NotImplementedException();
    }

    public void RemoveSource(string sourceId)
    {
        throw new NotImplementedException();
    }

    public void AddSymbolLayer(string layerName, string sourceName, string? belowLayerId, string? sourceLayer,
        IDictionary<string, object?> properties, float minZoom = 0, float maxZoom = 0, bool enableInteraction = false)
    {
        throw new NotImplementedException();
    }

    public void AddLineLayer(string layerName, string sourceName, string? belowLayerId, string? sourceLayer,
        IDictionary<string, object?> properties, float minZoom = 0, float maxZoom = 0, bool enableInteraction = false)
    {
        throw new NotImplementedException();
    }

    public void AddFillLayer(string layerName, string sourceName, string? belowLayerId, string? sourceLayer,
        IDictionary<string, object?> properties, float minZoom = 0, float maxZoom = 0, bool enableInteraction = false)
    {
        throw new NotImplementedException();
    }

    public void AddFillExtrusionLayer(string layerName, string sourceName, string? belowLayerId, string? sourceLayer,
        IDictionary<string, object?> properties, float minZoom = 0, float maxZoom = 0, bool enableInteraction = false)
    {
        throw new NotImplementedException();
    }

    public void AddCircleLayer(string layerName, string sourceName, string? belowLayerId, string? sourceLayer,
        IDictionary<string, object?> properties, float minZoom = 0, float maxZoom = 0, bool enableInteraction = false)
    {
        throw new NotImplementedException();
    }

    public void AddRasterLayer(string layerName, string sourceName, IDictionary<string, object?> properties, float minZoom = 0, float maxZoom = 0,
        string? belowLayerId = null)
    {
        throw new NotImplementedException();
    }

    public void AddHillshadeLayer(string layerName, string sourceName, IDictionary<string, object?> properties, float minZoom = 0,
        float maxZoom = 0, string? belowLayerId = null)
    {
        throw new NotImplementedException();
    }

    public void AddHeatmapLayer(string layerName, string sourceName, IDictionary<string, object?> properties, float minZoom = 0, float maxZoom = 0,
        string? belowLayerId = null)
    {
        throw new NotImplementedException();
    }

    public void RemoveLayer(string layerId)
    {
        throw new NotImplementedException();
    }
}