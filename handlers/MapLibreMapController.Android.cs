using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Maui.MapLibre.Handlers.Android;
using Org.Maplibre.Android.Constants;
using Org.Maplibre.Android.Geometry;
using Org.Maplibre.Android.Location;
using Org.Maplibre.Android.Location.Engine;
using Org.Maplibre.Android.Location.Modes;
using Org.Maplibre.Android.Maps;
using Org.Maplibre.Android.Style.Expressions;
using Org.Maplibre.Android.Style.Layers;
using Org.Maplibre.Android.Style.Sources;
using Org.Maplibre.Geojson;
using Application = Android.App.Application;
using Location = Android.Locations.Location;
using Exception = System.Exception;
using Object = Java.Lang.Object;
using Style = Org.Maplibre.Android.Maps.Style;

namespace Maui.MapLibre.Handlers;

public class MapLibreMapController
    : Object,
    IMapLibreMapOptionsSink,
    Application.IActivityLifecycleCallbacks,
    IOnMapReadyCallback,
    IOnCameraTrackingChangedListener,
    Org.Maplibre.Android.Maps.MapLibreMap.IOnCameraIdleListener,
    Org.Maplibre.Android.Maps.MapLibreMap.IOnCameraMoveListener,
    Org.Maplibre.Android.Maps.MapLibreMap.IOnCameraMoveStartedListener,
    MapView.IOnDidBecomeIdleListener,
    Org.Maplibre.Android.Maps.MapLibreMap.IOnMapClickListener,
    Org.Maplibre.Android.Maps.MapLibreMap.IOnMapLongClickListener,
    Style.IOnStyleLoaded
{
    private readonly Context _context;
    private readonly MapView _mapView;
    private Org.Maplibre.Android.Maps.MapLibreMap? _mapLibreMap;
    private Style? _style;
    private MapLibreMapOptions _options;
    private string? _styleString;
    private LatLngBounds? _bounds;
    private LocationComponent? _locationComponent;
    
    private bool _dragEnabled;
    private bool _trackCameraPosition;
    private bool _myLocationEnabled;
    private int _myLocationTrackingMode;
    private int _myLocationRenderMode;
    
    private readonly List<string> _interactiveFeatureLayerIds = new();
    private readonly Dictionary<string, FeatureCollection?> _addedFeaturesByLayer = new();
    
    public MapView View => _mapView;
    
    // Events
    public event Action<Org.Maplibre.Android.Maps.MapLibreMap>? OnMapReadyReceived;
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

    private LocationEngineCallbackListener? _onLocationEngineCallback;
    
    public MapLibreMapController(Context context, MapLibreMapOptions options, bool dragEnabled, string? styleString)
    {
        Org.Maplibre.Android.MapLibre.GetInstance(context);
        _context = context;
        _options = options;
        _mapView = new MapView(context, options);
        _styleString = styleString;
    }

    public void Init()
    {
        _mapView.GetMapAsync(this);
    }

    public void OnMapReady(Org.Maplibre.Android.Maps.MapLibreMap p0)
    {
        _mapLibreMap = p0;

        _mapLibreMap.AddOnCameraMoveStartedListener(this);
        _mapLibreMap.AddOnCameraMoveListener(this);
        _mapLibreMap.AddOnCameraIdleListener(this);
        _mapView.AddOnDidBecomeIdleListener(this);

        SetStyleString(_styleString);

        OnMapReadyReceived?.Invoke(p0);
    }

    public void OnStyleLoaded(Style p0)
    {
        if (_mapLibreMap == null) return;
        _style = p0;

        UpdateMyLocationEnabled(p0);

        if (_bounds != null)
        {
            _mapLibreMap.SetLatLngBoundsForCameraTarget(_bounds);
        }


        _mapLibreMap.AddOnMapClickListener(this);
        _mapLibreMap.AddOnMapLongClickListener(this);

        OnStyleLoadedReceived?.Invoke(p0);
    }

    public bool OnMapLongClick(LatLng p0)
    {
        return OnMapLongClickReceived?.Invoke(p0) ?? false;
    }

    public bool OnMapClick(LatLng p0)
    {
        return OnMapClickReceived?.Invoke(p0) ?? false;
    }

    public void OnDidBecomeIdle()
    {
        OnDidBecomeIdleReceived?.Invoke();
    }

    public void OnCameraMoveStarted(int p0)
    {
        OnCameraMoveStartedReceived?.Invoke(p0);
    }

    public void OnCameraMove()
    {
        OnCameraMoveReceived?.Invoke();
    }

    public void OnCameraIdle()
    {
        OnCameraIdleReceived?.Invoke();
    }

    public void OnCameraTrackingChanged(int p0)
    {
        OnCameraTrackingChangedReceived?.Invoke(p0);
    }

    public void OnCameraTrackingDismissed()
    {
        OnCameraTrackingDismissedReceived?.Invoke();
    }
    
    private void OnUserLocationUpdate(Location? location) {
        if (location == null) return;
        OnUserLocationUpdateReceived?.Invoke(location);
    }
    
    public void SetCameraTargetBounds(LatLngBounds? newBounds)
    {
        _bounds = newBounds;
        _mapLibreMap?.SetLatLngBoundsForCameraTarget(newBounds);
    }
    
    public void AddGeoJsonSource(string sourceName, string source)
    {
        var featureCollection = FeatureCollection.FromJson(source);
        if (featureCollection == null) return;
        
        var geoJsonSource = new GeoJsonSource(sourceName, featureCollection);
        _addedFeaturesByLayer[sourceName] = featureCollection;
        
        _style?.AddSource(geoJsonSource);
    }

    public void SetGeoJsonSource(string sourceName, string source)
    {
        var featureCollection = FeatureCollection.FromJson(source);
        if (featureCollection == null) return;
        
        var geoJsonSource = (GeoJsonSource?) _style?.GetSourceAs(sourceName);
        _addedFeaturesByLayer[sourceName] = featureCollection;

        geoJsonSource?.SetGeoJson(featureCollection);
    }
    
    public void RemoveLayer(string layerId)
    {
        if (_style == null) return;
        _style.RemoveLayer(layerId);
        _interactiveFeatureLayerIds.Remove(layerId);
    }
    
    public void RemoveSource(string sourceId)
    {
        _style?.RemoveSource(sourceId);
    }
    
    public void SetGeoJsonFeature(string sourceName, string geojsonFeature)
    {
        var feature = Feature.FromJson(geojsonFeature);
        if (feature == null) return;
        
        var featureCollection = _addedFeaturesByLayer[sourceName];
        if (featureCollection == null) return;
        
        var geoJsonSource = (GeoJsonSource?) _style?.GetSourceAs(sourceName);
        if (geoJsonSource == null) return;
        
        var features = featureCollection.Features();
        if (features == null) return;
        for (var i = 0; i < features.Count; i++)
        {
            var id = features[i].Id();
            if (id == null || !id.Equals(feature.Id())) continue;
            features[i] = feature;
            break;
        }
        geoJsonSource.SetGeoJson(featureCollection);
    }
    
    public void AddSymbolLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        PropertyValue[] properties,
        Expression? filter,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (_style == null) return;
        var symbolLayer = new SymbolLayer(layerName, sourceName);
        symbolLayer.SetProperties(properties);
        if (sourceLayer != null)
        {
            symbolLayer.SourceLayer = sourceLayer;
        }
        if (minZoom != 0)
        {
            symbolLayer.MinZoom = minZoom;
        }
        if (maxZoom != 0)
        {
            symbolLayer.MaxZoom = maxZoom;
        }
        if (filter != null)
        {
            symbolLayer.Filter = filter;
        }
        if (belowLayerId != null)
        {
            _style.AddLayerBelow(symbolLayer, belowLayerId);
        }
        else
        {
            _style.AddLayer(symbolLayer);
        }
        if (enableInteraction)
        {
            _interactiveFeatureLayerIds.Add(layerName);
        }
    }
    
    public void AddLineLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        IDictionary<string, object?> properties,
        Expression? filter,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (_style == null) return;
        var propertyValues = properties.Select(x => new PropertyValue(x.Key, x.Value as Object)).ToArray();
        var lineLayer = new LineLayer(layerName, sourceName);
        lineLayer.SetProperties(propertyValues);
        if (sourceLayer != null)
        {
            lineLayer.SourceLayer = sourceLayer;
        }
        if (minZoom != 0)
        {
            lineLayer.MinZoom = minZoom;
        }
        if (maxZoom != 0)
        {
            lineLayer.MaxZoom = maxZoom;
        }
        if (filter != null)
        {
            lineLayer.Filter = filter;
        }
        if (belowLayerId != null)
        {
            _style.AddLayerBelow(lineLayer, belowLayerId);
        }
        else
        {
            _style.AddLayer(lineLayer);
        }
        if (enableInteraction)
        {
            _interactiveFeatureLayerIds.Add(layerName);
        }
    }
    
    public void AddFillLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        PropertyValue[] properties,
        Expression? filter,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (_style == null) return;
        var fillLayer = new FillLayer(layerName, sourceName);
        fillLayer.SetProperties(properties);
        if (sourceLayer != null)
        {
            fillLayer.SourceLayer = sourceLayer;
        }
        if (minZoom != 0)
        {
            fillLayer.MinZoom = minZoom;
        }
        if (maxZoom != 0)
        {
            fillLayer.MaxZoom = maxZoom;
        }
        if (filter != null)
        {
            fillLayer.Filter = filter;
        }
        if (belowLayerId != null)
        {
            _style.AddLayerBelow(fillLayer, belowLayerId);
        }
        else
        {
            _style.AddLayer(fillLayer);
        }
        if (enableInteraction)
        {
            _interactiveFeatureLayerIds.Add(layerName);
        }
    }
    
    public void AddFillExtrusionLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        PropertyValue[] properties,
        Expression? filter,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (_style == null) return;
        var fillLayer = new FillExtrusionLayer(layerName, sourceName);
        fillLayer.SetProperties(properties);
        if (sourceLayer != null)
        {
            fillLayer.SourceLayer = sourceLayer;
        }
        if (minZoom != 0)
        {
            fillLayer.MinZoom = minZoom;
        }
        if (maxZoom != 0)
        {
            fillLayer.MaxZoom = maxZoom;
        }
        if (filter != null)
        {
            fillLayer.Filter = filter;
        }
        if (belowLayerId != null)
        {
            _style.AddLayerBelow(fillLayer, belowLayerId);
        }
        else
        {
            _style.AddLayer(fillLayer);
        }
        if (enableInteraction)
        {
            _interactiveFeatureLayerIds.Add(layerName);
        }
    }
    
    public void AddCircleLayer(
        string layerName,
        string sourceName,
        string? belowLayerId,
        string? sourceLayer,
        PropertyValue[] properties,
        Expression? filter,
        float minZoom = 0,
        float maxZoom = 0,
        bool enableInteraction = false)
    {
        if (_style == null) return;
        var circleLayer = new CircleLayer(layerName, sourceName);
        circleLayer.SetProperties(properties);
        if (sourceLayer != null)
        {
            circleLayer.SourceLayer = sourceLayer;
        }
        if (minZoom != 0)
        {
            circleLayer.MinZoom = minZoom;
        }
        if (maxZoom != 0)
        {
            circleLayer.MaxZoom = maxZoom;
        }
        if (filter != null)
        {
            circleLayer.Filter = filter;
        }
        if (belowLayerId != null)
        {
            _style.AddLayerBelow(circleLayer, belowLayerId);
        }
        else
        {
            _style.AddLayer(circleLayer);
        }
        if (enableInteraction)
        {
            _interactiveFeatureLayerIds.Add(layerName);
        }
    }
    
    public void AddRasterLayer(
        string layerName,
        string sourceName,
        PropertyValue[] properties,
        Expression? filter,
        float minZoom = 0,
        float maxZoom = 0,
        string? belowLayerId = null)
    {
        if (_style == null) return;
        var rasterLayer = new RasterLayer(layerName, sourceName);
        rasterLayer.SetProperties(properties);
        if (minZoom != 0)
        {
            rasterLayer.MinZoom = minZoom;
        }
        if (maxZoom != 0)
        {
            rasterLayer.MaxZoom = maxZoom;
        }
        if (belowLayerId != null)
        {
            _style.AddLayerBelow(rasterLayer, belowLayerId);
        }
        else
        {
            _style.AddLayer(rasterLayer);
        }
    }
    
    public void AddHillshadeLayer(
        string layerName,
        string sourceName,
        PropertyValue[] properties,
        Expression? filter,
        float minZoom = 0,
        float maxZoom = 0,
        string? belowLayerId = null)
    {
        if (_style == null) return;
        var hillshadeLayer = new HillshadeLayer(layerName, sourceName);
        hillshadeLayer.SetProperties(properties);
        if (minZoom != 0)
        {
            hillshadeLayer.MinZoom = minZoom;
        }
        if (maxZoom != 0)
        {
            hillshadeLayer.MaxZoom = maxZoom;
        }
        if (belowLayerId != null)
        {
            _style.AddLayerBelow(hillshadeLayer, belowLayerId);
        }
        else
        {
            _style.AddLayer(hillshadeLayer);
        }
    }
    
    public void AddHeatmapLayer(
        string layerName,
        string sourceName,
        PropertyValue[] properties,
        Expression? filter,
        float minZoom = 0,
        float maxZoom = 0,
        string? belowLayerId = null)
    {
        if (_style == null) return;
        var heatmapLayer = new HeatmapLayer(layerName, sourceName);
        heatmapLayer.SetProperties(properties);
        if (minZoom != 0)
        {
            heatmapLayer.MinZoom = minZoom;
        }
        if (maxZoom != 0)
        {
            heatmapLayer.MaxZoom = maxZoom;
        }
        if (belowLayerId != null)
        {
            _style.AddLayerBelow(heatmapLayer, belowLayerId);
        }
        else
        {
            _style.AddLayer(heatmapLayer);
        }
    }

    public void SetStyleString(string? styleString)
    {
        ClearLocationComponentLayer();
        if (styleString == null) return;
        _styleString = styleString.Trim();
        if (_mapLibreMap == null) return;

        if (_styleString.Length == 0) return;

        if (_styleString.StartsWith("{") || _styleString.StartsWith("["))
        {
            _mapLibreMap.SetStyle(new Style.Builder().FromJson(styleString), this);
            return;
        }

        if (_styleString.StartsWith("/"))
        {
            // Absolute path
            _mapLibreMap.SetStyle(
                new Style.Builder().FromUri("file://" + styleString), this);
            return;
        }

        if (!_styleString.StartsWith("http://")
            && !_styleString.StartsWith("https://")
            && !_styleString.StartsWith("mapbox://"))
        {
            // We are assuming that the style will be loaded from an asset here.
            // String key = MapLibreMapsPlugin.flutterAssets.getAssetFilePathByName(styleString);
            //_mapLibreMap.setStyle(new Style.Builder().fromUri("asset://" + key), onStyleLoadedCallback);
            return;
        }

        _mapLibreMap.SetStyle(new Style.Builder().FromUri(_styleString), this);
    }

    
    public void SetLocationEngineProperties(LocationEngineRequest? locationEngineRequest){
        if (_locationComponent == null) return;
        if (locationEngineRequest == null) return;

        _locationComponent.LocationEngine = locationEngineRequest.Priority == LocationEngineRequest.PriorityHighAccuracy 
            ? new LocationEngineProxy(new MapLibreGpsLocationEngine(_context)) 
            : LocationEngineDefault.Instance.GetDefaultLocationEngine(_context);
        _locationComponent.LocationEngineRequest = locationEngineRequest;
    }

    
    public void SetCompassEnabled(bool compassEnabled) {
        if (_mapLibreMap == null) return;
        _mapLibreMap.UiSettings.CompassEnabled = compassEnabled;
    }

    
    public void SetTrackCameraPosition(bool trackCameraPosition) {
        _trackCameraPosition = trackCameraPosition;
    }

    
    public void SetRotateGesturesEnabled(bool rotateGesturesEnabled) {
        if (_mapLibreMap == null) return;
        _mapLibreMap.UiSettings.RotateGesturesEnabled =rotateGesturesEnabled;
    }

    
    public void SetScrollGesturesEnabled(bool scrollGesturesEnabled) {
        if (_mapLibreMap == null) return;
        _mapLibreMap.UiSettings.ScrollGesturesEnabled = scrollGesturesEnabled;
    }

    
    public void SetTiltGesturesEnabled(bool tiltGesturesEnabled) {
        if (_mapLibreMap == null) return;
        _mapLibreMap.UiSettings.TiltGesturesEnabled = tiltGesturesEnabled;
    }

    
    public void SetMinMaxZoomPreference(double? min, double? max) {
        if (_mapLibreMap == null) return;
        _mapLibreMap.SetMinPitchPreference(min ?? MapLibreConstants.MinimumZoom);
        _mapLibreMap.SetMaxZoomPreference(max ?? MapLibreConstants.MaximumZoom);
    }

    
    public void SetZoomGesturesEnabled(bool zoomGesturesEnabled) {
        if (_mapLibreMap == null) return;
        _mapLibreMap.UiSettings.ZoomGesturesEnabled = zoomGesturesEnabled;
    }

    
    public void SetMyLocationEnabled(bool myLocationEnabled) {
        if (_myLocationEnabled == myLocationEnabled) {
            return;
        }
        _myLocationEnabled = myLocationEnabled;
        if (_mapLibreMap != null) {
            UpdateMyLocationEnabled(_style);
        }
    }

    
    public void SetMyLocationTrackingMode(int myLocationTrackingMode) {
        if (_mapLibreMap != null) {
            // ensure that location is trackable
            UpdateMyLocationEnabled(_style);
        }
        if (_myLocationTrackingMode == myLocationTrackingMode) {
            return;
        }
        _myLocationTrackingMode = myLocationTrackingMode;
        if (_mapLibreMap != null && _locationComponent != null) {
            UpdateMyLocationTrackingMode();
        }
    }

    
    public void SetMyLocationRenderMode(int myLocationRenderMode) {
        if (_myLocationRenderMode == myLocationRenderMode) {
            return;
        }
        _myLocationRenderMode = myLocationRenderMode;
        if (_mapLibreMap != null && _locationComponent != null) {
            UpdateMyLocationRenderMode();
        }
    }

    public void SetLogoViewMargins(int x, int y) {
        if (_mapLibreMap == null) return;
        _mapLibreMap.UiSettings.SetLogoMargins(x, 0, 0, y);
    }

    
    public void SetCompassGravity(int gravity) {
        if (_mapLibreMap == null) return;
        switch (gravity) {
            case 0:
                _mapLibreMap.UiSettings.CompassGravity = (int) GravityFlags.Top | (int) GravityFlags.Start;
                break;
            case 1:
                _mapLibreMap.UiSettings.CompassGravity = (int) GravityFlags.Top | (int) GravityFlags.End;
                break;
            case 2:
                _mapLibreMap.UiSettings.CompassGravity = (int) GravityFlags.Bottom | (int) GravityFlags.Start;
                break;
            case 3:
                _mapLibreMap.UiSettings.CompassGravity = (int) GravityFlags.Bottom | (int) GravityFlags.End;
                break;
        }
    }

    
    public void SetCompassViewMargins(int x, int y) {
        if (_mapLibreMap == null) return;
        switch ((GravityFlags) _mapLibreMap.UiSettings.CompassGravity) {
            case GravityFlags.Top | GravityFlags.Start:
                _mapLibreMap.UiSettings.SetCompassMargins(x, y, 0, 0);
                break;
            case GravityFlags.Top | GravityFlags.End:
                _mapLibreMap.UiSettings.SetCompassMargins(0, y, x, 0);
                break;
            case GravityFlags.Bottom | GravityFlags.Start:
                _mapLibreMap.UiSettings.SetCompassMargins(x, 0, 0, y);
                break;
            case GravityFlags.Bottom | GravityFlags.End:
                _mapLibreMap.UiSettings.SetCompassMargins(0, 0, x, y);
                break;
        }
    }

    
    public void SetAttributionButtonGravity(int gravity) {
        if (_mapLibreMap == null) return;
        switch (gravity) {
            case 0:
                _mapLibreMap.UiSettings.AttributionGravity = (int) GravityFlags.Top | (int) GravityFlags.Start;
                break;
            case 1:
                _mapLibreMap.UiSettings.AttributionGravity = (int) GravityFlags.Top | (int) GravityFlags.End;
                break;
            case 2:
                _mapLibreMap.UiSettings.AttributionGravity = (int) GravityFlags.Bottom | (int) GravityFlags.Start;
                break;
            case 3:
                _mapLibreMap.UiSettings.AttributionGravity = (int) GravityFlags.Bottom | (int) GravityFlags.End;
                break;
        }
    }

    
    public void SetAttributionButtonMargins(int x, int y) {
        if (_mapLibreMap == null) return;
        switch ((GravityFlags) _mapLibreMap.UiSettings.AttributionGravity) {
            case GravityFlags.Top | GravityFlags.Start:
                _mapLibreMap.UiSettings.SetAttributionMargins(x, y, 0, 0);
                break;
            case GravityFlags.Top | GravityFlags.End:
                _mapLibreMap.UiSettings.SetAttributionMargins(0, y, x, 0);
                break;
            case GravityFlags.Bottom | GravityFlags.Start:
                _mapLibreMap.UiSettings.SetAttributionMargins(x, 0, 0, y);
                break;
            case GravityFlags.Bottom | GravityFlags.End:
                _mapLibreMap.UiSettings.SetAttributionMargins(0, 0, x, y);
                break;
        }
    }

    private async void EnableLocationComponent(Style? style)
    {
        try
        {
            var hasLocationPermission = await HasLocationPermission();
            if (style == null) return;
            if (_mapLibreMap == null) return;
            if (!hasLocationPermission) return;

            _locationComponent = _mapLibreMap.LocationComponent;

            var options = LocationComponentActivationOptions.InvokeBuilder(_context, style)
                .LocationComponentOptions(BuildLocationComponentOptions(style))
                ?.Build();
            if (options == null) return;
            
            _locationComponent.ActivateLocationComponent(options);
            _locationComponent.LocationComponentEnabled = true;
            _locationComponent.SetMaxAnimationFps(30);
            UpdateMyLocationTrackingMode();
            UpdateMyLocationRenderMode();
            _locationComponent.AddOnCameraTrackingChangedListener(this);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private void UpdateMyLocationEnabled(Style? style)
    {
        if (_locationComponent == null && style != null && _myLocationEnabled)
        {
            EnableLocationComponent(style);
        }

        if (_myLocationEnabled)
        {
            StartListeningForLocationUpdates();
        }
        else
        {
            StopListeningForLocationUpdates();
        }

        if (_locationComponent != null)
        {
            _locationComponent.LocationComponentEnabled = _myLocationEnabled;
        }
    }

    private void StartListeningForLocationUpdates()
    {
        if (_onLocationEngineCallback != null) return;
        if (_locationComponent == null) return;
        if (!_locationComponent.IsLocationComponentActivated) return;
        if (_locationComponent.LocationEngine == null) return;

        _onLocationEngineCallback = new LocationEngineCallbackListener(result =>
        {
            OnUserLocationUpdate(result.LastLocation);
        }, _ => { });
        
        _locationComponent
            .LocationEngine
            .RequestLocationUpdates(_locationComponent.LocationEngineRequest, _onLocationEngineCallback, null);
    }

    private void StopListeningForLocationUpdates()
    {
        if (_onLocationEngineCallback == null) return;
        if (_locationComponent == null) return;
        if (!_locationComponent.IsLocationComponentActivated) return;
        if (_locationComponent.LocationEngine == null) return;
        
        _locationComponent.LocationEngine.RemoveLocationUpdates(_onLocationEngineCallback);
        _onLocationEngineCallback = null;
    }

    private void UpdateMyLocationTrackingMode()
    {
        if (_locationComponent == null) return;
        int[] trackingModes =
        [
            CameraMode.None, CameraMode.Tracking, CameraMode.TrackingCompass, CameraMode.TrackingGps
        ];
        var trackingMode = trackingModes[_myLocationTrackingMode];
        _locationComponent.SetCameraMode(trackingMode, null);
    }

    private void UpdateMyLocationRenderMode()
    {
        if (_locationComponent == null) return;
        int[] mapboxRenderModes = [RenderMode.Normal, RenderMode.Compass, RenderMode.Gps];
        _locationComponent.RenderMode = mapboxRenderModes[_myLocationRenderMode];
    }
    
    private void UpdateLocationComponentLayer(Style? style) {
        var options = BuildLocationComponentOptions(style);
        if (!LocationComponentRequiresUpdate()) return;
        _locationComponent?.ApplyStyle(options);
    }

    private void ClearLocationComponentLayer() {
        var options = BuildLocationComponentOptions(null);
        _locationComponent?.ApplyStyle(options);
    }
    
    private bool LocationComponentRequiresUpdate() {
        var lastLayerId = GetLastLayerOnStyle(_style);
        return lastLayerId != null && !lastLayerId.Equals("mapbox-location-bearing-layer");
    }

    private LocationComponentOptions BuildLocationComponentOptions(Style? style)
    {
        var optionsBuilder =
            LocationComponentOptions.InvokeBuilder(_context);
        optionsBuilder.TrackingGesturesManagement(true);

        var lastLayerId = GetLastLayerOnStyle(style);
        if (lastLayerId != null) {
            optionsBuilder.LayerAbove(lastLayerId);
        }
        return optionsBuilder.Build();
    }
    
    private static string? GetLastLayerOnStyle(Style? style) {
        if (style == null) return null;
        var layers = style.Layers;
        if (layers.Count == 0) return null;
        var lastLayer = layers[^1];
        return lastLayer.Id;
    }

    private static async Task<bool> HasLocationPermission()
    {
        return await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Granted
               || await Permissions.CheckStatusAsync<Permissions.LocationAlways>() == PermissionStatus.Granted;
    }

    public void OnActivityCreated(Activity activity, Bundle? savedInstanceState)
    {
        _mapView.OnCreate(savedInstanceState);
    }

    public void OnActivityDestroyed(Activity activity)
    {
        _mapView.OnDestroy();
    }

    public void OnActivityPaused(Activity activity)
    {
        _mapView.OnPause();
    }

    public void OnActivityResumed(Activity activity)
    {
        _mapView.OnResume();
    }

    public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
    {
        _mapView.OnSaveInstanceState(outState);
    }

    public void OnActivityStarted(Activity activity)
    {
        _mapView.OnStart();
    }

    public void OnActivityStopped(Activity activity)
    {
        _mapView.OnStop();
    }
}