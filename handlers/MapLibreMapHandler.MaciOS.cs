#nullable enable
using Microsoft.Maui.Handlers;
using UIKit;

namespace Maui.MapLibre.Handlers;

public partial class MapLibreMapHandler : ViewHandler<MapLibreMap, UIView>
{
    private MapLibreMapController _controller = null!;
    private string _styleUrl = null!;
    
    public IMapLibreMapController Controller => _controller;
    
    public MapLibreMapHandler() : base(PropertyMapper) { }
    
    protected override UIView CreatePlatformView()
    {
        var mapView = new UIView();
        return mapView;
    }
    
     public void UpdateStyleUrl(string styleUrl)
    {
        _styleUrl = styleUrl;
        _controller.SetStyleString(styleUrl);
    }
    
    public void UpdateMinMaxZoomPreference(double? minZoom, double? maxZoom)
    {
        _controller.SetMinMaxZoomPreference(minZoom, maxZoom);
    }
    
    public void UpdateRotateGestureEnabled(bool rotateGestureEnabled)
    {
        _controller.SetRotateGesturesEnabled(rotateGestureEnabled);
    }
    
    public void UpdateScrollGesturesEnabled(bool scrollGesturesEnabled)
    {
        _controller.SetScrollGesturesEnabled(scrollGesturesEnabled);
    }
    
    public void UpdateTiltGesturesEnabled(bool tiltGesturesEnabled)
    {
        _controller.SetTiltGesturesEnabled(tiltGesturesEnabled);
    }
    
    public void UpdateTrackCameraPosition(bool trackCameraPosition)
    {
        _controller.SetTrackCameraPosition(trackCameraPosition);
    }
    
    public void UpdateZoomGesturesEnabled(bool zoomGesturesEnabled)
    {
        _controller.SetZoomGesturesEnabled(zoomGesturesEnabled);
    }
    
    public void UpdateMyLocationEnabled(bool myLocationEnabled)
    {
        _controller.SetMyLocationEnabled(myLocationEnabled);
    }
    
    public void UpdateMyLocationTrackingMode(int myLocationTrackingMode)
    {
        _controller.SetMyLocationTrackingMode(myLocationTrackingMode);
    }
    
    public void UpdateMyLocationRenderMode(int myLocationRenderMode)
    {
        _controller.SetMyLocationRenderMode(myLocationRenderMode);
    }

    public void UpdateLogoViewMargins(int?[]? margin)
    {
        if (margin == null) return;
        var x = margin[0];
        var y = margin[1];
        if (x == null || y == null) return;
        _controller.SetLogoViewMargins((int) x, (int) y);
    }
    
    public void UpdateCompassGravity(int compassGravity)
    {
        _controller.SetCompassGravity(compassGravity);
    }
    
    public void UpdateCompassViewMargins(int?[]? margin)
    {
        if (margin == null) return;
        var x = margin[0];
        var y = margin[1];
        if (x == null || y == null) return;
        _controller.SetCompassViewMargins((int)x, (int)y);
    }
    
    public void UpdateAttributionButtonGravity(int attributionButtonGravity)
    {
        _controller.SetAttributionButtonGravity(attributionButtonGravity);
    }
    
    public void UpdateAttributionButtonMargins(int?[]? margin)
    {
        if (margin == null) return;
        var x = margin[0];
        var y = margin[1];
        if (x == null || y == null) return;
        _controller.SetAttributionButtonMargins((int)x, (int)y);
    }
}
