using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オプションのタブと対応するオプションデータボックスを管理するクラス
/// </summary>
public class Option : MonoBehaviour
{
    //Optionのタブのボタン
    private List<Button> buttons = new List<Button>();
    //Optionの詳細のデータを補完するオブジェクト
    private List<GameObject> optionDataBoxes = new List<GameObject>();
    //現在選択されているタブのインデックス
    int idx;

    [SerializeField] private OptionDataSet optionDataSet;

    private List<OptionTemp> option = new List<OptionTemp>();

    private Player_FP player;

    /// <summary>
    /// タブの色を変えて、対応するオプションデータボックスを表示する
    /// </summary>
    /// <param name="nextIdx"></param>
    void Change(int nextIdx)
    {
        //色を戻して非表示にする
        var colors = buttons[idx].colors;
        colors.normalColor = new Color32(255, 255, 255, 50);
        buttons[idx].colors = colors;
        optionDataBoxes[idx].SetActive(false);

        //次のタブのインデックスを更新
        idx = nextIdx;

        //色を変えて表示する
        colors = buttons[idx].colors;
        colors.normalColor = new Color32(255, 255, 255, 255);
        buttons[idx].colors = colors;
        optionDataBoxes[idx].SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try//タブのボタンとオプションデータボックスの取得
        {
            var buttonList = transform.Find("OptionListScrollView/Viewport/Content");
            foreach (Transform child in buttonList)
            {
                buttons.Add(child.GetComponent<Button>());
            }
            var optionDataBoxList = transform.Find("OptionMenu");
            foreach (Transform child in optionDataBoxList)
            {
                optionDataBoxes.Add(child.gameObject);
                option.Add(child.GetComponent<OptionTemp>());
                child.gameObject.SetActive(false);
            }
        }
        catch (System.NullReferenceException) //null参照例外をキャッチ
        {
            Debug.LogError("設定のタブ/オプションデータボックスが見つかりません。ヒエラルキーを確認してください。");
        }
    }


    private void OnEnable()
    {
        if (buttons.Count == 0) return;
        if (optionDataSet == null) optionDataSet = GameObject.Find("OptionDataBox").GetComponent<OptionDataSet>();
        //Optionの数値を反映
        foreach (var o in option)
        {
            o.SetData(optionDataSet.GetAllOption());
        }

    }

    private void OnDisable()
    {
        if (buttons.Count == 0) return;
        Change(0);
    }

    /// <summary>
    /// タブのボタンから呼び出される関数
    /// </summary>
    /// <param name="index"></param>
    public void OptionDataBoxOpen(int index)
    {
        Change(index);
    }

    /// <summary>
    /// Optionの閉じるボタンから呼び出される関数
    /// </summary>
    /// <param name="isSave"></param>
    public void CloseOption(bool isSave)
    {
        if (isSave)
        {
            AllOption[] op = new AllOption[option.Count];
            for (int i = 0; i < option.Count; i++)
                op[i] = option[i].GetData();

            AllOption setOp = new AllOption();
            //設定の項目が増えるとここも増えてしまう...他に記述方法はないのか...?
            for (int i = 0; i < op.Length; i++)
            {
                setOp.volumeOption.MVolume = Mathf.Max(setOp.volumeOption.MVolume, op[i].volumeOption.MVolume);
                setOp.volumeOption.BGMVolume = Mathf.Max(setOp.volumeOption.BGMVolume, op[i].volumeOption.BGMVolume);
                setOp.volumeOption.SEVolume = Mathf.Max(setOp.volumeOption.SEVolume, op[i].volumeOption.SEVolume);

                setOp.cameraOption.SpeedY = Mathf.Max(setOp.cameraOption.SpeedY, op[i].cameraOption.SpeedY);
                setOp.cameraOption.SpeedX = Mathf.Max(setOp.cameraOption.SpeedX, op[i].cameraOption.SpeedX);
                setOp.cameraOption.MoveY = setOp.cameraOption.MoveY || op[i].cameraOption.MoveY;
                setOp.cameraOption.MoveX = setOp.cameraOption.MoveX || op[i].cameraOption.MoveX;

                if (player == null)
                {
                    var obj = GameObject.Find("Player_FirstPerson");
                    if (obj != null)
                        player = obj.GetComponent<Player_FP>();
                }
                if (player != null) player.SetOptionData(setOp.cameraOption);
            }
            optionDataSet.SetAllOption(setOp);
        }
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Optionのリセットボタンから呼び出される関数
    /// </summary>
    public void ResetOption()
    {
        optionDataSet.ReData();
        OnEnable();
    }


}
