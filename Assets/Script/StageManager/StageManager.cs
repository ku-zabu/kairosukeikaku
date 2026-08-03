using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] float waitTime;
    PlayerInput input;
    bool menu;
    List<Button> buttons = new List<Button>();
    Text menuText;

    GameObject logParent;
    Text logText;

    GameObject HintParent;

    public int nowTime = 1;

    ItemTemp item;
    /// <summary>現在所持しているアイテム</summary>
    [SerializeField] List<int> itemList = new List<int>();
    List<ItemBox> itemBoxs = new List<ItemBox>();

    bool goal = false;

    List<StageSkinn> stageSkins = new List<StageSkinn>();
    List<ObjSkin> objSkins = new List<ObjSkin>();

    [SerializeField] string exitScene;

    GameObject subCamera;
    Fade fade;

    Player_Beye player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        subCamera = GameObject.Find("SubCamera").gameObject;
        subCamera.SetActive(false);

        fade = FindAnyObjectByType<Fade>();

        var parent = transform.Find("Canvas/ButtonList");
        for(int i = 0; i < parent.childCount; i++)
        {
            buttons.Add(parent.GetChild(i).GetComponent<Button>());
            if (parent.GetChild(i).name == "MenuButton")
                menuText = parent.GetChild(i).GetComponentInChildren<Text>();
        }
        buttons[1].interactable = false;
        buttons[4].interactable = false;
        buttons[5].interactable = false;
        buttons[6].interactable = false;

        menu = false;
        menuText.text = " V - メニュー";

        input = GetComponent<PlayerInput>();
        Input_able("Option", false);
        Input_able("Title", false);
        Input_able("UI", false);
        Input_able("Item", false);
        Input_able("End", false);
        Input_able("Player", true);

        logParent = transform.Find("Canvas/TextImage").gameObject;
        logText = logParent.transform.Find("LogText").GetComponent<Text>();
        logParent.SetActive(false);

        HintParent = transform.Find("Canvas/HintImage").gameObject;
        HintParent.SetActive(false);

        itemBoxs.AddRange(FindObjectsByType<ItemBox>(FindObjectsSortMode.None));
        foreach (var box in itemBoxs)
            box.ActiveChanger(nowTime);

        stageSkins.AddRange(FindObjectsByType<StageSkinn>(FindObjectsSortMode.None));
        foreach(var stage in stageSkins)
            stage.ChangeSkin(nowTime);

        objSkins.AddRange(FindObjectsByType<ObjSkin>(FindObjectsSortMode.None));
        foreach( var obj in objSkins)
            obj.ChangeSkin(nowTime);

        player = FindAnyObjectByType<Player_Beye>();

        GameManager.source.ChangeBgm(false);
    }

    void Input_able(string name, bool a)
    {
        input.SwitchCurrentActionMap(name);
        if (a) input.currentActionMap.Enable();
        else   input.currentActionMap.Disable();
    }

    /// 時間切り替え
    /// <summary> 過去ヘ </summary>
    public void OnPast()
    {
        if (nowTime == 0) return;
        nowTime = 0;
        ChangeTime().Forget();
    }
    /// <summary> 現在ヘ </summary>
    public void OnCurrent()
    {
        if (nowTime == 1) return;
        nowTime = 1;
        ChangeTime().Forget();
    }
    /// <summary> 未来ヘ </summary>
    public void OnFuture()
    {
        if (nowTime == 2) return;
        nowTime = 2;
        ChangeTime().Forget();
    }
    /// <summary> 切り替え後 waitTime ミリ秒待つ </summary>
    /// <returns></returns>
    async UniTask ChangeTime()
    {
        GameManager.source.PlaySe("Time");

        player.rb.linearVelocity = Vector3.zero;

        if (!menu) Input_able("Player", false);
        else       Input_able("UI", false);

        for (int i = 0; i < 4; i++) 
        {
            buttons[i].interactable = false;
        }

        subCamera.SetActive(true);

        await UniTask.Yield();
        fade.FadeStart(waitTime).Forget();
        await UniTask.Yield();


        foreach (var box in itemBoxs)
            box.ActiveChanger(nowTime);

        foreach (var stage in stageSkins)
            stage.ChangeSkin(nowTime);

        foreach (var obj in objSkins)
            obj.ChangeSkin(nowTime);

        Tree[] trees = FindObjectsByType<Tree>(FindObjectsSortMode.None);
        foreach (var tree in trees)
            tree.ChangeMode(nowTime);

        item = null;
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime));

        for (int i = 0; i < 4; i++)
        {
            if (i == nowTime) continue;
            buttons[i].interactable = true;
        }

        if (!menu) Input_able("Player", true);
        else       Input_able("UI", true);

        SetItem(item);
    }

    /// Menu切り替え
    /// <summary> Menuを開閉 </summary>
    public void OnMenu()
    {
        return;
        menu = !menu;
        string t = " V - ";
        menuText.text = t + (menu ? "閉じる" : "メニュー");
        if(menu) //OpenMenu
        {
            Input_able("Player", false);
            Input_able("UI", true);
        }
        else     //CloseMenu
        {
            Input_able("UI", false);
            Input_able("Player", true);
        }
    }

    /// <summary> アイテムを参照 </summary>
    /// <param name="getItem"></param>
    public void SetItem(ItemTemp getItem)
    {
        if (getItem == null)
        {
            buttons[4].interactable = false;
            buttons[5].interactable = false;
            buttons[6].interactable = false;
            return;
        }
        if (getItem.inversText != "")       
            buttons[4].interactable = true;
        else
            buttons[4].interactable = false;

        if (getItem.acquirText != "")
            buttons[5].interactable = true;
        else
            buttons[5].interactable = false;

        if (getItem.actionText != "")
            buttons[6].interactable = true;
        else
            buttons[6].interactable = false;

        item = getItem;
    }
    /// <summary> アイテムを忘却 </summary>
    public void unsetItem(bool n = false)
    {
        for(int i=4;i<buttons.Count;i++)
        {
            buttons[i].interactable = false;
        }
        if (n)
            item = null;
    }

    /// アクション
    /// <summary> 調べる </summary>
    public void OnInvestigate()
    {
        if (!buttons[4].interactable || item == null) return;
        if(item.hint)
            OpenHint();
        else
            OpenText(item.inversText);
    }
    ///<summary> 取得 </summary>
    public void OnAcquire()
    {
        if (!buttons[5].interactable || item == null) return;
        OpenText(item.acquirText);
        itemList.Add(item.Acquir());
        unsetItem();
        itemList.Remove(0);
        item.ChangerSet();
        SetItem(item);
    }
    ///<summary> 行動 </summary>
    public void OnAction()
    {
        if (!buttons[6].interactable || item == null) return;
        OpenText(item.actionText);
        if(item.itemNo != 0)
            itemList.Remove(item.itemNo);
        item.Action(nowTime);
        SetItem(item);
    }
    /// <summary> 何かしらコメントを出す </summary>
    void OpenText(string comment)
    {
        logParent.SetActive(true);
        logText.text = comment;
        if (!goal)
        {
            Input_able("Player", false);
            Input_able("Item", true);
        }
        else
        {
            Input_able("Player", false);
            Input_able("End", true);
        }
    }

    void OpenHint()
    {
        HintParent.SetActive(true);
        if (!goal)
        {
            Input_able("Player", false);
            Input_able("Item", true);
        }
        else
        {
            Input_able("Player", false);
            Input_able("End", true);
        }
    }

    public void OnCloseM()
    {
        if (!goal)
        {
            HintParent.SetActive(false);
            logParent.SetActive(false);
            Input_able("Item", false);
            Input_able("Player", true);
        }
        else
        {
            OnEnd();
        }
    }

    public bool ItemCheck(int i)
    {
        if(itemList.Count == 0) return false;
        return itemList.Contains(i);
    }

    public void Goal()
    {
        goal = true;
        Input_able("End", true);
        Input_able("Player", false);
        OpenText("脱出成功！");
    }
    
    public void OnEnd()
    {
        SceneManager.LoadScene(exitScene);
    }

}
