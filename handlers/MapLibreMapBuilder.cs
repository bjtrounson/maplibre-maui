using Android.Content;
using Org.Maplibre.Android.Camera;
using Org.Maplibre.Android.Geometry;
using Org.Maplibre.Android.Location.Engine;
using Org.Maplibre.Android.Maps;

namespace Maui.MapLibre.Handlers;

public class MapLibreMapBuilder : IMapLibreMapOptionsSink
{
    private readonly MapLibreMapOptions _options = new();
    private bool _trackCameraPosition;
    private bool _myLocationEnabled;
    private bool _dragEnabled = true;
    private int _myLocationTrackingMode;
    private int _myLocationRenderMode;
    private string _styleString = "";
    private LatLngBounds? _bounds;
    private LocationEngineRequest? _locationEngineRequest;

    public MapLibreMapController Build(Context context) {
        var controller = new MapLibreMapController(context, _options, _dragEnabled, _styleString);
        controller.SetMyLocationEnabled(_myLocationEnabled);
        controller.SetMyLocationTrackingMode(_myLocationTrackingMode);
        controller.SetMyLocationRenderMode(_myLocationRenderMode);
        controller.SetTrackCameraPosition(_trackCameraPosition);

        controller.SetCameraTargetBounds(_bounds);

        controller.SetLocationEngineProperties(_locationEngineRequest);

        return controller;
    }
    
    public void SetInitialCameraPosition(CameraPosition position) {
        _options.InvokeCamera(position);
    }
    
    public void SetDragEnabled(bool enabled) {
        _dragEnabled = enabled;
    }

    public void SetCameraTargetBounds(LatLngBounds bounds)
    {
        _bounds = bounds;
    }

    public void SetCompassEnabled(bool compassEnabled)
    {
        _options.InvokeCompassEnabled(compassEnabled);
    }

    public void SetStyleString(string styleString)
    {
        _styleString = styleString;
    }

    public void SetMinMaxZoomPreference(double? min, double? max)
    {
        if (min != null) {
            _options.InvokeMinZoomPreference((double)min);
        }
        if (max != null) {
            _options.InvokeMaxZoomPreference((double)max);
        }
    }

    public void SetRotateGesturesEnabled(bool rotateGesturesEnabled)
    {
        _options.InvokeRotateGesturesEnabled(rotateGesturesEnabled);
    }

    public void SetScrollGesturesEnabled(bool scrollGesturesEnabled)
    {
        _options.InvokeScrollGesturesEnabled(scrollGesturesEnabled);
    }

    public void SetTiltGesturesEnabled(bool tiltGesturesEnabled)
    {
        _options.InvokeTiltGesturesEnabled(tiltGesturesEnabled);
    }

    public void SetTrackCameraPosition(bool trackCameraPosition)
    {
        _trackCameraPosition = trackCameraPosition;
    }

    public void SetZoomGesturesEnabled(bool zoomGesturesEnabled)
    {
        _options.InvokeZoomGesturesEnabled(zoomGesturesEnabled);
    }

    public void SetMyLocationEnabled(bool myLocationEnabled)
    {
        _myLocationEnabled = myLocationEnabled;
    }

    public void SetMyLocationTrackingMode(int myLocationTrackingMode)
    {
        _myLocationTrackingMode = myLocationTrackingMode;
    }

    public void SetMyLocationRenderMode(int myLocationRenderMode)
    {
        _myLocationRenderMode = myLocationRenderMode;
    }

    public void SetLogoViewMargins(int x, int y)
    {
        _options.LogoMargins([
            x, // left
            0, // top
            0, // right
            y // bottom
        ]);
    }

    public void SetCompassGravity(int gravity)
    {
        _options.InvokeCompassGravity(gravity);
    }

    public void SetCompassViewMargins(int x, int y)
    {
        _options.CompassMargins([
            x, // left
            y, // top
            0, // right
            0 // bottom
        ]);
    }

    public void SetAttributionButtonGravity(int gravity)
    {
        _options.InvokeAttributionGravity(gravity);
    }

    public void SetAttributionButtonMargins(int x, int y)
    {
        _options.AttributionMargins([
            x, // left
            y, // top
            0, // right
            0 // bottom
        ]);
    }

    public void SetLocationEngineProperties(LocationEngineRequest? locationEngineRequest)
    {
        _locationEngineRequest = locationEngineRequest;
    }
}