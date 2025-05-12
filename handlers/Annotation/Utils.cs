using System.Text.Json;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Maui.MapLibre.Handlers.Annotation.Properties;

namespace Maui.MapLibre.Handlers.Annotation;

public static class Utils
{
    public static string BuildFeatureCollection(IEnumerable<Feature<IGeometryObject, ILayerProperties>> features)
    {
        var collection = new FeatureCollection<ILayerProperties>();
        collection.Features.AddRange(features);
        return JsonSerializer.Serialize(collection);
    }

    public static string GetRandomString(int length = 10)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}