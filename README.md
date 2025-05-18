# MAUI MapLibre

A MAUI wrapper for the [MapLibre](https://maplibre.org/) library.

This is a work in progress and is not yet ready for production use. The API is likely to change.

## Getting started
```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:maplibre="clr-namespace:Maui.MapLibre.Handlers;assembly=Maui.Maplibre.Handlers"
             xmlns:layers="clr-namespace:Maui.MapLibre.Handlers.Layers;assembly=Maui.Maplibre.Handlers"
             xmlns:sources="clr-namespace:Maui.MapLibre.Handlers.Sources;assembly=Maui.Maplibre.Handlers"
             x:Class="MauiSample.MainPage">
        <maplibre:MapLibreMap MyLocationEnabled="True" StyleUrl="https://demotiles.maplibre.org/style.json"></maplibre:MapLibreMap>
</ContentPage>
```