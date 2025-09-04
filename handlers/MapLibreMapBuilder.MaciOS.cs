using CoreLocation;
using Org.Maplibre.MaciOS;

namespace Maui.MapLibre.Handlers;

public partial class MapLibreMapBuilder
{
    private bool _trackCameraPosition;
    private bool _myLocationEnabled;
    private bool _dragEnabled = true;
    private int _myLocationTrackingMode;
    private int _myLocationRenderMode;
    private string _styleString = "";
    private MLNCoordinateBounds? _bounds;
    
   public void SetInitialCameraPosition(CLLocationCoordinate2D position)
   {
       return;
   }
    
    public void SetDragEnabled(bool enabled) {
        _dragEnabled = enabled;
    }

    public void SetCameraTargetBounds(Geometry.LatLngBounds bounds)
    {
        _bounds = (MLNCoordinateBounds) bounds.ToPlatform();
    }

    public void SetCompassEnabled(bool compassEnabled)
    {
        return;
    }

    public void SetStyleString(string styleString)
    {
        _styleString = styleString;
    }

    public void SetMinMaxZoomPreference(double? min, double? max)
    {
        return;
        /*if (min != null) {
            _options.InvokeMinZoomPreference((double)min);
        }
        if (max != null) {
            _options.InvokeMaxZoomPreference((double)max);
        }*/
    }

    public void SetRotateGesturesEnabled(bool rotateGesturesEnabled)
    {
        return;
        //_options.InvokeRotateGesturesEnabled(rotateGesturesEnabled);
    }

    public void SetScrollGesturesEnabled(bool scrollGesturesEnabled)
    {
        return;
        //_options.InvokeScrollGesturesEnabled(scrollGesturesEnabled);
    }

    public void SetTiltGesturesEnabled(bool tiltGesturesEnabled)
    {
        return;
        //_options.InvokeTiltGesturesEnabled(tiltGesturesEnabled);
    }

    public void SetTrackCameraPosition(bool trackCameraPosition)
    {
        return;
        //_trackCameraPosition = trackCameraPosition;
    }

    public void SetZoomGesturesEnabled(bool zoomGesturesEnabled)
    {
        return;
        //_options.InvokeZoomGesturesEnabled(zoomGesturesEnabled);
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
        return;
        /*_options.LogoMargins([
            x, // left
            0, // top
            0, // right
            y // bottom
        ]);*/
    }

    public void SetCompassGravity(int gravity)
    {
        return;
        //_options.InvokeCompassGravity(gravity);
    }

    public void SetCompassViewMargins(int x, int y)
    {
        return;
        /*_options.CompassMargins([
            x, // left
            y, // top
            0, // right
            0 // bottom
        ]);*/
    }

    public void SetAttributionButtonGravity(int gravity)
    {
        return;
        //_options.InvokeAttributionGravity(gravity);
    }

    public void SetAttributionButtonMargins(int x, int y)
    {
        return;
        /*_options.AttributionMargins([
            x, // left
            y, // top
            0, // right
            0 // bottom
        ]);*/
    } 
}