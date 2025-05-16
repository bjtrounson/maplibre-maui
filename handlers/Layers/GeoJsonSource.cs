using GeoJSON.Text.Feature;

namespace Maui.MapLibre.Handlers.Layers;

public class GeoJsonSource : ContentView
{
    // Bindable properties to receive data from the LayerViewModel
    public static readonly BindableProperty SourceNameProperty =
        BindableProperty.Create(nameof(SourceName), typeof(string), typeof(GeoJsonSource), null, propertyChanged: OnPropertiesChanged);

    public string SourceName
    {
        get => (string)GetValue(SourceNameProperty);
        set => SetValue(SourceNameProperty, value);
    }

    public static readonly BindableProperty FeatureCollectionProperty =
        BindableProperty.Create(nameof(FeatureCollection), typeof(FeatureCollection), typeof(GeoJsonSource), null, propertyChanged: OnPropertiesChanged);

    public FeatureCollection? FeatureCollection
    {
        get => (FeatureCollection?)GetValue(FeatureCollectionProperty);
        set => SetValue(FeatureCollectionProperty, value);
    }

    private bool _isAdded = false;

    protected override void OnParentChanged()
    {
        base.OnParentChanged();
        if (_isAdded) return;
        var parentMap = FindParentMapLibreMap(this);
        if (parentMap == null) return;

        parentMap.StyleLoaded += OnStyleLoaded;
    }

    private void OnStyleLoaded(object? sender, EventArgs e)
    {
        AddLayerToParentMap();
        _isAdded = true;
    }

    private static void OnPropertiesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        // You could trigger adding here if properties change, but triggering from a separate property is cleaner.
        
        
    }

    private void AddLayerToParentMap()
    {
        // Find the parent MapLibreMap
        var parentMap = FindParentMapLibreMap(this);
        if (parentMap == null) return;
        if (string.IsNullOrEmpty(SourceName)) return;
        if (FeatureCollection == null) return;
        parentMap.AddGeoJsonSource(SourceName, FeatureCollection);
    }

    // Helper to find the parent MapLibreMap
    private static MapLibreMap? FindParentMapLibreMap(Element element)
    {
        var parent = element.Parent;
        while (parent != null)
        {
            if (parent is MapLibreMap map)
            {
                return map;
            }
            parent = parent.Parent;
        }
        return null;
    }
}