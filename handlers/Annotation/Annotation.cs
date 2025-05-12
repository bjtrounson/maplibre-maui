using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Maui.MapLibre.Handlers.Annotation.Properties;

namespace Maui.MapLibre.Handlers.Annotation;

public abstract class Annotation
{
    public string Id { get; set; }

    public abstract Feature<IGeometryObject, ILayerProperties> ToGeoJson();
}