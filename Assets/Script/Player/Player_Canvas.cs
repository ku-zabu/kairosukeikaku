using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Canvas : MonoBehaviour
{
    private Animator animator;
    private bool openMenu;

    private GameObject actionTxt;
    [SerializeField] private Animator artifactAnime;

    [SerializeField] private float settingTime;
    [SerializeField] private float stayingTime;
    [SerializeField] private float coolTime;

    private Image limit;
    private Image current;
    private Image past;
    private Image cool;

    [SerializeField] Material mat;

    [SerializeField] PC_Menu menu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform artifact = transform.Find("Field/ArtifactShaft/Artifact");
        if (animator == null) animator = GetComponent<Animator>();
        if (artifactAnime == null) artifactAnime = artifact.GetComponent<Animator>();
        actionTxt = transform.Find("Field/CenterImage/Image").gameObject;
        if (actionTxt == null) Debug.LogError("ACTION!!の場所が取得できていません");
        actionTxt.SetActive(false);


        limit = artifact.Find("Edge/LimitGauge").GetComponent<Image>();
        limit.fillAmount = 0;

        var future = artifact.Find("Subject/FutureFragment");
        current = future.GetChild(0).GetComponent<Image>();
        current.fillAmount = 1;
        past = future.GetChild(0).GetChild(0).GetComponent<Image>();
        past.fillAmount = 0;
        cool =artifact.GetChild(2).GetComponent<Image>();
        cool.fillAmount = 0;

        mat.SetFloat("_AlphaValue", 0);

        if(menu == null) menu = transform.Find("Menu").GetComponent<PC_Menu>();
    }

    public void OpenMenu()
    {
        openMenu = !openMenu;
        animator.SetBool("OpenMenu", openMenu);
    }

    public void WhatPossibleAction(bool active)
    {
        actionTxt.SetActive(active);
    }

    public void GetArtifact()
    {
        artifactAnime.SetBool("Get", true);
    }
    /// <summary>
    /// アーティファクト起動の関数たち
    /// </summary>
    /// <param name="pf"></param>
    /// <returns></returns>
    public IEnumerator BootArtifact(bool pf)
    {
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
    }
    public IEnumerator InUseArtifact()
    {
        float useTime = 0;
        while (useTime < stayingTime)
        {
            useTime += Time.deltaTime;
            var value = Mathf.Lerp(1.0f, 0.0f, useTime / stayingTime);
            limit.fillAmount = value;
            yield return null;
        }
        limit.fillAmount = 0.0f;
    }
    public IEnumerator EndArtifact(bool pf)
    {
        float setTime = 0;
        mat.SetFloat("_AlphaValue", 1);
        while (setTime < settingTime)
        {
            setTime += Time.deltaTime;
            var value = Mathf.Lerp(0.0f, 1.0f, setTime / settingTime);
            mat.SetFloat("_AlphaValue", 1 - value);
            cool.fillAmount = value;
            if (pf)
                current.fillAmount = value;
            else
                past.fillAmount = 1 - value;
            yield return null;
        }
        cool.fillAmount = 1.0f;
    }
    public IEnumerator CoolArtifact()
    {
        float time = 0;
        while(time < coolTime)
        {
            time += Time.deltaTime;
            var value =Mathf.Lerp(1.0f,0.0f, time / coolTime);
            cool.fillAmount = value;
            yield return null;
        }
        cool.fillAmount = 0.0f;
    }
    //--------------------------------------

    public void SetItems(GameObject icon, GameObject text)
    {
        menu.AddItems(icon, text);
    }
}
