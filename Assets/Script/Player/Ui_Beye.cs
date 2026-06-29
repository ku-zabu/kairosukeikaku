using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Beye : MonoBehaviour
{
    GameMane_Beye gameMane;

    GameObject menuParent;
    Button menuChangeButton;
    Text menuChangeText;

    Button[] streamButton = new Button[3];
    int occasion = 1;

    Button[] itemButton = new Button[3];

    GameObject messageParent;
    Text messageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameMane = FindAnyObjectByType<GameMane_Beye>();

        menuParent = transform.Find("MenuImage").gameObject;
        menuParent.SetActive(false);

        var buttonList = transform.Find("ButtonList");
        menuChangeButton = buttonList.Find("MenuButton").GetComponent<Button>();
        menuChangeText = menuChangeButton.transform.GetComponentInChildren<Text>();
        menuChangeText.text = " V:Menu";

        for (int i = 0; i < streamButton.Length; i++)
            streamButton[i] = buttonList.Find("StreamButton_" + i).GetComponent<Button>();
        streamButton[1].interactable = false;

        var actionList = transform.Find("ActionList");
        for (int i = 0; i < itemButton.Length; i++)
        {
            itemButton[i] = actionList.GetChild(i).GetComponent<Button>();
            itemButton[i].interactable = false;
        }

        messageParent = transform.Find("MessageBack").gameObject;
        messageText = messageParent.GetComponentInChildren<Text>();
        messageParent.SetActive(false);
    }

    public void Menu()
    {
        gameMane.OnMenu();
    }
    public void OnMenu(bool menu)
    {
        menuParent.SetActive(menu);
        menuChangeText.text = menu ? " V:Close" : " V:Menu";
    }

    public void StreamTrigger(int occ)
    {
        if (occ < 0 || streamButton.Length <= occ) 
        {
            Debug.LogError("’l‚ª•s³‚È’l");
            return;
        }
        occasion = occ;
        menuChangeButton.interactable = false;
        for (int i = 0; i < streamButton.Length; i++)
            streamButton[i].interactable = false;
        gameMane.StreamTime(occasion).Forget();
    }
    public void StreamEnd()
    {
        menuChangeButton.interactable = true;
        for (int i = 0; i < streamButton.Length; i++)
            streamButton[i].interactable = true;
        streamButton[occasion].interactable = false;
    }

    public void ContactItem()
    {
        for(int i = 0; i < 2; i++)
        {
            itemButton[i].interactable = true;
        }
    }

    public void ContactGimmick()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 1) continue;
            itemButton[i].interactable = true;
        }
    }

    public void Separation()
    {
        foreach (var button in itemButton)
            button.interactable = false;
    }

    public void Investigate()
    {
        gameMane.OnInvestigate();
    }
    public void OnInvestigate(string text)
    {
        messageParent.SetActive(true);
        messageText.text = text;
    }

    public void Acquire()
    {
        gameMane.OnAcquire();
    }
    public void OnAcquire(string text)
    {
        Separation();
        messageParent.SetActive(true);
        messageText.text = text;
    }

    public void CloseM()
    {
        gameMane.OnCloseM();
    }
    public void CloseMessage()
    {
        messageParent.SetActive(false);
    }

    public void Action()
    {
        gameMane.OnAction();
    }
    public void OnAction(string text)
    {
        messageParent.SetActive(true);
        messageText.text = text;
    }
}
/*
 
 
    
 
 */