using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public AbstractMap map;

    // 衝突時に呼び出される関数
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの名前を取得
        string objectName = collision.gameObject.name;

        if (objectName == "Sphere(Clone)")
        {
            Debug.Log("Collided with: " + objectName);

            // 他のオブジェクトを名前で検索
            GameObject Sphere = GameObject.Find(objectName);

            // Rigidbodyコンポーネントを取得
            Rigidbody SphereRigidbody = Sphere.GetComponent<Rigidbody>();

            // isKinematicをtrueに設定して物理演算の影響を受けないようにする
            SphereRigidbody.isKinematic = true;

            Vector3 SpherePosition = Sphere.transform.position;

            Debug.Log("WorldPos: " + SpherePosition);

            Vector2d GeoPos = GetGeoLocation(SpherePosition);

            Debug.Log(Sphere + " Position: " + GeoPos);
        }
    }

    public Vector2d GetGeoLocation(Vector3 worldPosition)
    {
        // ワールド座標を緯度経度に変換
        Vector2d geoLocation = map.WorldToGeoPosition(worldPosition);
        return geoLocation;
    }
}