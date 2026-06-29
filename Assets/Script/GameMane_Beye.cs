using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMane_Beye : MonoBehaviour
{
    ItemChecker checker;
    List<IDisplayTemp> itemDisplays = new List<IDisplayTemp>();
    PlayerInput input;

    int occasion;

    bool menu = false;
    Player_Beye player;
    Ui_Beye ui;

    [SerializeField] float awaitTime;

    ItemTemp setItem;
    GimmickTemp setGimmick;

    bool end = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        checker = GetComponent<ItemChecker>();
        itemDisplays.AddRange(FindObjectsByType<IDisplayTemp>(FindObjectsSortMode.None));
        input = GetComponent<PlayerInput>();
        input.SwitchCurrentActionMap("Player");
        player= FindAnyObjectByType<Player_Beye>();
        ui = FindAnyObjectByType<Ui_Beye>();

        occasion = 1;
        foreach (var dis in itemDisplays)
            dis.Check(occasion);
    }

    private void Update()
    {
        var value = input.actions["Move"].ReadValue<Vector2>();
        if (value != Vector2.zero)
            player.Move(value);

        if(setItem != null)
        {
            if (!setItem.transform.parent.gameObject.activeSelf) Separation();
        }
        if(setGimmick != null)
        {
            if (!setGimmick.transform.parent.gameObject.gameObject.activeSelf) Separation();
        }
    }

    public void OnMenu(InputValue value = null)
    {
        menu = !menu;
        input.SwitchCurrentActionMap(menu ? "UI" : "Player");
        ui.OnMenu(menu);
    }

    public void ContactItem(ItemTemp item)
    {
        setItem = item;
        ui.ContactItem();
    }
    public void ContactGimmick(GimmickTemp gimmick)
    {
        setGimmick = gimmick;
        ui.ContactGimmick();
    }

    public void Separation()
    {
        setItem = null;
        setGimmick = null;
        ui.Separation();
    }

    void OnPast(InputValue value)
    {
        if (occasion != 0) StreamTrigger(0);
    }

    void OnCurrent(InputValue value)
    {
        if (occasion != 1) StreamTrigger(1);
    }

    void OnFuture(InputValue value)
    {
        if (occasion != 2) StreamTrigger(2);
    }

    void StreamTrigger(int value)
    {
        ui.StreamTrigger(value);
    }

    public async UniTask StreamTime(int value)
    {
        input.DeactivateInput();
        occasion = value;
        foreach (var dis in itemDisplays)
            dis.Check(occasion);
        await UniTask.Delay(TimeSpan.FromSeconds(awaitTime));
        input.ActivateInput();
        input.SwitchCurrentActionMap(menu ? "UI" : "Player");
        ui.StreamEnd();
    }

    public void OnInvestigate(InputValue value = null)
    {
        input.SwitchCurrentActionMap("Item");
        if (setItem != null)
            ui.OnInvestigate(setItem.inversText);
        else if(setGimmick != null)
            ui.OnInvestigate(setGimmick.inversText);
    }

    public void OnAcquire(InputValue value = null)
    {
        input.SwitchCurrentActionMap("Item");
        if(setItem != null)
        {
            setItem.gameObject.SetActive(false);
            ui.OnAcquire(setItem.acquireText);
            checker.AddItem(setItem.itemNo);
            setItem = null;
        }
    }
    /// <summary>
    /// 行動ボタン
    /// </summary>
    /// <param name="value"></param>
    public void OnAction(InputValue value = null)
    {
        input.SwitchCurrentActionMap("Item");
        if (setGimmick != null)
        {
            if (setGimmick.itemNo < 0)
            {
                if(checker.UseItem(setGimmick.needItem))
                {
                    setGimmick.itemNo = setGimmick.needItem;
                    ui.OnAction(setGimmick.actionText);
                }
                else
                {
                    ui.OnAction("何か、足りない気がする");
                }
            }
            else
            {
                if (!setGimmick.uniqueTrigger)
                {
                    if (setGimmick.UniqueGimmick())
                    {
                        ui.OnAction(setGimmick.uniqueText);
                    }
                    else
                    {
                        checker.GetItem(setGimmick.itemNo);
                        setGimmick.itemNo = -1;
                        ui.OnAction(setGimmick.getItem);
                    }
                }
                else
                {
                    ui.OnAction("これ以上、触るのは止そう");
                }
            }
        }
        
    }


    public void OnCloseM(InputValue value = null)
    {
        if (!end)
        {
            input.SwitchCurrentActionMap(menu ? "UI" : "Player");
            ui.CloseMessage();
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲーム開発中
#else
    Application.Quit();//ゲーム開発後
#endif
        }
    }

    public void Completion()
    {
        end = true;
        input.SwitchCurrentActionMap("End");
        ui.OnAction("脱出成功！！");
    }
}
