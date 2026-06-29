using UnityEngine;

public class IDisplayTemp : MonoBehaviour
{
    [SerializeField] protected bool p;
    [SerializeField] protected bool c;
    [SerializeField] protected bool f;

    public virtual void Check(int occ)
    {
    }
}
