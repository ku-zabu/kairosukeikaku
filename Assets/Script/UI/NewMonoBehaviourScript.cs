using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject textGameObject;
    [SerializeField] Button button;
    private void Start()
    {
        bool isActive = true;

        button.onClick.AddListener(() =>
        {
            isActive = !isActive;
            textGameObject.SetActive(isActive);
        });
    }
}
