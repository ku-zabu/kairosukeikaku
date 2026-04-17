using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Q : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
