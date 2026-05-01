using UnityEngine;

/// <summary>
/// 設定のデータを管理するクラス
/// </summary>
public class OptionDataSet : MonoBehaviour
{
    //オプションの初期データを補完するオブジェクト
    [SerializeField] private EarlyOptionDataFile earlyOptionDataFile;
    private AllOption allOption;

    private bool title = true;//タイトルシーンかどうか

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            //アタッチ忘れ対策
            if (earlyOptionDataFile == null)
                earlyOptionDataFile = Resources.Load<EarlyOptionDataFile>("Data/EarlyOptionDataFile");
            //オプションの初期データを補完
            allOption = earlyOptionDataFile.GetAllOption();
        }
        catch (System.NullReferenceException) //null参照例外をキャッチ
        {
            Debug.LogError("EarlyOptionDataFileが見つかりません。スクリプタブルオブジェクトの設定を確認してください。");
        }
        DontDestroyOnLoad(gameObject);//シーンを跨いでオプションのデータを保持するために、オブジェクトを破棄しないようにする
    }

    /// <summary>
    /// Optionのデータを取得するメソッド
    /// </summary>
    /// <returns></returns>
    public AllOption GetAllOption()
    {
        return allOption;
    }

    /// <summary>
    /// 設定の更新を行うメソッド
    /// </summary>
    /// <param name="all"></param>
    public void SetAllOption(AllOption all)
    {
        allOption = all;
    }

    /// <summary>
    /// データのリセットを行うメソッド
    /// </summary>
    public void ReData()
    {
        allOption = earlyOptionDataFile.GetAllOption();
    }
}
