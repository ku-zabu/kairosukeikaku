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
    Transform pos;
    GameObject seed;

    private void Awake()
    {
        sm = FindAnyObjectByType<StageManager>();
        mode = new int[3];
        for (int i = 0; i < mode.Length; i++) mode[i] = 1;
        nowTime = 1;
        pos = transform.Find("First/Terepo").transform;
        seed = transform.Find("Seed").gameObject;
        seed.SetActive(false);
    }

    bool Check()
    {
        if (setItem[0])
        {
            return water[0] || water[1];
        }
        else if(setItem[1])
        {
            return water[1];
        }
        return false;
    }

    /// <summary>
    /// ŽžŠÔ‚É‰ž‚¶‚Ä•Ï‰»
    /// </summary>
    /// <param name="i"></param>
    public void ChangeMode(int i)
    {
        if (setItem[i])
        {
            seed.SetActive(true);
        }
        else
        {
            seed.SetActive(false);
        }

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
                else if(Check())
                    mode[2] = 2;
                else
                    mode[2] = mode[1];

                break;
        }
        nowTime = i;
        ChangerSet();
    }

    /// <summary>
    /// Mode‚Å‚É‰ž‚¶‚Ä•Ï‰»
    /// </summary>
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
            for (int w = 0; w < water.Length; w++) water[w] = false;
            for (int m = 0; m < mode.Length; m++) mode[m] = 1;
            seed.SetActive(false);
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
                seed.SetActive(true);
                break;

            case 2:
                var p = FindAnyObjectByType<Player_Beye>().transform;
                p.position = pos.position;
                break;
        }
        ChangerSet();
    }
}
