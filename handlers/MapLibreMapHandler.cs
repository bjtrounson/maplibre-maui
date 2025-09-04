﻿namespace Maui.MapLibre.Handlers;

public partial class MapLibreMapHandler 
{
    public static IPropertyMapper<MapLibreMap, MapLibreMapHandler> PropertyMapper =
        new PropertyMapper<MapLibreMap, MapLibreMapHandler>(ViewMapper)
        {
            [nameof(MapLibreMap.StyleUrl)] = MapStyleUrl,
            [nameof(MapLibreMap.MinZoom)] = MapMinZoom,
            [nameof(MapLibreMap.MaxZoom)] = MapMaxZoom,
            [nameof(MapLibreMap.RotateGestureEnabled)] = MapRotateGestureEnabled,
            [nameof(MapLibreMap.ScrollGesturesEnabled)] = MapScrollGesturesEnabled,
            [nameof(MapLibreMap.TiltGesturesEnabled)] = MapTiltGesturesEnabled,
            [nameof(MapLibreMap.TrackCameraPosition)] = MapTrackCameraPosition,
            [nameof(MapLibreMap.ZoomGesturesEnabled)] = MapZoomGesturesEnabled,
            [nameof(MapLibreMap.MyLocationEnabled)] = MapMyLocationEnabled,
            [nameof(MapLibreMap.MyLocationTrackingMode)] = MapMyLocationTrackingMode,
            [nameof(MapLibreMap.MyLocationRenderMode)] = MapMyLocationRenderMode,
            [nameof(MapLibreMap.LogoViewMargins)] = MapLogoViewMargins,
            [nameof(MapLibreMap.CompassGravity)] = MapCompassGravity,
            [nameof(MapLibreMap.CompassViewMargins)] = MapCompassViewMargins,
            [nameof(MapLibreMap.AttributionButtonGravity)] = MapAttributionButtonGravity,
            [nameof(MapLibreMap.AttributionButtonMargins)] = MapAttributionButtonMargins,
        };
        
    public static void MapStyleUrl(MapLibreMapHandler handler, MapLibreMap view)
    {
        handler.UpdateStyleUrl(view.StyleUrl);
    }
    
    public static void MapMinZoom(MapLibreMapHandler handler, MapLibreMap view)
    {
        handler.UpdateMinMaxZoomPreference(view.MinZoom, view.MaxZoom);
    }
    
    public static void MapMaxZoom(MapLibreMapHandler handler, MapLibreMap view)
    {
        handler.UpdateMinMaxZoomPreference(view.MinZoom, view.MaxZoom);
    }
    
    public static void MapRotateGestureEnabled(MapLibreMapHandler handler, MapLibreMap view)
    {
        handler.UpdateRotateGestureEnabled(view.RotateGestureEnabled);
    }
    
    public static void MapScrollGesturesEnabled(MapLibreMapHandler handler, MapLibreMap view)
    {
        handler.UpdateScrollGesturesEnabled(view.ScrollGesturesEnabled);
    }
    
    public static void MapTiltGesturesEnabled(MapLibreMapHandler handler, MapLibreMap view)
    {
        handler.UpdateTiltGesturesEnabled(view.TiltGesturesEnabled);
    }
    
    public static void MapTrackCameraPosition(MapLibreMapHandler handler, MapLibreMap view)
    {
        handler.UpdateTrackCameraPosition(view.TrackCameraPosition);
    }
    
    public static void MapZoomGesturesEnabled(MapLibreMapHandler handler, MapLibreMap view)
    {
        handler.UpdateZoomGesturesEnabled(view.ZoomGesturesEnabled);
    }
    
    public static void MapMyLocationEnabled(MapLibreMapHandler handler, MapLibreMap view)
    {
        handler.UpdateMyLocationEnabled(view.MyLocationEnabled);
    }
    
    public static void MapMyLocationTrackingMode(MapLibreMapHandler handler, MapLibreMap view) => handler.UpdateMyLocationTrackingMode(view.MyLocationTrackingMode);
    
    public static void MapMyLocationRenderMode(MapLibreMapHandler handler, MapLibreMap view) => handler.UpdateMyLocationRenderMode(view.MyLocationRenderMode);
    
    public static void MapLogoViewMargins(MapLibreMapHandler handler, MapLibreMap view) => handler.UpdateLogoViewMargins(view.LogoViewMargins);
    
    public static void MapCompassGravity(MapLibreMapHandler handler, MapLibreMap view) => handler.UpdateCompassGravity(view.CompassGravity);
    
    public static void MapCompassViewMargins(MapLibreMapHandler handler, MapLibreMap view) => handler.UpdateCompassViewMargins(view.CompassViewMargins);
    
    public static void MapAttributionButtonGravity(MapLibreMapHandler handler, MapLibreMap view) => handler.UpdateAttributionButtonGravity(view.AttributionButtonGravity);
    
    public static void MapAttributionButtonMargins(MapLibreMapHandler handler, MapLibreMap view) => handler.UpdateAttributionButtonMargins(view.AttributionButtonMargins);
}