using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeBox : ItemBox
{
    List<Tree> trees = new List<Tree>();

    private void Start()
    {
        trees.AddRange(FindObjectsByType<Tree>(FindObjectsSortMode.None));
    }
}
