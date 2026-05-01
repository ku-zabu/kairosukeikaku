using UnityEngine;
using UnityEngine.InputSystem;

public class Player_FP : MonoBehaviour
{
    //カメラの回転を制御するための変数
    Transform cam;
    float xRotation = 0f;                       //カメラの上下の回転量
    float lookSpeedX = 10, lookSpeedY = 10;     //カメラの回転速度
    bool lookMoveX = false, lookMoveY = false;  //リバース機能

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //カメラの回転を制御する関数 上下はカメラ、左右はプレイヤーを回転させる 
    void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        var xRot = input.y * lookSpeedX * Time.deltaTime * (lookMoveX ? 1 : -1);
        var yRot = input.x * lookSpeedY * Time.deltaTime * (lookMoveY ? -1 : 1);

        xRotation += xRot;
        xRotation = Mathf.Clamp(xRotation, -90f, 45f);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * yRot);
    }
}
