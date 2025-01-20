package org.maplibre.maui;

import android.content.Context;
import android.view.View;
import android.util.Log;

import org.maplibre.android.MapLibre;
import org.maplibre.android.camera.CameraPosition;
import org.maplibre.android.geometry.LatLng;
import org.maplibre.android.maps.MapView;

public class DotnetMapLibre
{
    public static View createMap(Context context, String styleUrl) {
      // Init MapLibre
      MapLibre.getInstance(context);

      MapView mapView = new MapView(context);
      mapView.getMapAsync(map -> {
        // Add a style to the map
        map.setStyle(styleUrl);
        map.setCameraPosition(new CameraPosition.Builder().target(new LatLng(0.0,0.0)).zoom(1.0).build());
      });

      return mapView;
    }

    public static void dispose(View view) {
      if (view instanceof MapView) {
          ((MapView)view).onDestroy();
      }
    }
}
