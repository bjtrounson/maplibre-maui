#nullable enable
using Android.Views;
using AndroidX.CoordinatorLayout.Widget;
using MapLibreAndroid;
using Microsoft.Maui.Handlers;

namespace Maui.MapLibre.Handlers;

public partial class MauiMapLibreHandler : ViewHandler<MauiMapLibre, Android.Views.View>
{
    protected override Android.Views.View CreatePlatformView()
    {
        if (Platform.CurrentActivity == null)
        {
            throw new InvalidOperationException("MauiMapLibreHandler requires a current activity.");
        }

        var layout = new CoordinatorLayout(Platform.CurrentActivity);
        layout.LayoutParameters ??= new CoordinatorLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
        layout.LayoutParameters.Width = ViewGroup.LayoutParams.MatchParent;
        layout.LayoutParameters.Height = ViewGroup.LayoutParams.MatchParent;
        var mapView = DotnetMapLibre.CreateMap(Platform.CurrentActivity) ?? throw new InvalidOperationException();
        layout.AddView(mapView);

        return layout;
    }
}
