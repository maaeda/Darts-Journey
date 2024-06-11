using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _speed = 0.1f;// 移動速度
    float sensitiveRotate = 5.0f;//カメラの回転速度

    float fixedZRotation = 0.0f; // Z軸の固定値

    void Update()
    {
        playerMove();
        CameraMove();
    }

    void playerMove() 
    {
        var velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            velocity.z = _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x = -_speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.z = -_speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x = _speed;
        }
        if (velocity.x != 0 || velocity.z != 0)
        {
            transform.position += transform.rotation * velocity;
        }

    }

    void CameraMove() 
    {
        if (Input.GetMouseButton(0))
        {
            float rotateX = Input.GetAxis("Mouse X") * sensitiveRotate;
            float rotateY = Input.GetAxis("Mouse Y") * sensitiveRotate;
            this.gameObject.transform.Rotate(rotateY, rotateX, 0.0f);

        }
        // Z軸の回転を固定する
        Vector3 currentRotation = this.gameObject.transform.rotation.eulerAngles;
        currentRotation.z = fixedZRotation;
        this.gameObject.transform.rotation = Quaternion.Euler(currentRotation);

    }
}