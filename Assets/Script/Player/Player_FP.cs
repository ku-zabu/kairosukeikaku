using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_FP : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Rigidbody rb;
    [SerializeField] Player_Canvas canvas;

    //カメラの回転を制御するための変数
    Camera camera;
    Transform camTra;
    public float xRotation = 0f;                       //カメラの上下の回転量
    float lookSpeedX = 7, lookSpeedY = 7;     //カメラの回転速度
    bool lookMoveX = false, lookMoveY = false;  //リバース機能

    [SerializeField] bool artifactActive;
    bool inPossibleMove;
    bool openMenu;

    GameObject attentionObj;
    ItemTemp item;

    Player_FP myScript;

    [SerializeField] Camera subCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(playerInput == null) playerInput = GetComponent<PlayerInput>();
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (canvas == null) canvas = GameObject.Find("PlayerCanvas").GetComponent<Player_Canvas>();
        myScript = GetComponent<Player_FP>();

        camera = Camera.main;
        camTra = camera.transform;

        openMenu = false;

        if(subCamera ==null) subCamera = transform.Find("Main Camera").GetChild(0).GetComponent<Camera>();
        var texture = subCamera.targetTexture;
        texture.width = Screen.width; 
        texture.height = Screen.height;
        subCamera.enabled = false;

        GameObject dataBox;
        if ((dataBox = GameObject.Find("OptionDataBox")) != null) 
        {
            var data = dataBox.GetComponent<OptionDataSet>().GetData();
            SetOptionData(data);
        }

        GetArtifact(artifactActive);
    }

    /// <summary>
    /// カメラ、およびプレイヤーの回転を制御するメソッド
    /// </summary>
    /// <param name="value"></param>
    void OnLook(InputValue value)
    {
        if (camTra == null) return;
        if (inPossibleMove) return;
        Vector2 input = value.Get<Vector2>();
        var xRot = input.y * lookSpeedX * Time.deltaTime * (lookMoveX ? 1 : -1);
        var yRot = input.x * lookSpeedY * Time.deltaTime * (lookMoveY ? -1 : 1);

        xRotation += xRot;
        xRotation = Mathf.Clamp(xRotation, -55f, 55f);

        camTra.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * yRot);
    }

    void Update()
    {
        Move();
        Beam();
        
    }

    /// <summary>
    /// 自身の移動を制御するメソッド
    /// </summary>
    void Move()
    {
        if(inPossibleMove) return;
        var input = playerInput.actions["Move"].ReadValue<Vector2>();
        if(input == Vector2.zero) return;
        Vector3 moveForward = (transform.forward * input.y + transform.right * input.x);
        rb.linearVelocity = moveForward * (playerInput.actions["Sprint"].inProgress ? 4 : 2);
    }
    /// <summary>
    /// レイを飛ばすメソッド
    /// </summary>
    void Beam()
    {
        Vector3 center = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = camera.ScreenPointToRay(center);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2.5f))
        {
            if (attentionObj != hit.collider.gameObject)
            {
                attentionObj = hit.collider.gameObject;
                item = attentionObj.GetComponent<ItemTemp>();
            }
        }
        else
        {
            attentionObj = null;
            item = null;
        }
        if (item != null)
            canvas.WhatPossibleAction(true);
        else
            canvas.WhatPossibleAction(false);
    }

    /// <summary>
    /// 過去に飛ぶボタン
    /// </summary>
    /// <param name="inputValue"></param>
    void OnPast(InputValue inputValue)
    {
        if (!artifactActive) return;
        StartCoroutine(BootArtifact(false));
    }
    /// <summary>
    /// 未来に飛ぶボタン
    /// </summary>
    /// <param name="inputValue"></param>
    void OnFuture(InputValue inputValue)
    {
        if (!artifactActive) return;
        StartCoroutine(BootArtifact(true));
    }

    /// <summary>
    /// アーティファクトを起動
    /// </summary>
    IEnumerator BootArtifact(bool pf)
    {
        if (inPossibleMove) yield break;

        artifactActive = false;

        inPossibleMove = true;
        openMenu = false;
        yield return StartCoroutine(SetTexture());           //テクスチャーの風景を更新
        //------------------------オブジェクト表示切り替え処理--------------------
        yield return StartCoroutine(canvas.BootArtifact(pf));//移行時間
        inPossibleMove = false;
        openMenu = true;

        yield return StartCoroutine (canvas.InUseArtifact());//滞在時間

        if(inPossibleMove) OnJump(null);
        inPossibleMove = true;
        openMenu = false;
        yield return StartCoroutine(SetTexture());           //テクスチャーの風景を更新
        //------------------------オブジェクト表示切り替え処理--------------------
        yield return StartCoroutine(canvas.EndArtifact(pf)); //移行時間
        inPossibleMove = false;
        openMenu = true;
        yield return StartCoroutine(canvas.CoolArtifact());  //クールタイム
        artifactActive = true;
    }

    IEnumerator SetTexture()
    {
        subCamera.enabled = true;
        yield return null;
        subCamera.enabled = true;
    }

    /// <summary>
    /// メニューを開くボタン
    /// </summary>
    /// <param name="inputValue"></param>
    void OnJump(InputValue inputValue)
    {
        if (!openMenu) return;
            canvas.OpenMenu();
            inPossibleMove = !inPossibleMove;
            Cursor.lockState = inPossibleMove ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = inPossibleMove;
    }
    /// <summary>
    /// マウスクリックでインタラクトするボタン
    /// </summary>
    void OnInteract()
    {
        if (item == null || inPossibleMove) return;
        item.Interact(myScript);
        item = null;
    }

    public void StopCameraAndMove()
    {
        openMenu = false;
        inPossibleMove = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void GetArtifact(bool c)
    {
        if (c)
        {
            canvas.GetArtifact();
            artifactActive = true;
        }
        openMenu = true;
        Cursor.lockState = CursorLockMode.Locked;
        inPossibleMove = false;
    }

    public void SetItems(GameObject icon, GameObject text)
    {
        canvas.SetItems(icon, text);
    }

    public void SetOptionData(CameraOption data)
    {
        lookSpeedX = data.SpeedX;
        lookSpeedY = data.SpeedY;
        lookMoveX = data.MoveX;
        lookMoveY = data.MoveY;
    }
}
