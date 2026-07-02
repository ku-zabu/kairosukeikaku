using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource seSource;
    float2 values = new float2(0.5f, 0.5f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        bgmSource.volume = values.x;
        seSource.volume = values.y;
    }

    public float2 GetValues()
    {
        return new float2(bgmSource.volume, seSource.volume);
    }

    public void Confirmation(float bgmValue, float seValue, bool se)
    {
        bgmSource.volume = bgmValue * 0.001f;
        if (se)
        {
            seSource.volume = seValue * 0.001f;
            seSource.Play();
            Debug.Log(GetValues());
        }
    }

    public void SetValues(bool set, float bgmValue = 0, float seValue = 0)
    {
        if(set)
        {
            bgmSource.volume = bgmValue * 0.1f;
            seSource.volume = seValue * 0.1f;
        } else
        {
            bgmSource.volume = values.x;
            seSource.volume = values.y;
        }
    }
}
