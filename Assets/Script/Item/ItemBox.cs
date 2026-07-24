using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public bool[] active = new bool[3];

    virtual public void ActiveChanger(int i)
    {
        switch (i)
        {
            case 0:
                gameObject.SetActive(active[0]);
                break;
            case 1:
                gameObject.SetActive(active[1]);
                break;
            case 2:
                gameObject.SetActive(active[2]);
                break;
            default:
                break;
        }
    }
}
