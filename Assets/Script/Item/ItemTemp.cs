using UnityEngine;

public class ItemTemp : MonoBehaviour
{
    public bool hint;

    public int itemNo;
    /// <summary>
    /// ’²‚×‚é
    /// </summary>
    public string inversText;
    /// <summary>
    /// æ“¾
    /// </summary>
    public string acquirText;
    /// <summary>
    /// s“®
    /// </summary>
    public string actionText;

    /// <summary>
    /// ’²‚×‚é
    /// </summary>
    public virtual void Invers() { }

    /// <summary>
    /// æ“¾
    /// </summary>
    /// <returns></returns>
    public virtual int Acquir() { return 0; }

    /// <summary>
    /// s“®
    /// </summary>
    public virtual void Action(int i) { }

    public virtual void ChangerSet() { }
}
