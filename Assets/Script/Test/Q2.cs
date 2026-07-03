using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Q2 : MonoBehaviour
{
    [SerializeField, Header("ì«Ç›çûÇ›ÇΩÇ¢ÉVÅ[Éì")] private SceneAsset gameScene;

    public void OnButtonClicked()
    {
        SceneManager.LoadScene(gameScene.name);
    }
}
