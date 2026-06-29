using UnityEngine;

/// <summary>
/// 俯瞰視点時のプレイやー
/// </summary>
public class Player_Beye : MonoBehaviour
{
    GameMane_Beye gameMane;
    Rigidbody rb;

    [SerializeField] float moveSpeed;
    public bool moveType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameMane = FindAnyObjectByType<GameMane_Beye>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Move(Vector2 value)
    {
        var moveF = moveType ? new Vector3(value.y, 0, -value.x) : new Vector3(value.x, 0, value.y);

        rb.linearVelocity = moveF * moveSpeed;

        rb.rotation = Quaternion.RotateTowards(
            rb.rotation,
            Quaternion.LookRotation(moveF),
            480 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (other.TryGetComponent<ItemTemp>(out var item))
                gameMane.ContactItem(item);
            else
                Debug.LogError("Scriptが未割当");
        }
        else if (other.gameObject.CompareTag("Gimmick"))
        {
            if (other.TryGetComponent<GimmickTemp>(out var gimmick))
                gameMane.ContactGimmick(gimmick);
            else
                Debug.LogError("Scriptが未割当");
        }
        else if (other.gameObject.CompareTag("Exit"))
        {
            gameMane.Completion();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item") || other.gameObject.CompareTag("Gimmick"))
        {
            gameMane.Separation();
        }
    }
}
