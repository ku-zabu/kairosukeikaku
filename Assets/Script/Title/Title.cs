using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルシーンの管理
/// </summary>
public class Title : MonoBehaviour
{
    //Optionのデータを補完するオブジェクトのプレハブ
    [SerializeField] private GameObject OptionDataBox;
    //設定のオブジェクト
    private GameObject option;
    //ゲームシーン
    [SerializeField] private Scene gameScene;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //OptionDataBoxの生成状況確認とoptionオブジェクトの取得
        try
        {
            //アタッチ忘れ対策
            if (OptionDataBox == null)
                OptionDataBox = Resources.Load<GameObject>("Prefabs/Option/OptionDataBox");
            //OptionDataBoxがシーン上に存在しない場合は生成
            if (GameObject.Find(OptionDataBox.name) == null)
            {
                var ins = Instantiate(OptionDataBox);
                ins.name = OptionDataBox.name;//名前を統一
            }
            //optionオブジェクトの取得
            option = transform.GetChild(1).gameObject;
            option.SetActive(false);//最初は非表示
        }
        catch (System.NullReferenceException) //null参照例外をキャッチ
        {
            Debug.LogError("OptionDataBoxが見つかりません。プレハブの設定を確認してください。");
        }
    }

    //GameStart
    public void StartGameButton()
    {
        SceneManager.LoadScene(gameScene.name);
    }

    //OptionOpen
    public void OptionButton()
    {
        option.SetActive(true);
    }

    public void QuitGameButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲーム開発中
#else
        Application.Quit();//ゲーム開発後
#endif
    }
}
