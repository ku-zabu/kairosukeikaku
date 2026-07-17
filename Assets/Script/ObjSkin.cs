using UnityEngine;

public class ObjSkin : MonoBehaviour
{
    [SerializeField] Material[] material = new Material[3];
    Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
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
                    if (material[0] != null)
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

        rend.material = material[index];
    }
}
