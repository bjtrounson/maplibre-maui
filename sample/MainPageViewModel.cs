using CommunityToolkit.Mvvm.ComponentModel;
using Maui.MapLibre.Handlers;
using Maui.MapLibre.Handlers.Annotation;

namespace MauiSample;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty] private MapLibreMap _mapLibreMap;

    public void AddLineLayer()
    {
        MapLibreMap.AddLineLayer("line-layer", "line-source", null, null, LineManager.BaseProperties, 0, 0, true);
    }
}