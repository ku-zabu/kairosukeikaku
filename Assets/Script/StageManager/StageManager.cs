using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] Int32 waitTime;
    PlayerInput input;
    bool menu;
    List<Button> buttons = new List<Button>();
    Text menuText;

    int nowTime = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        //ここで別のスクリプトの関数を呼び、nowTimeを渡す
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
    async UniTask ChangeTime()
    {
        if (!menu) Input_able("Player", false);
        else       Input_able("UI", false);

        for(int i=0; i < 3; i++)
        {
            buttons[i].interactable = false;
        }

        await UniTask.Delay(waitTime);

        for (int i = 0; i < 3; i++)
        {
            if (i == nowTime) continue;
            buttons[i].interactable = true;
        }

        if (!menu) Input_able("Player", true);
        else       Input_able("UI", true);
    }

    /// Menu切り替え
    /// <summary> Menuを開閉 </summary>
    public void OnMenu()
    {
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

    /// アクション
    /// <summary> 調べる </summary>
    public void OnInvestigate()
    {

    }
    ///<summary> 取得 </summary>
    public void OnAcquire()
    {

    }
    ///<summary> 行動 </summary>
    public void OnAction()
    {

    }
}
