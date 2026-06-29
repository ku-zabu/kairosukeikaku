using UnityEngine;

public class WateringDisplay : IDisplayTemp
{
    public override void Check(int occ)
    {
        switch (occ)
        {
            case 0:
                gameObject.SetActive(p);
                break;
            case 1:
                gameObject.SetActive(c);
                break;
            case 2:
                gameObject.SetActive(f);
                break;
        }
    }
}
