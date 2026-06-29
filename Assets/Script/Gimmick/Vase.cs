using Unity.VisualScripting;
using UnityEngine;

public class Vase : GimmickTemp
{

    private void Start()
    {
        checker = FindAnyObjectByType<ItemChecker>();
    }

    public override bool UniqueGimmick()
    {
        if (checker.UseItem(3))
        {
            uniqueTrigger = true;    
            return true;
        }
        else
        {
            return false;
        }
    }
}
