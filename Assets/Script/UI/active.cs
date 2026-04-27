using UnityEngine;
using UnityEngine.UI;

public class active : MonoBehaviour
{
    [SerializeField] GameObject textGameObject;
    [SerializeField] Button button;
    private void Start()
    {
        bool isActive = false;

        button.onClick.AddListener(() =>
        {
            isActive = !isActive;
            textGameObject.SetActive(isActive);
        });
    }
}