using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Q : MonoBehaviour
{
    [SerializeField] GameObject Stagebutoon;
    [SerializeField] GameObject Option;
    [SerializeField, Header("ì«Ç›çûÇ›ÇΩÇ¢ÉVÅ[Éì")] private SceneAsset gameScene;

    public void OnButtonClicked()
    {
        if (Stagebutoon != null && Option != null)
        {
            bool nextStageState = !Stagebutoon.activeSelf;

            Stagebutoon.SetActive(nextStageState);

            Option.SetActive(!nextStageState);

        }
    }
         public void SceneLoad()
    {
        SceneManager.LoadScene(gameScene.name);
    }

    

}


