using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

public class CoordinatesConverter : MonoBehaviour
{
    public AbstractMap map;

    public Vector2d GetLatLongFromPosition(Vector3 position)
    {
        // Unityのワールド座標を緯度経度に変換
        Vector2d latLong = map.WorldToGeoPosition(position);
        Debug.Log(latLong);
        return latLong;
    }
}