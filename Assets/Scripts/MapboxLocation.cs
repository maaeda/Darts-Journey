using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class MapboxLocation : MonoBehaviour
{
    public AbstractMap map = null;
    public GameObject cube = null;

    void Start()
    {
        map.MapVisualizer.OnMapVisualizerStateChanged += (state) =>
        {
            if (state == ModuleState.Finished) {
                double lat = 32.7503353; // 緯度
                double lng = 129.8777355; // 経度

                Vector2d latlng = new Vector2d(lat, lng);

                Vector3 pos = map.GeoToWorldPosition(latlng);
                Debug.Log(pos);

                cube.transform.position = pos;
            }
        };
    }

    void Update()
    {

    }
}