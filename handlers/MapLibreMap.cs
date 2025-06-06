﻿using System.Text.Json;
using System.Windows.Input;
using GeoJSON.Text.Feature;
using Maui.MapLibre.Handlers.EventArgs;
using Maui.MapLibre.Handlers.Geometry;
using Maui.MapLibre.Handlers.Properties;
using Microsoft.Maui.Handlers;
using Map = Maui.MapLibre.Handlers.Maps.Map;
using Style = Maui.MapLibre.Handlers.Maps.Style;

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
    public static readonly BindableProperty MapReadyCommandProperty = BindableProperty.Create(nameof(MapReadyCommand), typeof(ICommand), typeof(MapLibreMap));
    public static readonly BindableProperty StyleLoadedCommandProperty = BindableProperty.Create(nameof(StyleLoadedCommand), typeof(ICommand), typeof(MapLibreMap));
    public static readonly BindableProperty DidBecomeIdleCommandProperty = BindableProperty.Create(nameof(DidBecomeIdleCommand), typeof(ICommand), typeof(MapLibreMap));
    public static readonly BindableProperty CameraMoveStartedCommandProperty = BindableProperty.Create(nameof(CameraMoveStartedCommand), typeof(ICommand), typeof(MapLibreMap));
    public static readonly BindableProperty CameraMoveCommandProperty = BindableProperty.Create(nameof(CameraMoveCommand), typeof(ICommand), typeof(MapLibreMap));
    public static readonly BindableProperty CameraIdleCommandProperty = BindableProperty.Create(nameof(CameraIdleCommand), typeof(ICommand), typeof(MapLibreMap));
    public static readonly BindableProperty CameraTrackingChangedCommandProperty = BindableProperty.Create(nameof(CameraTrackingChangedCommand), typeof(ICommand), typeof(MapLibreMap));
    public static readonly BindableProperty CameraTrackingDismissedCommandProperty = BindableProperty.Create(nameof(CameraTrackingDismissedCommand), typeof(ICommand), typeof(MapLibreMap));
    public static readonly BindableProperty MapClickCommandProperty = BindableProperty.Create(nameof(MapClickCommand), typeof(ICommand), typeof(MapLibreMap));
    public static readonly BindableProperty MapLongClickCommandProperty = BindableProperty.Create(nameof(MapLongClickCommand), typeof(ICommand), typeof(MapLibreMap));
    public static readonly BindableProperty UserLocationUpdateCommandProperty = BindableProperty.Create(nameof(UserLocationUpdateCommand), typeof(ICommand), typeof(MapLibreMap));
    
    public ICommand? MapReadyCommand
    {
        get => (ICommand?)GetValue(MapReadyCommandProperty);
        set => SetValue(MapReadyCommandProperty, value);
    }

    public ICommand? StyleLoadedCommand
    {
        get => (ICommand?)GetValue(StyleLoadedCommandProperty);
        set => SetValue(StyleLoadedCommandProperty, value);
    }

    public ICommand? DidBecomeIdleCommand
    {
        get => (ICommand?)GetValue(DidBecomeIdleCommandProperty);
        set => SetValue(DidBecomeIdleCommandProperty, value);
    }

    public ICommand? CameraMoveStartedCommand
    {
        get => (ICommand?)GetValue(CameraMoveStartedCommandProperty);
        set => SetValue(CameraMoveStartedCommandProperty, value);
    }

    public ICommand? CameraMoveCommand
    {
        get => (ICommand?)GetValue(CameraMoveCommandProperty);
        set => SetValue(CameraMoveCommandProperty, value);
    }

    public ICommand? CameraIdleCommand
    {
        get => (ICommand?)GetValue(CameraIdleCommandProperty);
        set => SetValue(CameraIdleCommandProperty, value);
    }

    public ICommand? CameraTrackingChangedCommand
    {
        get => (ICommand?)GetValue(CameraTrackingChangedCommandProperty);
        set => SetValue(CameraTrackingChangedCommandProperty, value);
    }

    public ICommand? CameraTrackingDismissedCommand
    {
        get => (ICommand?)GetValue(CameraTrackingDismissedCommandProperty);
        set => SetValue(CameraTrackingDismissedCommandProperty, value);
    }

    public ICommand? MapClickCommand
    {
        get => (ICommand?)GetValue(MapClickCommandProperty);
        set =>  SetValue(MapClickCommandProperty, value);
    }

    public ICommand? MapLongClickCommand
    {
        get => (ICommand?)GetValue(MapLongClickCommandProperty);
        set =>   SetValue(MapLongClickCommandProperty, value);
    }

    public ICommand? UserLocationUpdateCommand
    {
        get => (ICommand?)GetValue(UserLocationUpdateCommandProperty);
        set =>   SetValue(UserLocationUpdateCommandProperty, value);
    }

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

    public void AddImageSource(string sourceName, string imageUri, LatLngQuad? coordinates)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        controller.AddImageSource(sourceName, imageUri, coordinates);
    }

    public void AddRasterSource(string sourceName, string? tileUrl, string[]? tileUrlTemplates, int tileSize,
        int minZoom, int maxZoom)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        controller.AddRasterSource(sourceName, tileUrl, tileUrlTemplates, tileSize, minZoom, maxZoom);
    }

    public void AddRasterDemSource(string sourceName, string? tileUrl, string[]? tileUrlTemplates, int tileSize, int minZoom, int maxZoom)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        controller.AddRasterDemSource(sourceName, tileUrl, tileUrlTemplates, tileSize, minZoom, maxZoom);
    }

    public void AddVectorSource(string sourceName, string? tileUrl, string[]? tileUrlTemplates,
        int minZoom, int maxZoom)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        controller.AddVectorSource(sourceName, tileUrl, tileUrlTemplates, minZoom, maxZoom);
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
        controller.AddLineLayer(layerName, sourceName, belowLayerId, sourceLayer, propertyValues, minZoom, maxZoom, enableInteraction);
    }
    
    public void AddSymbolLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        SymbolLayerProperties properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        var propertyValues = properties.ToDictionary();
        controller.AddSymbolLayer(layerName, sourceName, belowLayerId, sourceLayer, propertyValues, minZoom, maxZoom, enableInteraction);
    }
    
    public void AddCircleLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        CircleLayerProperties properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        var propertyValues = properties.ToDictionary();
        controller.AddCircleLayer(layerName, sourceName, belowLayerId, sourceLayer, propertyValues, minZoom, maxZoom, enableInteraction);
    }

    public void AddFillLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        FillLayerProperties properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        var propertyValues = properties.ToDictionary();
        controller.AddFillLayer(layerName, sourceName, belowLayerId, sourceLayer, propertyValues, minZoom, maxZoom, enableInteraction);
    }

    public void AddFillExtrusionLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        FillExtrusionLayerProperties properties,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        var propertyValues = properties.ToDictionary();
        controller.AddFillExtrusionLayer(layerName, sourceName, belowLayerId, sourceLayer, propertyValues, minZoom, maxZoom, enableInteraction);
    }
    
    public void AddRasterLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        RasterLayerProperties properties,
        float minZoom = 0,
        float maxZoom = 0)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        var propertyValues = properties.ToDictionary();
        controller.AddRasterLayer(layerName, sourceName, propertyValues, minZoom, maxZoom, belowLayerId);
    }

    public void AddHeatmapLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        HeatmapProperties properties,
        float minZoom = 0,
        float maxZoom = 0)
    {
        if (Handler is not MapLibreMapHandler handler) return;
        var controller = handler.Controller;
        var propertyValues = properties.ToDictionary();
        controller.AddHeatmapLayer(layerName, sourceName, propertyValues, minZoom, maxZoom, belowLayerId);
    }
    
    // TODO Map parameter may want to return the controller here. 
    public event EventHandler<MapReadyEventArgs>? MapReady;
    public event EventHandler? DidBecomeIdle;
    // TODO int parameter
    public event EventHandler<CameraMoveStartedEventArgs>? CameraMoveStarted;
    public event EventHandler? CameraMove;
    public event EventHandler? CameraIdle;
    // TODO int parameter
    public event EventHandler<CameraTrackingChangedEventArgs>? CameraTrackingChanged;
    public event EventHandler? CameraTrackingDismissed;
    // LatLng and bool parameter
    public event EventHandler<MapClickEventArgs>? MapClick;
    // LatLng and bool parameter
    public event EventHandler<MapClickEventArgs>? MapLongClick;
    // TODO style parameter
    public event EventHandler<StyleLoadedEventArgs>? StyleLoaded;
    // TODO Location parameter
    public event EventHandler<UserLocationUpdateEventArgs>? UserLocationUpdate;
    
    internal void OnMapReady(Map map)
    {
        var args = new MapReadyEventArgs
        {
            Map = map
        };
        MapReady?.Invoke(this, args);
        MapReadyCommand?.Execute(map);
    }
    
    internal void OnStyleLoaded(Style style)
    {
        var args = new StyleLoadedEventArgs
        {
            Style = style
        };
        StyleLoaded?.Invoke(this, args);
        StyleLoadedCommand?.Execute(style);
    }

    internal void OnDidBecomeIdle()
    {
        DidBecomeIdle?.Invoke(this, System.EventArgs.Empty);
        DidBecomeIdleCommand?.Execute(null);
    }

    internal void OnCameraMoveStarted(int reason)
    {
        var args = new CameraMoveStartedEventArgs
        {
            Reason = reason
        };
        CameraMoveStarted?.Invoke(this, args);
        CameraMoveCommand?.Execute(reason);
    }

    internal void OnCameraMove()
    {
        CameraMove?.Invoke(this, System.EventArgs.Empty);
        CameraMoveCommand?.Execute(null);
    }

    internal void OnCameraIdle()
    {
        CameraIdle?.Invoke(this, System.EventArgs.Empty);
        CameraIdleCommand?.Execute(null);
    }

    internal void OnCameraTrackingChanged(int mode)
    {
        var args = new CameraTrackingChangedEventArgs
        {
            Mode = mode
        };
        CameraTrackingChanged?.Invoke(this, args);
        CameraTrackingChangedCommand?.Execute(mode);
    }

    internal void OnCameraTrackingDismissed()
    {
        CameraTrackingDismissed?.Invoke(this, System.EventArgs.Empty);
        CameraTrackingDismissedCommand?.Execute(null);
    }

    internal bool OnMapClick(LatLng latLng)
    {
        var args = new MapClickEventArgs
        {
            LatLng = latLng
        };
        MapClick?.Invoke(this, args);
        MapClickCommand?.Execute(latLng);
        return false;
    }

    internal bool OnMapLongClick(LatLng latLng)
    {
        var args = new MapClickEventArgs
        {
            LatLng = latLng
        };
        MapLongClick?.Invoke(this, args);
        MapLongClickCommand?.Execute(latLng);
        return false;
    }

    internal void OnUserLocationUpdate(Location location)
    {
        var args = new UserLocationUpdateEventArgs
        {
            Location = location
        };
        UserLocationUpdate?.Invoke(this, args);
        UserLocationUpdateCommand?.Execute(location);
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

