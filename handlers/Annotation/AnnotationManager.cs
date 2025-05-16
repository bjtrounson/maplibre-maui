using Maui.MapLibre.Handlers.Annotation.Properties;

namespace Maui.MapLibre.Handlers.Annotation;

public abstract class AnnotationManager<T>(
    MapLibreMapController controller,
    Func<T, int>? selectLayer = null)
    : IDisposable
    where T : Annotation
{
    protected MapLibreMapController Controller = controller;
    protected string Id = Utils.GetRandomString();
    private Dictionary<string, T> _idToAnnotation = new();
    private readonly Dictionary<string, int> _idToLayerIndex = new();
    protected List<ILayerProperties> AllLayerProperties = new();

    private Func<T, int>? SelectLayer { get; set; } = selectLayer;

    private string MakeLayerId(int layerIndex) => $"{Id}_{layerIndex}";

    private void SetAll()
    {
        if (SelectLayer == null)
        {
            Controller.SetGeoJsonSource(MakeLayerId(0), Utils.BuildFeatureCollection(_idToAnnotation.Values.Select(x => x.ToGeoJson())));
            return;
        }
        
        var featureBuckets = AllLayerProperties.Select(_ => new List<T>()).ToList();
        foreach (var annotation in _idToAnnotation.Values)
        {
            var layerIndex = SelectLayer(annotation);
            _idToLayerIndex[annotation.Id] = layerIndex;
            featureBuckets[layerIndex].Add(annotation);
        }

        for (var i = 0; i < featureBuckets.Count; i++)
        {
            Controller.SetGeoJsonSource(MakeLayerId(i), Utils.BuildFeatureCollection(featureBuckets[i].Select(x => x.ToGeoJson())));
        }
    }
    
    public void Add(T annotation)
    {
        _idToAnnotation[annotation.Id] = annotation;
        SetAll();
    }
    
    public void Remove(T annotation)
    {
        _idToAnnotation.Remove(annotation.Id);
        SetAll();
    }
    
    public void Clear()
    {
        _idToAnnotation.Clear();
        SetAll();
    }
    
    public void AddAll(IEnumerable<T> annotations)
    {
        foreach (var annotation in annotations)
        {
            _idToAnnotation[annotation.Id] = annotation;
        }
        SetAll();
    }
    
    public void RemoveAll()
    {
        foreach (var annotation in _idToAnnotation.Values)
        {
            _idToAnnotation.Remove(annotation.Id);
        }
        SetAll();
    }

    public void Dispose()
    {
        _idToAnnotation.Clear();
        SetAll();
        for (var i = 0; i < AllLayerProperties.Count; i++)
        {
            Controller.RemoveLayer(MakeLayerId(i));
            Controller.RemoveSource(MakeLayerId(i));
        }
    }
}

public class LineManager(MapLibreMapController controller) : AnnotationManager<Line>(controller,
    l => l.Options.LinePattern == null ? 0 : 1)
{
    public static readonly LineLayerProperties BaseProperties = new(
        lineJoin: "miter",
        lineOpacity: 1,
        lineColor: "#000000",
        lineWidth: 1,
        lineGapWidth: 0,
        lineOffset: 0,
        lineBlur: 0
    );

    public new List<LineLayerProperties> AllLayerProperties => [BaseProperties];
}
