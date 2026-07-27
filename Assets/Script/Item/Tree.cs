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

    /// <summary>
    /// éûä‘Ç…âûÇ∂Çƒïœâª
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
                else if(!setItem[0] && !water[0] && setItem[1] && water[1])//Ç±Ç±ÇèCê≥
                    mode[2] = 2;
                else
                    mode[2] = mode[1];

                break;
        }
        nowTime = i;
        ChangerSet();
    }

    /// <summary>
    /// ModeÇ≈Ç…âûÇ∂Çƒïœâª
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
            1 => setItem[nowTime] ? "éÌÇ™êAÇ¶ÇƒÇ†ÇÈ" : "âΩÇ©êAÇ¶ÇÍÇªÇ§Çæ",
            2 => "Ç»ÇÒÇ∆Ç©ìoÇÍÇªÇ§Çæ",
            3 => "ó¨êŒÇ…ìoÇÈÇ±Ç∆ÇÕÇ≈Ç´Ç»Ç¢",
            _ => "Ç»ÇÒÇ©ä‘à·Ç¡ÇƒÇÈ"
        };

        acquirText = mode[nowTime] switch
        {
            1 => setItem[nowTime] ? "éÌÇâÒé˚ÇµÇΩ" : "",
            2 => "",
            3 => "",
            _ => "Ç»ÇÒÇ©ä‘à·Ç¡ÇƒÇÈ"
        };

        bool item = sm.ItemCheck(itemNo);
        actionText = mode[nowTime] switch
        {
            1 => item ? (setItem[nowTime] ? "" : "éÌÇêAÇ¶ÇΩ" ) : "",
            2 => "Ç»ÇÒÇ∆Ç©ìoÇÈÇ±Ç∆Ç™Ç≈Ç´ÇΩ",
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
            for (int j = 0; j < water.Length; j++) water[j] = false;
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
