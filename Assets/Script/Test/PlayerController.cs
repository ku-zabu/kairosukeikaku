using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]

public class Playerontroller : MonoBehaviour
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

            rb.rotation = Quaternion.RotateTowards(
                rb.rotation,
                Quaternion.LookRotation(moveForward),
                360 * Time.deltaTime);
        }

        if (input.actions["Q"].WasPressedThisFrame())

        {
            Debug.Log("Q‚ª‰Ÿ‚³‚ê‚½");
        }

    }
}
