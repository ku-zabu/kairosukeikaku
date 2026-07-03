using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Beye : MonoBehaviour
{

    private Rigidbody rigidBody;

    private Vector3 velocity;

    private Vector3 input;

    private Animator animator;

    [SerializeField]
    private LayerMask groundLayers;

    [SerializeField]
    private float walkSpeed = 4f;

    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private Vector3 groundPositionOffset = new Vector3(0f, 0.02f, 0f);

    [SerializeField]
    private float groundColliderRadius = 0.29f;

    public PlayerInput pi;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        pi = GetComponent<PlayerInput>();

    }

    void FixedUpdate()
    {

       var value = pi.actions["Move"].ReadValue<Vector2>();

        var V = new Vector3(value.x, 0f, value.y) * walkSpeed;
        Debug.Log(V);

        rigidBody.AddForce(V,ForceMode.Acceleration);

    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (move != 0)
        {
            rigidBody.AddForce(new Vector2(move * walkSpeed, 0));
        }
        else
        {
            // ���͂��Ȃ����́A�ړ������̑��x�������I��0�ɂ���
            rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
        }
    }


}
