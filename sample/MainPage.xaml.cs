namespace MauiSample;
public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

  protected override void OnAppearing()
  {
    base.OnAppearing();
    DeviceDisplay.Current.KeepScreenOn = true; 
  }
}

