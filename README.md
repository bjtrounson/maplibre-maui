# MapLibre MAUI

[![License](https://img.shields.io/badge/License-MIT-blue.svg)](/LICENSE.md)

_.NET MAUI library for creating maps
with [MapLibre Native for Android, iOS & macOS](https://github.com/maplibre/maplibre-gl-native)._

I created this project as I needed to use MapLibre for a project at work in our MAUI app that we migrated from Xamarin.

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