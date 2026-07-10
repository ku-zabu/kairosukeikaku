using UnityEngine;

public class Tree : ItemTemp
{
    StageManager sm;
    public int mode = 1;
    public string[] inversTexts = new string[3];
    public string[] acquirTexts = new string[3];
    public string[] actionTexts = new string[3];
    public bool[] water = new bool[2];
    public int needItemNo;
    public bool setItem;

    private void Awake()
    {
        sm = FindAnyObjectByType<StageManager>();
    }

    public void ChangerSet()
    {
        inversText = mode switch
        {
            1 => setItem ? "Ží‚ªA‚¦‚Ä‚ ‚é" : "‰½‚©A‚¦‚ê‚»‚¤‚¾",
            2 => "‚È‚ñ‚Æ‚©“o‚ê‚»‚¤‚¾",
            3 => "—¬Î‚É“o‚é‚±‚Æ‚Í‚Å‚«‚È‚¢",
            _ => "‚È‚ñ‚©ŠÔˆá‚Á‚Ä‚é"
        };

        acquirText = mode switch
        {
            1 => setItem ? "Ží‚ð‰ñŽû‚µ‚½" : "",
            2 => "",
            3 => "",
            _ => "‚È‚ñ‚©ŠÔˆá‚Á‚Ä‚é"
        };

        bool item = sm.ItemCheck(needItemNo);
        actionText = mode switch
        {
            1 => item ? "Ží‚ðA‚¦‚½" : "",
            2 => "‚È‚ñ‚Æ‚©“o‚é‚±‚Æ‚ª‚Å‚«‚½",
            3 => "",
            _ => ""
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangerSet();
    }


}
