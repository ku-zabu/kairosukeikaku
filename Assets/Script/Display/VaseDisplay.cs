using System.Collections.Generic;
using UnityEngine;

public class VaseDisplay : IDisplayTemp
{
    List<GimmickTemp> vases = new List<GimmickTemp>();

    private void Start()
    {
        vases.AddRange(GetComponentsInChildren<GimmickTemp>());
    }

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
                bool t = false;
                foreach(var g in vases)
                {
                    t = g.uniqueTrigger;
                    if (!t) break;
                }
                gameObject.SetActive(t);
                transform.Find("Plant").gameObject.SetActive(t);

                break;
        }
    }
}
