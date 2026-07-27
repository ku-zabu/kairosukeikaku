using Cysharp.Threading.Tasks;
using UnityEngine;

public class Fade : MonoBehaviour
{
    GameObject subCamera;
    [SerializeField] Material mat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        subCamera = GameObject.Find("SubCamera").gameObject;
        mat.SetFloat("_AlphaValue", 0.0f);
    }

    public async UniTask FadeStart(float waitTime)
    {
        mat.SetFloat("_AlphaValue", 1.0f);
        await UniTask.Yield();
        subCamera.SetActive(false);
        float setTime = 0;
        while (setTime < waitTime)
        {
            setTime += Time.deltaTime;
            var value = Mathf.Lerp(0.0f, 1.0f, setTime / waitTime);
            mat.SetFloat("_AlphaValue", 1 - value);
            await UniTask.Yield();
        }
    }
}
/*
 
        float setTime = 0;
        mat.SetFloat("_AlphaValue", 1);
        while (setTime < settingTime)
        {
            setTime += Time.deltaTime;
            var value = Mathf.Lerp(0.0f, 1.0f, setTime / settingTime);
            mat.SetFloat("_AlphaValue", 1 - value);
            if (pf)
                current.fillAmount = 1 - value;
            else
                past.fillAmount = value;
            limit.fillAmount = value;
            yield return null;
        }
 
 */