using UnityEngine;

public class Artifact : ItemTemp
{
    public override void Interact(Player_FP player)
    {
        player.StopCameraAndMove();
        var canvas = Resources.Load<GameObject>("Prefabs/Item/ArtifactCanvas");
        if (canvas == null) Debug.LogError("ArtifactCanvas‚ÌŽæ“¾‚ÉŽ¸”s‚µ‚Ü‚µ‚½");
        var obj = Instantiate(canvas, new Vector3(0, -10, 0), Quaternion.identity);
        obj.name = "ArtifactCanvas";
        var copy = obj.GetComponent<Artifact>();
        copy.GetPlayerScript(player);
    }

    Animator anime;
    Player_FP pl;

    private void Start()
    {
        if (gameObject.name != "ArtifactCanvas") return;
        anime = GetComponent<Animator>();
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = GameObject.Find("UI/UICamera").GetComponent<Camera>();
        canvas.planeDistance = 5;
        anime.SetBool("Open", true);
        if (itemIcon == null) itemIcon = Resources.Load<GameObject>("Prefabs/Item/Icon/ArtifactIcon");
        if (itemText == null) itemText = Resources.Load<GameObject>("Prefabs/Item/Detail/ArtifactDetail");
    }

    public void GetPlayerScript(Player_FP player)
    {
        pl = player;
    }

    public void ChoiceManual(bool choice)
    {
        if (choice)
        {
            Destroy(GameObject.Find("ArtifactBox"));
            pl.SetItems(itemIcon, itemText);
        }
        
        pl.GetArtifact(choice);
        Destroy(gameObject);
    }
}
