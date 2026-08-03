using System.Collections.Generic;
using UnityEngine;

public class Watering_Can : ItemTemp
{
    List<Tree> trees = new List<Tree>();

    private void Start()
    {
        trees.AddRange(FindObjectsByType<Tree>(FindObjectsSortMode.None));
    }

    public override void Action(int i)
    {
        var mas = FindAnyObjectByType<StageManager>().nowTime;

        foreach (Tree tree in trees)
        {
            if (tree.setItem[mas] || tree.mode[mas] != 1)
            {
                tree.water[mas] = true;
            }
        }

        GameManager.source.PlaySe("Lever");
    }
}
