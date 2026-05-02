using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Player_Canvas : MonoBehaviour
{
    private Animator animator;
    private bool openMenu;

    private GameObject actionTxt;
    [SerializeField] private Animator artifactAnime;

    [SerializeField] private float stayingTime;
    [SerializeField] private float coolTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (artifactAnime == null) artifactAnime = transform.Find("Field/ArtifactBox/Artifact").GetComponent<Animator>();
        actionTxt = transform.Find("Field/CenterImage/Image").gameObject;
        if (actionTxt == null) Debug.LogError("ACTION!!ÇÃèÍèäÇ™éÊìæÇ≈Ç´ÇƒÇ¢Ç‹ÇπÇÒ");
        actionTxt.SetActive(false);
    }

    public void OpenMenu()
    {
        openMenu = !openMenu;
        animator.SetBool("OpenMenu", openMenu);
    }

    public void PossibleAction()
    {
        actionTxt.SetActive(true);
    }

    public void InPossibleAction()
    {
        actionTxt.SetActive(false);
    }

    public void GetArtifact()
    {
        artifactAnime.SetBool("Get",true);
    }
}
