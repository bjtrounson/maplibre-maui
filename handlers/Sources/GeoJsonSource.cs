using GeoJSON.Text.Feature;
using Maui.MapLibre.Handlers.Annotation;

namespace Maui.MapLibre.Handlers.Sources;

public class GeoJsonSource : StyleView
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

    private static void OnPropertiesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        // You could trigger adding here if properties change, but triggering from a separate property is cleaner.
        
        
    }

    protected override void AddLayerToParentMap()
    {
        // Find the parent MapLibreMap
        var parentMap = FindParentMapLibreMap(this);
        if (parentMap == null) return;
        if (string.IsNullOrEmpty(SourceName)) return;
        if (FeatureCollection == null) return;
        parentMap.AddGeoJsonSource(SourceName, FeatureCollection);
    }
}