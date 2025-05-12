using Android.App;
using Android.Content;
using Org.Maplibre.Android.Camera;

namespace Maui.MapLibre.Handlers;

public class MapLibreMapFactory
{
    public static MapLibreMapController Create(Activity activity, Context context, Dictionary<string, object> args) {
        var builder = new MapLibreMapBuilder();

        args.TryGetValue("options", out var options);
        if (args.TryGetValue("initialCameraPosition", out var position)) {
            builder.SetInitialCameraPosition((CameraPosition) position);
        }
        if (args.TryGetValue("dragEnabled", out var dragEnabled))
        {
            builder.SetDragEnabled((bool) dragEnabled);
        }
        if(args.TryGetValue("styleString", out var styleString)) {
            builder.SetStyleString((string)styleString);
        }
        
        var controller = builder.Build(context);
        activity.RegisterActivityLifecycleCallbacks(controller);
        return controller;
    }
}