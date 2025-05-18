#nullable enable
using Microsoft.Maui.Handlers;
using UIKit;

namespace Maui.MapLibre.Handlers;

public partial class MauiMapLibreHandler : ViewHandler<MauiMapLibre, UIKit.UIView>
{
    protected override UIKit.UIView CreatePlatformView()
    {
        var mapView = new UIView();
        return mapView;
    }
}
