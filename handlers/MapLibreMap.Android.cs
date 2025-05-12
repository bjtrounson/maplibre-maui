#nullable enable

using System.Text.Json;
using Android.Views;
using Android.Widget;
using Maui.MapLibre.Handlers.Annotation.Properties;
using View = Android.Views.View;

namespace Maui.MapLibre.Handlers;

public partial class MapLibreMapHandler
{
    private MapLibreMapController _controller = null!;
    private string _styleUrl = null!;
    
    public MapLibreMapController Controller => _controller;

    protected override View CreatePlatformView()
    {
        if (Platform.CurrentActivity == null)
        {
            throw new InvalidOperationException("MapLibreMapHandler requires a current activity.");
        }
        
        var activity = Platform.CurrentActivity;
        var context = activity.ApplicationContext;
        var options = new Dictionary<string, object> { { "styleString", _styleUrl } };
        
        if (context == null) throw new InvalidOperationException("MapLibreMapHandler requires a valid context.");

        _controller = MapLibreMapFactory.Create(activity, context, options);
        
        // Init and then return the map view
        _controller.Init();
        var mapView = _controller.View;
        
        // Create the root layout
        var layout = new FrameLayout(activity);
        layout.LayoutParameters ??= new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
        layout.LayoutParameters.Width = ViewGroup.LayoutParams.MatchParent;
        layout.LayoutParameters.Height = ViewGroup.LayoutParams.MatchParent;
        
        layout.AddView(mapView);
        
        return layout;
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

    public void UpdateLogoViewMargins(int x, int y)
    {
        _controller.SetLogoViewMargins(x, y);
    }
    
    public void UpdateCompassGravity(int compassGravity)
    {
        _controller.SetCompassGravity(compassGravity);
    }
    
    public void UpdateCompassViewMargins(int x, int y)
    {
        _controller.SetCompassViewMargins(x, y);
    }
    
    public void UpdateAttributionButtonGravity(int attributionButtonGravity)
    {
        _controller.SetAttributionButtonGravity(attributionButtonGravity);
    }
    
    public void UpdateAttributionButtonMargins(int x, int y)
    {
        _controller.SetAttributionButtonMargins(x, y);
    }
    
    public void AddLineLayer(string layerName, string sourceName, string? belowLayerId, string? sourceLayer, LineLayerProperties properties, int minZoom, int maxZoom, bool enableInteraction)
    {
        var propertyValues = properties.ToDictionary();
        _controller.AddLineLayer(layerName, sourceName, belowLayerId, sourceLayer, propertyValues, null, minZoom, maxZoom, enableInteraction);
    }
}
