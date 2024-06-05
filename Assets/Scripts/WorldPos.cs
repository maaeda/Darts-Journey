using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class MapboxLocation : MonoBehaviour
{
    public AbstractMap map = null;

    void Start()
    {
        map.MapVisualizer.OnMapVisualizerStateChanged += (state) =>
        {
            if (state == ModuleState.Finished) {

                Vector3 ObjectPos = this.transform.position;
                Vector2d WorldPos = map.WorldToGeoPosition(ObjectPos);
                Debug.Log(WorldPos);

            }
        };
    }

    void Update()
    {

    }
}