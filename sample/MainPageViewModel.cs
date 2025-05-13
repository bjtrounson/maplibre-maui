using CommunityToolkit.Mvvm.ComponentModel;
using Maui.MapLibre.Handlers;
using Maui.MapLibre.Handlers.Annotation;

namespace MauiSample;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty] private MapLibreMap _mapLibreMap;
}