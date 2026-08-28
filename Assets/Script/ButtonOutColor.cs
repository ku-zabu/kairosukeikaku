using UnityEngine;
using UnityEngine.UI;

public class ButtonOutColor : MonoBehaviour
{

    Button button;
    [SerializeField] Outline outline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(button.interactable)
        {
            outline.effectColor = new Color32(180, 180, 0, 255);
        }
        else
        {
            outline.effectColor = new Color32(50, 50, 50, 255);
        }
    }
}
