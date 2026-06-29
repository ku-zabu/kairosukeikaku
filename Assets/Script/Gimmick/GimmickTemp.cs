using UnityEngine;

public class GimmickTemp : MonoBehaviour
{
    public ItemChecker checker;
    public int gimNo;
    public int needItem;
    public int itemNo = -1;
    public string inversText;
    public string actionText;
    public string getItem;
    public bool uniqueTrigger;
    public string uniqueText;

    public virtual bool UniqueGimmick() { return false; }
}
