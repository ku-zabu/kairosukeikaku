using UnityEngine;

public class Tree : ItemTemp
{
    StageManager sm;
    public int[] mode = new int[3];
    string[] inversTexts = new string[3];
    string[] acquirTexts = new string[3];
    string[] actionTexts = new string[3];
    public bool[] water = new bool[3];
    public bool[] setItem = new bool[3];
    public int nowTime = 1;
    [SerializeField] Transform pos;

    private void Awake()
    {
        sm = FindAnyObjectByType<StageManager>();
        mode = new int[3];
        for (int i = 0; i < mode.Length; i++) mode[i] = 1;
        nowTime = 1;
        pos = transform.Find("First/Terepo").transform;
    }

    public void ChangeMode(int i)
    {
        switch (i)
        {
            case 0:
                break;
            case 1:
                if (setItem[0] && water[0])
                    mode[1] = 2;
                else
                    mode[1] = mode[0];

                break;
            case 2:
                if (setItem[0] && water[1])
                    mode[2] = mode[1] + 1;
                else if(!setItem[0] && !water[0] && setItem[1] && water[1])//‚±‚±‚ðC³
                    mode[2] = 2;
                else
                    mode[2] = mode[1];

                break;
        }
        nowTime = i;
        ChangerSet();
    }

    public override void ChangerSet()
    {
        switch (mode[nowTime])
        {
            case 1:
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);

                break;
            case 2:
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 3:
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                break;
        }

        inversText = mode[nowTime] switch
        {
            1 => setItem[nowTime] ? "Ží‚ªA‚¦‚Ä‚ ‚é" : "‰½‚©A‚¦‚ê‚»‚¤‚¾",
            2 => "‚È‚ñ‚Æ‚©“o‚ê‚»‚¤‚¾",
            3 => "—¬Î‚É“o‚é‚±‚Æ‚Í‚Å‚«‚È‚¢",
            _ => "‚È‚ñ‚©ŠÔˆá‚Á‚Ä‚é"
        };

        acquirText = mode[nowTime] switch
        {
            1 => setItem[nowTime] ? "Ží‚ð‰ñŽû‚µ‚½" : "",
            2 => "",
            3 => "",
            _ => "‚È‚ñ‚©ŠÔˆá‚Á‚Ä‚é"
        };

        bool item = sm.ItemCheck(itemNo);
        actionText = mode[nowTime] switch
        {
            1 => item ? (setItem[nowTime] ? "" : "Ží‚ðA‚¦‚½" ) : "",
            2 => "‚È‚ñ‚Æ‚©“o‚é‚±‚Æ‚ª‚Å‚«‚½",
            3 => "",
            _ => ""
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangerSet();
    }

    public override int Acquir()
    {
        if (setItem[nowTime])
        {
            setItem[nowTime] = false;
            water[nowTime] = false;
            return itemNo;
        }
        return 0;
    }

    public override void Action(int i)
    {
        switch (mode[nowTime])
        {
            case 1:
                setItem[nowTime] = true;
                break;

            case 2:
                var p = FindAnyObjectByType<Player_Beye>().transform;
                p.position = pos.position;
                break;
        }
        ChangerSet();
    }
}
