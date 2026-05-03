using UnityEditor.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PC_Menu : MonoBehaviour
{
    int idx = -1;
    [SerializeField] private Transform iconContent;
    [SerializeField] private Transform textContent;
    [SerializeField] private GameObject option;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform image = transform.Find("BackSecondImage/Box/ItemImage");
        if (iconContent == null) iconContent = image.GetChild(0).GetChild(0).GetChild(0);
        if (textContent == null) textContent = image.GetChild(1).GetChild(0).GetChild(0);
        if (option == null) option = transform.Find("OptionBack").gameObject;
        option.SetActive(false);
    }

    public void AddItems(GameObject icon, GameObject text)
    {
        var obj = Instantiate(icon, Vector3.zero, Quaternion.identity, iconContent);
        obj.transform.localPosition = new Vector3(0f, 0f, 0f);
        obj.transform.localRotation = Quaternion.identity;
        var but = obj.GetComponent<Button>();
        but.onClick.AddListener(() => OpenText(obj.transform));

        obj = Instantiate(text, Vector3.zero, Quaternion.identity, textContent);
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localRotation = Quaternion.identity;
        obj.SetActive(false);
    }

    public void AttentionBox(Transform obj)
    {
        obj.SetAsLastSibling();
    }


    void OpenText(Transform tran)
    {
        if (idx >= 0)
            textContent.GetChild(idx).gameObject.SetActive(false);

        idx = tran.GetSiblingIndex();
        textContent.GetChild(idx).gameObject.SetActive(true);
    }

    public void OpenOption()
    {
        option.SetActive(true);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Title");
    }
}
