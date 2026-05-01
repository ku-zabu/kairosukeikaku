using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 音量設定のオプションデータボックスを管理するクラス
/// </summary>
public class OptionAudio : MonoBehaviour, OptionTemp
{
    //スライダーとテキストのリスト
    List<Slider> sliders = new List<Slider>();
    List<TextMeshProUGUI> dropdowns = new List<TextMeshProUGUI>();

    /// <summary>
    /// Startより遅いと間に合わないため、Awakeでスライダーとテキストの取得を行う
    /// </summary>
    void Awake()
    {
        try //音量設定のスライダーの取得とテキストの取得を試みる
        {
            var sliderList = transform.Find("Slider");
            foreach (Transform child in sliderList) 
            {
                sliders.Add(child.GetComponent<Slider>());
            }
            if(sliders.Count == 0)
            {
                Debug.LogError("音量設定のスライダーが見つかりません。ヒエラルキーを確認してください。");
            }
            var txtList = transform.Find("Value");
            foreach (Transform child in txtList) 
            {
                dropdowns.Add(child.GetComponent<TextMeshProUGUI>());
            }
            if(dropdowns.Count == 0)
            {
                Debug.LogError("音量設定のテキストが見つかりません。ヒエラルキーを確認してください。");
            }
        }
        catch (System.NullReferenceException) //null参照例外をキャッチ
        {
            Debug.LogError("音量設定のスライダーまたはテキストが見つかりません。ヒエラルキーを確認してください。");
        }

        //数の不一致の確認
        if (sliders.Count != dropdowns.Count)
        {
            Debug.LogError("音量設定のスライダーとテキストの数が異なります。ヒエラルキーを確認してください。");
            return;
        }
    }
    /// <summary>
    /// 数値の視覚化
    /// </summary>
    /// <param name="index"></param>
    public void OnSliderValueChanged(int index)
    {
        if (index < 0 || index >= sliders.Count)
        {
            Debug.LogError("Index out of range!");
            return;
        }
        float sliderValue = sliders[index].value * 100;
        dropdowns[index].text = sliderValue.ToString("F1")+"%";
    }

    public void SetData(AllOption all)
    {
        var volumeOption = all.volumeOption;
        sliders[0].value = volumeOption.MVolume;
        sliders[1].value = volumeOption.BGMVolume;
        sliders[2].value = volumeOption.SEVolume;
        OnSliderValueChanged(0);
        OnSliderValueChanged(1);
        OnSliderValueChanged(2);
    }

    public AllOption GetData()
    {
        var volumeOption = new VolumeOption
        {
            MVolume = sliders[0].value,
            BGMVolume = sliders[1].value,
            SEVolume = sliders[2].value
        };

        return new AllOption
        {
            volumeOption = volumeOption
        };
    }
}
