using System.Text.Json;
using GeoJSON.Text.Feature;
using Maui.MapLibre.Handlers.Properties;
using Microsoft.Maui.Handlers;

namespace Maui.MapLibre.Handlers;

// All the code in this file is included in all platforms.
public class MapLibreMap : StackLayout
{
    public static readonly BindableProperty StyleUrlProperty = BindableProperty.Create(nameof(StyleUrl), typeof(string), typeof(MapLibreMap));
    public static readonly BindableProperty MinZoomProperty = BindableProperty.Create(nameof(MinZoom), typeof(float), typeof(MapLibreMap));
    public static readonly BindableProperty MaxZoomProperty = BindableProperty.Create(nameof(MaxZoom), typeof(float), typeof(MapLibreMap));
    public static readonly BindableProperty RotateGestureEnabledProperty = BindableProperty.Create(nameof(RotateGestureEnabled), typeof(bool), typeof(MapLibreMap));
    public static readonly BindableProperty ScrollGesturesEnabledProperty = BindableProperty.Create(nameof(ScrollGesturesEnabled), typeof(bool), typeof(MapLibreMap));
    public static readonly BindableProperty TiltGesturesEnabledProperty = BindableProperty.Create(nameof(TiltGesturesEnabled), typeof(bool), typeof(MapLibreMap));
    public static readonly BindableProperty TrackCameraPositionProperty = BindableProperty.Create(nameof(TrackCameraPosition), typeof(bool), typeof(MapLibreMap));
    public static readonly BindableProperty ZoomGesturesEnabledProperty = BindableProperty.Create(nameof(ZoomGesturesEnabled), typeof(bool), typeof(MapLibreMap));
    public static readonly BindableProperty MyLocationEnabledProperty = BindableProperty.Create(nameof(MyLocationEnabled), typeof(bool), typeof(MapLibreMap));
    public static readonly BindableProperty MyLocationTrackingModeProperty = BindableProperty.Create(nameof(MyLocationTrackingMode), typeof(int), typeof(MapLibreMap));
    public static readonly BindableProperty MyLocationRenderModeProperty = BindableProperty.Create(nameof(MyLocationRenderMode), typeof(int), typeof(MapLibreMap));
    public static readonly BindableProperty LogoViewMarginsProperty = BindableProperty.Create(nameof(LogoViewMargins), typeof(int?[]), typeof(MapLibreMap));
    public static readonly BindableProperty CompassGravityProperty = BindableProperty.Create(nameof(CompassGravity), typeof(int), typeof(MapLibreMap));
    public static readonly BindableProperty CompassViewMarginsProperty = BindableProperty.Create(nameof(CompassViewMargins), typeof(int?[]), typeof(MapLibreMap));
    public static readonly BindableProperty AttributionButtonGravityProperty = BindableProperty.Create(nameof(AttributionButtonGravity), typeof(int), typeof(MapLibreMap));
    public static readonly BindableProperty AttributionButtonMarginsProperty = BindableProperty.Create(nameof(AttributionButtonMargins), typeof(int?[]), typeof(MapLibreMap));
        

    public string StyleUrl
    {
        get => (string)GetValue(StyleUrlProperty);
        set => SetValue(StyleUrlProperty, value);
    }
    
    public float MinZoom
    {
        get => (float)GetValue(MinZoomProperty);
        set => SetValue(MinZoomProperty, value);
    }
    
    public float MaxZoom
    {
        get => (float)GetValue(MaxZoomProperty);
        set => SetValue(MaxZoomProperty, value);
    }
    
    public bool RotateGestureEnabled
    {
        get => (bool)GetValue(RotateGestureEnabledProperty);
        set => SetValue(RotateGestureEnabledProperty, value);
    }
    
    public bool ScrollGesturesEnabled
    {
        get => (bool)GetValue(ScrollGesturesEnabledProperty);
        set => SetValue(ScrollGesturesEnabledProperty, value);
    }
    
    public bool TiltGesturesEnabled
    {
        get => (bool)GetValue(TiltGesturesEnabledProperty);
        set => SetValue(TiltGesturesEnabledProperty, value);
    }
    
    public bool TrackCameraPosition
    {
        get => (bool)GetValue(TrackCameraPositionProperty);
        set => SetValue(TrackCameraPositionProperty, value);
    }
    
    public bool ZoomGesturesEnabled
    {
        get => (bool)GetValue(ZoomGesturesEnabledProperty);
        set => SetValue(ZoomGesturesEnabledProperty, value);
    }
    
    public bool MyLocationEnabled
    {
        get => (bool)GetValue(MyLocationEnabledProperty);
        set => SetValue(MyLocationEnabledProperty, value);
    }
    
    public int MyLocationTrackingMode
    {
        get => (int)GetValue(MyLocationTrackingModeProperty);
        set => SetValue(MyLocationTrackingModeProperty, value);
    }
    
    public int MyLocationRenderMode
    {
        get => (int)GetValue(MyLocationRenderModeProperty);
        set => SetValue(MyLocationRenderModeProperty, value);
    }
    
    public int?[]? LogoViewMargins
    {
        get => (int?[])GetValue(LogoViewMarginsProperty);
        set => SetValue(LogoViewMarginsProperty, value);
    }
    
    public int CompassGravity
    {
        get => (int)GetValue(CompassGravityProperty);
        set => SetValue(CompassGravityProperty, value);
    }
    
    public int?[]? CompassViewMargins
    {
        get => (int?[])GetValue(CompassViewMarginsProperty);
        set => SetValue(CompassViewMarginsProperty, value);
    }
    
    public int AttributionButtonGravity
    {
        get => (int)GetValue(AttributionButtonGravityProperty);
        set => SetValue(AttributionButtonGravityProperty, value);
    }
    
    public int?[]? AttributionButtonMargins
    {
        get => (int?[])GetValue(AttributionButtonMarginsProperty);
        set => SetValue(AttributionButtonMarginsProperty, value);
    }

    public void AddGeoJsonSource(string sourceName, FeatureCollection collection)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        var json = JsonSerializer.Serialize(collection);
        controller.AddGeoJsonSource(sourceName, json);
    }

    public void AddLineLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        LineLayerProperties properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        var propertyValues = properties.ToDictionary();
        controller.AddLineLayer(layerName, sourceName, belowLayerId, sourceLayer, propertyValues, null, minZoom, maxZoom, enableInteraction);
    }
    
    public void AddSymbolLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        IDictionary<string, object?> properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        controller.AddLineLayer(layerName, sourceName, belowLayerId, sourceLayer, properties, null, minZoom, maxZoom, enableInteraction);
    }
    
    public void AddCircleLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        IDictionary<string, object?> properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        controller.AddLineLayer(layerName, sourceName, belowLayerId, sourceLayer, properties, null, minZoom, maxZoom, enableInteraction);
    }

    public void AddFillLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        IDictionary<string, object?> properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        controller.AddLineLayer(layerName, sourceName, belowLayerId, sourceLayer, properties, null, minZoom, maxZoom, enableInteraction);
    }
    
    public void AddRasterLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        IDictionary<string, object?> properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        controller.AddLineLayer(layerName, sourceName, belowLayerId, sourceLayer, properties, null, minZoom, maxZoom, enableInteraction);
    }

    public void AddHillshadeLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        IDictionary<string, object?> properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        controller.AddLineLayer(layerName, sourceName, belowLayerId, sourceLayer, properties, null, minZoom, maxZoom, enableInteraction);
    }

    public void AddHeatmapLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        IDictionary<string, object?> properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        controller.AddLineLayer(layerName, sourceName, belowLayerId, sourceLayer, properties, null, minZoom, maxZoom, enableInteraction);
    }
    
    public event EventHandler? MapReady;
    public event EventHandler? StyleLoaded;
    
    internal void OnMapReady()
    {
        MapReady?.Invoke(this, EventArgs.Empty);
    }
    
    internal void OnStyleLoaded()
    {
        StyleLoaded?.Invoke(this, EventArgs.Empty);
    }
}

public partial class MapLibreMapHandler() : ViewHandler<MapLibreMap, global::Android.Views.View>(PropertyMapper) 
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
    
    public static ICommandMapper<MapLibreMap, MapLibreMapHandler> CommandMapper = new CommandMapper<MapLibreMap, MapLibreMapHandler>(ViewCommandMapper)
    {
        
    }
    
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

