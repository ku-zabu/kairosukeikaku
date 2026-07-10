using UnityEngine;

public class ItemTemp : MonoBehaviour
{
    public int itemNo;
    public string inversText;
    public string acquirText;
    public string actionText;

    public virtual void Invers() { }
    public virtual int Acquir() { return 0; }
    public virtual void Action() { }
}
