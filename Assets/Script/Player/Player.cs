using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        var value = input.actions["Move"].ReadValue<Vector2>();
        if (value != Vector2.zero)
        {
            Vector3 cameraForeard = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 moveForward = cameraForeard * value.y + Camera.main.transform.right * value.x;
            rb.linearVelocity = new float3(moveForward) * (input.actions["Sprint"].inProgress ? 4 : 2);

            float current = 0;
            //回転量の計算
            var rotAngle = Mathf.SmoothDampAngle(
                0,
                Vector3.Angle(transform.forward, moveForward),//ベクトル差の計算
                ref current,
                0.05f,          //進行方向を向くのにかかる時間
                Mathf.Infinity  //最高回転角度
                );
            //回転の計算
            var nextRot = Quaternion.RotateTowards(
                rb.rotation,    //現在角度
                Quaternion.LookRotation(moveForward, Vector3.up),//進行方向へのクォータニオン取得
                rotAngle
                );
            //反映
            rb.rotation = nextRot;
        }

        if (input.actions["Q"].IsPressed())
        
        {
            Debug.Log("Qが押された");
        }
        
    }
}