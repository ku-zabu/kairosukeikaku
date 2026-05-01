using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カメラ設定のオプションデータボックスを管理するクラス
/// </summary>
public class OptionCamera : MonoBehaviour, OptionTemp
{
    //スライダーとボタンのリスト
    private List<Slider> sliders = new List<Slider>();
    private Button[,] buttons = new Button[2, 2];
    private bool[] move = new bool[2]; //カメラの回転のリバース機能の状態を管理する配列

    /// <summary>
    /// Startより遅いと間に合わないため、Awakeでスライダーとボタンの取得を行う
    /// </summary>
    void Awake()
    {
        try //カメラ設定のスライダーとボタンの取得を試みる
        {
            var Contents = transform.Find("Scroll View/Viewport/Content");
            var sliderList = Contents.Find("Sliders");
            foreach (Transform child in sliderList)
            {
                sliders.Add(child.GetComponent<Slider>());
            }

            var buttonList = Contents.Find("Buttons");
            var buttonY = buttonList.Find("MoveY");
            foreach (Transform child in buttonY)
            {
                buttons[0, child.GetSiblingIndex()] = child.GetComponent<Button>();
            }
            var buttonX = buttonList.Find("MoveX");
            foreach (Transform child in buttonX)
            {
                buttons[1, child.GetSiblingIndex()] = child.GetComponent<Button>();
            }
        }
        catch (System.NullReferenceException) //null参照例外をキャッチ
        {
            Debug.LogError("カメラ設定のスライダーまたはボタンが見つかりません。ヒエラルキーを確認してください。");
            return;
        }
    }
    /// <summary>
    /// カメラの回転のリバース機能を切り替える関数
    /// </summary>
    /// <param name="type"></param>
    public void ChangeMove(bool type)
    {
        if (type)
        {
            move[0] = !move[0];
            ChangedColor(0, move[0]);
        }
        else
        {
            move[1] = !move[1];
            ChangedColor(1, move[1]);
        }
    }

    /// <summary>
    /// ボタンの色を切り替える関数
    /// </summary>
    /// <param name="t">ボタンの種類（0: Y軸, 1: X軸）</param>
    /// <param name="m">リバース機能の状態</param>
    void ChangedColor(int t,bool m)
    {
        var colors = buttons[t, 0].colors;
        colors.normalColor = !m ? new Color32(255, 255, 255, 255) : new Color32(255, 255, 255, 50);
        buttons[t, 0].colors = colors;

        colors = buttons[t, 1].colors;
        colors.normalColor = m ? new Color32(255, 255, 255, 255) : new Color32(255, 255, 255, 50);
        buttons[t, 1].colors = colors;
    }


    public void SetData(AllOption all)
    {
        var cameraOption = all.cameraOption;
        sliders[0].value = cameraOption.SpeedY;
        sliders[1].value = cameraOption.SpeedX;
        move[0] = cameraOption.MoveY;
        move[1] = cameraOption.MoveX;
        ChangedColor(0, move[0]);
        ChangedColor(1, move[1]);
    }

    public AllOption GetData()
    {

        CameraOption cameraOption = new CameraOption
        {
            SpeedY = sliders[0].value,
            SpeedX = sliders[1].value,
            MoveY = move[0],
            MoveX = move[1]
        };
        return new AllOption{
            cameraOption = cameraOption
        };
    }
}
