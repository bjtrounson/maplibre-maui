using Org.Maplibre.Android.Location.Engine;
using Exception = Java.Lang.Exception;
using Object = Java.Lang.Object;

namespace Maui.MapLibre.Handlers.Android;

public class LocationEngineCallbackListener(Action<LocationEngineResult> onSuccess, Action<Exception> onFailure)
    : Object, ILocationEngineCallback
{
    public void OnFailure(Exception p0)
    {
        onFailure(p0);
    }

    public void OnSuccess(Object? p0)
    {
        onSuccess((LocationEngineResult) p0!);
    }
}