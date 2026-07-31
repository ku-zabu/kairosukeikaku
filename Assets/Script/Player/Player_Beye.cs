using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Beye : MonoBehaviour
{
    public Rigidbody rb;
    PlayerInput input;
    [SerializeField] float moveSpeed;
    [SerializeField] float roteSpeed;
    Animator anim;

    StageManager stagemanager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = FindAnyObjectByType<PlayerInput>();
        stagemanager = FindAnyObjectByType<StageManager>();
        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        float mag = 0;
        if (Physics.SphereCast(transform.position + new Vector3(0, 1.1f, 0), 1.0f, -Vector3.up, out var hit, 0.2f)) 
        {
            var value = input.actions["Move"].ReadValue<Vector2>();
            var moveF = new Vector3(value.x, 0, value.y) * moveSpeed;
            rb.AddForce(moveF, ForceMode.Acceleration);

            mag = value.magnitude;

            if(value != Vector2.zero)
            {
                rb.rotation = Quaternion.RotateTowards(
                    rb.rotation,
                    Quaternion.LookRotation(moveF.normalized),
                    360 * roteSpeed * Time.deltaTime);
            }
        }
        else //空中にいる
        {
            rb.AddForce(-Vector3.up * 3f, ForceMode.Impulse);
        }

        anim.SetFloat("Value", mag);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            stagemanager.Goal();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<ItemTemp>(out ItemTemp item))
        {
            stagemanager.SetItem(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stagemanager.unsetItem(true);
    }
}

