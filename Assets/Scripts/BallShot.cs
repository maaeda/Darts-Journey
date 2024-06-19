using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallShot : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject childObj;
    private float speed = 1300;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        childObj = transform.GetChild(0).gameObject;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        GameObject Sphere = GameObject.Find("Sphere(Clone)");
        if(Input.GetKeyDown("space"))
        {
            Destroy(Sphere);
            GameObject ball =  (GameObject)Instantiate(sphere, childObj.transform.position, Quaternion.identity);
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(transform.forward * speed);
        }

        if(Input.GetMouseButtonDown(0))
        {
            Destroy(Sphere);
            GameObject ball =  (GameObject)Instantiate(sphere, childObj.transform.position, Quaternion.identity);
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(transform.forward * speed);
        }

        if (Input.GetMouseButtonDown(2))
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }

        float mx = Input.GetAxis("Mouse X");//カーソルの横の移動量を取得
        float my = Input.GetAxis("Mouse Y");//カーソルの縦の移動量を取得
        transform.Rotate(my*-1,mx,0);
        if (Mathf.Abs(mx) > 0.001f) // X方向に一定量移動していれば横回転
        {
            transform.RotateAround(transform.position, Vector3.up, mx); // 回転軸はplayerオブジェクトのワールド座標Y軸
        }

        if(Input.GetKey("right"))
        {
            transform.Rotate(0,1,0);
        }

        if(Input.GetKey("left"))
        {
            transform.Rotate(0,-1,0);
        }

        if(Input.GetKey("up"))
        {
            transform.Rotate(-1,0,0);
        }

        if(Input.GetKey("down"))
        {
            transform.Rotate(1,0,0);
        }
    }
}