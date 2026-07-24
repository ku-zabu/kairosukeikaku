using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Terepo : ItemTemp
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void Action(int i)
    {
        var p = FindAnyObjectByType<Player_Beye>().transform;
        p.position = transform.position;
    }

}
