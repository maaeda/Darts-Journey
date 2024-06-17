using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;

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

            double latitude  = sphereGeoLocation.y;//経度
            double longitude = sphereGeoLocation.x;//緯度

            Application.OpenURL($"https://fukuno.jig.jp/app/map/latlng/#{longitude}%2C{latitude}");//""の中には開きたいWebページのURLを入力します

        }
    }
}