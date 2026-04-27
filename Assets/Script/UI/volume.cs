using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class volume : MonoBehaviour
{

    [SerializeField] Slider[] sliders = new Slider[5];

    private void Start()
    {

        DontDestroyOnLoad(gameObject);


    }
    
}
