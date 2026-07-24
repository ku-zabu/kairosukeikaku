
public class SeedBox : ItemBox
{
    public override void ActiveChanger(int i)
    {
        bool a = false;
        switch (i)
        {
            case 0:
                break;
            case 1:
                a = true;
                break;
            case 2:
                break;
        }
        gameObject.SetActive(a);
    }
}
