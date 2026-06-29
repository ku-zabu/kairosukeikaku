using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    Dictionary<int, bool> inHand = new Dictionary<int, bool>();

    public void AddItem(int no)
    {
        inHand.Add(no, true);
    }

    public bool UseItem(int no)
    {
        if (inHand.ContainsKey(no))
        {
            if (inHand[no])
            {
                if (no == 3)
                    return true;
                inHand[no] = false;
                return true;
            }
        }
        return false;
    }

    public void GetItem(int no)
    {
        if (inHand.ContainsKey(no))
            inHand[no] = true;
        else
            AddItem(no);
    }
}
