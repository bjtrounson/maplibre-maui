namespace Maui.MapLibre.Handlers.Properties;

public interface ILayerProperties
{
    public void FromJson(string json);
    public IDictionary<string, object?> ToDictionary();
}