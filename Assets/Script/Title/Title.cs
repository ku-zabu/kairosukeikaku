using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// タイトルシーンの管理
/// </summary>
public class Title : MonoBehaviour
{
    [SerializeField, Header("OptionObject")] GameObject option;
    [SerializeField, Header("読み込みたいシーン")] private string gameScene;
    [SerializeField, Header("PlayerInput")] PlayerInput input;
    [SerializeField, Header("Animator")] Animator anima;
    int command = 3;

    [SerializeField, Header("BGMの音量")] Slider bgmVolume;
    [SerializeField, Header("SEの音量")] Slider seVolume;

    [SerializeField, Header("BGMの音量数")] Text bgmText;
    [SerializeField, Header("SEの音量数")] Text seText;

    private void Start()
    {
        AnimeD();
        option.SetActive(false);

        GameManager.source.ChangeBgm(true);

    }

    public void AnimeD()
    {
        input.SwitchCurrentActionMap("Option");
        input.currentActionMap.Disable();
        input.SwitchCurrentActionMap("Title");
        input.currentActionMap.Disable();
    }
    public void AnimeE()
    {
        input.ActivateInput();
        switch (command)
        {
            case 0: //ゲームスタート
                return;
            case 1: //オプションを開く
                input.SwitchCurrentActionMap("Title");
                input.currentActionMap.Disable();
                input.SwitchCurrentActionMap("Option");
                input.currentActionMap.Enable();
                break;
            case 2: //ゲームをやめる
                return;
            case 3: //オプションを閉じる
                input.SwitchCurrentActionMap("Option");
                input.currentActionMap.Disable();
                input.SwitchCurrentActionMap("Title");
                input.currentActionMap.Enable();
                break;
            default:
                Debug.LogError("未割当");
                break;

        }
    }

    public void AnimeC()
    {
        switch (command)
        {
            case 0: //ゲームスタート
                
                SceneManager.LoadScene(gameScene);
                return;

            case 1: //オプションを開く
                option.SetActive(true);
                var values = GameManager.source.GetValues();

                bgmVolume.value = values.x * 1000;
                seVolume.value = values.y * 1000;
                bgmText.text = $"{bgmVolume.value * 0.1f:F1}%";
                seText.text = $"{seVolume.value * 0.1f:F1}%";
                break;

            case 2: //ゲームをやめる
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲーム開発中
#else
        Application.Quit();//ゲーム開発後
#endif
                return;

            case 3: //オプションを閉じる
                option.SetActive(false);
                break;

            default:
                Debug.LogError("未割当");
                break;
        }
        anima.Play("Title_0");
    }

    //----------Title----------
    /// <summary> GameStart </summary>
    public void OnStart() { Operation(0); }
    /// <summary> OpenOption </summary>
    public void OnOption() { Operation(1); }
    /// <summary> QuitGame </summary>
    public void OnQuit() { Operation(2); }

    //----------Option----------
    /// <summary> 変更を適用 </summary>
    public void OnApply()
    {
        GameManager.source.SetValues(true, bgmVolume.value, seVolume.value);
        Operation(3);
    }
    /// <summary> 変更を破棄 </summary>
    public void OnCancel()
    {
        GameManager.source.SetValues(false);
        Operation(3);
    }

    /// <summary> 切り替え </summary>
    /// <param name="id"></param>
    void Operation(int id)
    {
        command = id;
        anima.Play("Title_1");
    }

    private void FixedUpdate()
    {
        if(input.currentActionMap.name == "Option")
        {
            var volume = new float2(input.actions["BGM"].ReadValue<float>(), input.actions["SE"].ReadValue<float>());
            bgmVolume.value += volume.x;
            seVolume.value += volume.y;

            bgmText.text = $"{bgmVolume.value * 0.1f:F1}%";
            seText.text = $"{seVolume.value * 0.1f:F1}%";

            GameManager.source.Confirmation(bgmVolume.value, seVolume.value, volume.y != 0);
        }
    }

    public void SeUp()
    {
        GameManager.source.Confirmation(bgmVolume.value, seVolume.value, true);
    }

}
