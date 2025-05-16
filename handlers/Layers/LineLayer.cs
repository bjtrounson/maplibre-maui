using Maui.MapLibre.Handlers.Annotation.Properties;

namespace Maui.MapLibre.Handlers.Layers;

public class LineLayer : ContentView
{
    // Bindable properties for LineLayer parameters
    public static readonly BindableProperty LayerNameProperty = BindableProperty.Create(nameof(LayerName), typeof(string), typeof(LineLayer), null, propertyChanged: OnPropertiesChanged);
    public string LayerName
    {
        get => (string)GetValue(LayerNameProperty);
        set => SetValue(LayerNameProperty, value);
    }

    public static readonly BindableProperty SourceNameProperty = BindableProperty.Create(nameof(SourceName), typeof(string), typeof(LineLayer), null, propertyChanged: OnPropertiesChanged);
    public string SourceName
    {
        get => (string)GetValue(SourceNameProperty);
        set => SetValue(SourceNameProperty, value);
    }

    public static readonly BindableProperty BelowLayerIdProperty = BindableProperty.Create(nameof(BelowLayerId), typeof(string), typeof(LineLayer), null, propertyChanged: OnPropertiesChanged);
    public string BelowLayerId
    {
        get => (string)GetValue(BelowLayerIdProperty);
        set => SetValue(BelowLayerIdProperty, value);
    }

    public static readonly BindableProperty SourceLayerProperty = BindableProperty.Create(nameof(SourceLayer), typeof(string), typeof(LineLayer), null, propertyChanged: OnPropertiesChanged);
    public string SourceLayer
    {
        get => (string)GetValue(SourceLayerProperty);
        set => SetValue(SourceLayerProperty, value);
    }

    public static readonly BindableProperty PropertiesProperty = BindableProperty.Create(nameof(Properties), typeof(LineLayerProperties), typeof(LineLayer), null, propertyChanged: OnPropertiesChanged);
    public LineLayerProperties? Properties
    {
        get => (LineLayerProperties?)GetValue(PropertiesProperty);
        set => SetValue(PropertiesProperty, value);
    }

    public static readonly BindableProperty MinZoomProperty = BindableProperty.Create(nameof(MinZoom), typeof(float), typeof(LineLayer), 0f, propertyChanged: OnPropertiesChanged);
    public float MinZoom
    {
        get => (float)GetValue(MinZoomProperty);
        set => SetValue(MinZoomProperty, value);
    }

    public static readonly BindableProperty MaxZoomProperty = BindableProperty.Create(nameof(MaxZoom), typeof(float), typeof(LineLayer), 0f, propertyChanged: OnPropertiesChanged);
    public float MaxZoom
    {
        get => (float)GetValue(MaxZoomProperty);
        set => SetValue(MaxZoomProperty, value);
    }

    public static readonly BindableProperty EnableInteractionProperty = BindableProperty.Create(nameof(EnableInteraction), typeof(bool), typeof(LineLayer), false, propertyChanged: OnPropertiesChanged);
    public bool EnableInteraction
    {
        get => (bool)GetValue(EnableInteractionProperty);
        set => SetValue(EnableInteractionProperty, value);
    }

    private static void OnPropertiesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        // You could trigger adding here
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
    
    private void AddLayerToParentMap()
    {
        var parentMap = FindParentMapLibreMap(this);
        if (parentMap == null) return;
        if(string.IsNullOrEmpty(LayerName)) return;
        if (string.IsNullOrEmpty(SourceName)) return;
        if (Properties == null) return;
        parentMap.AddLineLayer(LayerName, SourceName, BelowLayerId, SourceLayer, Properties, MinZoom, MaxZoom, EnableInteraction);
    }

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