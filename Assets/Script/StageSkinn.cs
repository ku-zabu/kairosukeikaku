using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StageSkinn : MonoBehaviour
{
    [SerializeField] Material[] material = new Material[3];
    List<Renderer> list = new List<Renderer>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            list.Add(transform.GetChild(i).GetComponent<Renderer>());
        }
    }

    public void ChangeSkin(int index)
    {
        switch (index)
        {
            case 0:
                if (material[0] == null)
                {
                    if (material[1] != null)
                        index = 1;
                    else
                        index = 2;
                }
                break;
            case 1:
                if (material[1] == null)
                {
                    if(material[0] != null)
                        index = 0;
                    else
                        index = 2;
                }
                break;
            case 2:
                if (material[2] == null)
                {
                    if (material[1] != null)
                        index = 1;
                    else
                        index = 0;
                }
                break;
        }

        if (material[index] == null) return;

        foreach(var m in list)
        {
            m.material = material[index];
        }
    }
}
