using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Plantstage_choice : MonoBehaviour
{
    public float Scene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Onclick()
    {
        SceneManager.LoadScene("Scene");
    }
}
