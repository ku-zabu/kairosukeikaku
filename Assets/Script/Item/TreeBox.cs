using System.Collections.Generic;
using UnityEngine;

public class TreeBox : ItemBox
{
    List<Tree> trees = new List<Tree>();

    private void Start()
    {
        trees.AddRange(FindObjectsByType<Tree>(FindObjectsSortMode.None));
    }
}
