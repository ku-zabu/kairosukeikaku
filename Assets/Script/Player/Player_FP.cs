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

    bool artifactActive;
    bool checkMenu;
    bool miniGame;

    GameObject attentionObj;
    ItemTemp item;

    Player_FP myScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(playerInput == null) playerInput = GetComponent<PlayerInput>();
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (canvas == null) canvas = GameObject.Find("PlayerCanvas").GetComponent<Player_Canvas>();
        myScript = GetComponent<Player_FP>();

        camera = Camera.main;
        camTra = camera.transform;

        Cursor.lockState = CursorLockMode.Locked;

        artifactActive = false;
        miniGame = true;

        checkMenu= false; ;
    }

    /// <summary>
    /// カメラ、およびプレイヤーの回転を制御するメソッド
    /// </summary>
    /// <param name="value"></param>
    void OnLook(InputValue value)
    {
        if (checkMenu) return;
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
        if(checkMenu) return;
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
        if (Physics.Raycast(ray, out hit, 5f))
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
            canvas.PossibleAction();
        else
            canvas.InPossibleAction();
    }

    /// <summary>
    /// 過去に飛ぶボタン
    /// </summary>
    /// <param name="inputValue"></param>
    void OnPast(InputValue inputValue)
    {
        if (!artifactActive) return;
        Debug.Log("過去に移動");
    }
    /// <summary>
    /// 未来に飛ぶボタン
    /// </summary>
    /// <param name="inputValue"></param>
    void OnFuture(InputValue inputValue)
    {
        if (!artifactActive) return;
        Debug.Log("未来に移動");
    }
    /// <summary>
    /// メニューを開くボタン
    /// </summary>
    /// <param name="inputValue"></param>
    void OnJump(InputValue inputValue)
    {
        if (miniGame)
        {
            canvas.OpenMenu();
            checkMenu = !checkMenu;
            Cursor.lockState = checkMenu ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = checkMenu;
        }
        else
        {

        }
    }
    /// <summary>
    /// マウスクリックでインタラクトするボタン
    /// </summary>
    void OnInteract()
    {
        if (item == null) return;
        item.Interact(myScript);
        item = null;
    }

    public void GetArtifact()
    {
        artifactActive = true;
        canvas.GetArtifact();
    }
}
