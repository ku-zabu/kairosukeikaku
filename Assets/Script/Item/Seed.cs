using UnityEngine;

public class Seed : ItemTemp
{
    public override int Acquir()
    {
        gameObject.SetActive(false);
        return itemNo;
    }
}
