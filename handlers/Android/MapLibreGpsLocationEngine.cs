using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Java.Lang;
using Org.Maplibre.Android.Location.Engine;
using Location = Android.Locations.Location;
using Object = Java.Lang.Object;
using Exception = Java.Lang.Exception;

namespace Maui.MapLibre.Handlers.Android;

public class MapLibreGpsLocationEngine(Context context) : Object, ILocationEngineImpl
{
    private readonly LocationManager? _locationManager = (LocationManager?) context.GetSystemService(Context.LocationService);
    private string _currentProvider = LocationManager.PassiveProvider;

    public Object CreateListener(ILocationEngineCallback? p0)
    {
        return new AndroidLocationEngineCallbackTransport(p0);
    }

    public void GetLastLocation(ILocationEngineCallback p0)
    {
        var location = GetLastLocationFor(_currentProvider);
        if (location != null)
        {
            p0.OnSuccess(LocationEngineResult.Create(location));
        }

        if (_locationManager == null)
        {
            p0.OnFailure(new Exception("LocationManager is null"));
            return;
        }

        foreach (var provider in _locationManager.AllProviders)
        {
            location = GetLastLocationFor(provider);
            if (location == null) continue;
            p0.OnSuccess(LocationEngineResult.Create(location));
            return;
        }
        
        p0.OnFailure(new Exception("Last location unavailable"));
    }

    public void RemoveLocationUpdates(PendingIntent? p0)
    {
        if (p0 == null) return;
        _locationManager?.RemoveUpdates(p0);
    }

    public void RemoveLocationUpdates(Object? p0)
    {
        if (p0 is not ILocationListener listener) return;
        _locationManager?.RemoveUpdates(listener);
    }

    public void RequestLocationUpdates(LocationEngineRequest p0, PendingIntent p1)
    {
        _currentProvider = GetBestProvider(p0.Priority);
        _locationManager?.RequestLocationUpdates(_currentProvider, p0.Interval, p0.Displacement, p1);
    }

    public void RequestLocationUpdates(LocationEngineRequest p0, Object p1, Looper? p2)
    {
        _currentProvider = GetBestProvider(p0.Priority);
        if (p1 is not ILocationListener listener) return;
        _locationManager?.RequestLocationUpdates(_currentProvider, p0.Interval, p0.Displacement, listener, p2);
    }

    private string GetBestProvider(int priority)
    {
        string? provider = null;
        if (priority != LocationEngineRequest.PriorityNoPower)
        {
            provider = LocationManager.GpsProvider;
        }

        return provider ?? LocationManager.PassiveProvider;
    }

    private Location? GetLastLocationFor(string provider)
    {
        if (_locationManager == null) return null;
        Location? location;
        try
        {
            location = _locationManager.GetLastKnownLocation(provider);
        }
        catch (IllegalArgumentException)
        {
            return null;
        }
        return location;
    }

    private class AndroidLocationEngineCallbackTransport(ILocationEngineCallback? callback) : Object, ILocationListener
    {
        public void OnLocationChanged(Location location)
        {
            callback?.OnSuccess(location);
        }

        public void OnProviderDisabled(string provider)
        {
            callback?.OnFailure(new Exception("Current provider disabled"));
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string? provider, Availability status, Bundle? extras)
        {
        }
    }
}