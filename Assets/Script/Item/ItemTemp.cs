using UnityEngine;

public class ItemTemp : MonoBehaviour
{
    [SerializeField] bool item;
    public virtual void Interact(Player_FP player = null)
    {
        if (item) Destroy(gameObject);
    }
}
