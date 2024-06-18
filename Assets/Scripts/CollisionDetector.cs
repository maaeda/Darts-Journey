using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine.Networking;

public class CollisionDetector : MonoBehaviour
{
    public AbstractMap map = null;

    void Start()
    {
        //AbstractMapインスタンスを取得する
        map = FindObjectOfType<AbstractMap>();

        if (map == null)
        {
            Debug.LogError("AbstractMap instance not found.");
        }
    }

    // 衝突時に呼び出される関数
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの名前を取得
        string objectName = collision.gameObject.name;

        if (objectName == "Sphere(Clone)")
        {
            Debug.Log($"衝突したオブジェクトは: {objectName}");

            // 衝突したオブジェクトを取得
            GameObject Sphere = collision.gameObject;;

            // Rigidbodyコンポーネントを取得
            Rigidbody SphereRigidbody = Sphere.GetComponent<Rigidbody>();

            // isKinematicをtrueに設定して物理演算の影響を受けないようにする
            SphereRigidbody.isKinematic = true;

            //衝突した位置を取得
            Vector3 SphereWorldPos = Sphere.transform.position;
            Debug.Log($"{objectName}のワールド座標: {SphereWorldPos}");

            //衝突位置を少し編集
            Vector3 sphereWorldPosVec3 = new Vector3(SphereWorldPos.x, SphereWorldPos.y,0);

            //世界座標を取得
            Vector2d sphereGeoLocation = map.WorldToGeoPosition(sphereWorldPosVec3);
            Debug.Log($"{objectName}の世界座標: {sphereGeoLocation}");

            double latitude  = sphereGeoLocation.y;//緯度
            double longitude = sphereGeoLocation.x;//経度

            //Application.OpenURL($"https://fukuno.jig.jp/app/map/latlng/#{longitude}%2C{latitude}");//""の中には開きたいWebページのURLを入力します
            Application.OpenURL($"https://maps.google.com/maps?ll={longitude},{latitude}&q={longitude},{latitude}");//""の中には開きたいWebページのURLを入力します
            string yahooApiUrl = $"https://map.yahooapis.jp/geoapi/V1/reverseGeoCoder?lat={longitude}&lon={latitude}&output=json&appid=dj00aiZpPW9jcFJrM0JYZE1ZQSZzPWNvbnN1bWVyc2VjcmV0Jng9MzI-";

            StartCoroutine(GetLocationName(yahooApiUrl));
        }
    }

    IEnumerator GetLocationName(string apiUrl)
    {
        string requestUrl = apiUrl;
        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log($"Raw JSON Response: {jsonResponse}");

            // JSONデータのパース
            ReverseGeocodeResponse response = JsonUtility.FromJson<ReverseGeocodeResponse>(jsonResponse);

            // 取得したデータの使用
            if (response != null && response.Feature != null && response.Feature.Length > 0)
            {
                Feature feature = response.Feature[0];
                if (feature.Property != null)
                {
                    string address = feature.Property.Address;
                    string serchName = null;

                    // AddressElementを取得して表示
                    AddressElement[] addressElements = feature.Property.AddressElement;
                    for (int i = 0; i < addressElements.Length && i < 2; i++)
                    {
                        serchName += addressElements[i].Name;
                        //serchName.Append<>(addressElements[i].Name);
                        Debug.Log($"Address Element {i + 1}: Name={addressElements[i].Name}, Kana={addressElements[i].Kana}, Level={addressElements[i].Level}");
                    }
                    Application.OpenURL($"https://www.japan47go.travel/ja/search/result?place={serchName}");//""の中には開きたいWebページのURLを入力します
                    Debug.Log($"SerchName: {serchName}");
                }
                else
                {
                    Debug.LogError("Property data is missing in the feature.");
                }
            }
            else
            {
                Debug.LogError("Failed to parse JSON or no feature data available.");
            }
        }
    }
}

[System.Serializable]
public class ReverseGeocodeResponse
{
    public ResultInfo ResultInfo;
    public Feature[] Feature;
}

[System.Serializable]
public class ResultInfo
{
    public int Count;
    public int Total;
    public int Start;
    public float Latency;
    public int Status;
    public string Description;
    public string Copyright;
    public string CompressType;
}

[System.Serializable]
public class Feature
{
    public Geometry Geometry;
    public Property Property;
}

[System.Serializable]
public class Geometry
{
    public string Type;
    public string Coordinates;
}

[System.Serializable]
public class Property
{
    public Country Country;
    public string Address;
    public AddressElement[] AddressElement;
}

[System.Serializable]
public class Country
{
    public string Code;
    public string Name;
}

[System.Serializable]
public class AddressElement
{
    public string Name;
    public string Kana;
    public string Level;
    public string Code;
}