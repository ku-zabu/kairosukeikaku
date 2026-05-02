using UnityEngine;

public class Artifact : ItemTemp
{
    public override void Interact(Player_FP player)
    {
        player.GetArtifact();
        base.Interact();
    }
}
