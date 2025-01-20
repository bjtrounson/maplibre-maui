using Microsoft.Maui.Handlers;

namespace Maui.MapLibre.Handlers;

// All the code in this file is included in all platforms.
public class MauiMapLibre : View
{
    public static readonly BindableProperty StyleUrlProperty = BindableProperty.Create(nameof(StyleUrl), typeof(string), typeof(MauiMapLibre), default(string));

    public string StyleUrl
    {
        get => (string)GetValue(StyleUrlProperty);
        set => SetValue(StyleUrlProperty, value);
    }
}

public partial class MauiMapLibreHandler
{
    public static IPropertyMapper<MauiMapLibre, MauiMapLibreHandler> PropertyMapper =
        new PropertyMapper<MauiMapLibre, MauiMapLibreHandler>(ViewHandler.ViewMapper);

    public MauiMapLibreHandler() : base(PropertyMapper)
    {

    }
}

