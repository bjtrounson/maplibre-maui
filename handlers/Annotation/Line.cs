using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using Maui.MapLibre.Handlers.Annotation.Properties;

namespace Maui.MapLibre.Handlers.Annotation;

public class Line : Annotation
{
    public LineOptions Options { get; set; }
    public Line(string id, LineOptions options)
    {
        Id = id;
        Options = options;
    }
    
    public override Feature<IGeometryObject, ILayerProperties> ToGeoJson()
    {
         return Options.ToGeoJson();
    }
}

public class LineOptions(
    string? lineJoin,
    int? lineOpacity,
    string? lineColor,
    double? lineWidth,
    double? lineGapWidth,
    double? lineOffset,
    double? lineBlur,
    string? linePattern,
    IList<IPosition>? geometry,
    bool? draggable)
{
    public string? LineJoin { get; set; } = lineJoin;
    public int? LineOpacity { get; set; } = lineOpacity;
    public string? LineColor { get; set; } = lineColor;
    public double? LineWidth { get; set; } = lineWidth;
    public double? LineGapWidth { get; set; } = lineGapWidth;
    public double? LineOffset { get; set; } = lineOffset;
    public double? LineBlur { get; set; } = lineBlur;
    public string? LinePattern { get; set; } = linePattern;
    public IList<IPosition>? Geometry { get; set; } = geometry;
    public bool? Draggable { get; set; } = draggable;

    public LineOptions CopyWith(LineOptions changes)
    {
        return new LineOptions(
            lineJoin: changes.LineJoin ?? LineJoin,
            lineOpacity: changes.LineOpacity ?? LineOpacity,
            lineColor: changes.LineColor ?? LineColor,
            lineWidth: changes.LineWidth ?? LineWidth,
            lineGapWidth: changes.LineGapWidth ?? LineGapWidth,
            lineOffset: changes.LineOffset ?? LineOffset,
            lineBlur: changes.LineBlur ?? LineBlur,
            linePattern: changes.LinePattern ?? LinePattern,
            geometry: changes.Geometry ?? Geometry,
            draggable: changes.Draggable ?? Draggable
        );
    }

    public Feature<IGeometryObject, ILayerProperties> ToGeoJson(bool addGeometry = true)
    {
        var line = new LineString(Geometry);
        
        var properties = ToLineLayerProperties();
        var feature = new Feature<IGeometryObject, ILayerProperties>(line, properties);
        return feature;
    }
    
    private LineLayerProperties ToLineLayerProperties()
    {
        return new LineLayerProperties(
            lineJoin: LineJoin,
            lineOpacity: LineOpacity,
            lineColor: LineColor,
            lineWidth: LineWidth,
            lineGapWidth: LineGapWidth,
            lineOffset: LineOffset,
            lineBlur: LineBlur,
            linePattern: LinePattern
        );
    }
}