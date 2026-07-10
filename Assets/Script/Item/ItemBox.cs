using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public bool[] active = new bool[3];

    virtual public void ActiveChanger(int i)
    {
        switch (i)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}
