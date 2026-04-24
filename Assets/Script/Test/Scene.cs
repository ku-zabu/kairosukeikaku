using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{

    [SerializeField] private string _loadScene; //シーン名を記述

    public void SceneChange()
    {
        SceneManager.LoadScene(_loadScene);
    }

}