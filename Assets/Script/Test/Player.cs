using Unity.Cinemachine;
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
        var value = input.actions["Move"].ReadValue<Vector3>();
        if (value != Vector3.zero)
        {

            rb.linearVelocity = new float3(value.x, 0f, value.y) * (input.actions["Sprint"].inProgress ? 4 : 2);

            rb.rotation = Quaternion.RotateTowards(
                rb.rotation,
                Quaternion.LookRotation(new float3(value.x, 0f, value.y)),
                360 * Time.deltaTime);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("artifact"));
        {
            Debug.Log("Ç†Å[ÇƒÇ°Ç”ÇüÇ≠Ç∆");
        }

    }
}