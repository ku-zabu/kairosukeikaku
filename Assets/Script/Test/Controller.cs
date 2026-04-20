using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Controller : MonoBehaviour
{
    [SerializeField] private InputActionAsset asset;
    private InputActionTrace _trace;
    private InputAction Q;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _trace = new InputActionTrace();
        _trace.SubscribeTo(asset.FindActionMap("Default").FindAction("Action", true));
        Q= GetComponent<InputAction>();

        InputSystem.pollingFrequency = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Q != null)
        {

        }
    }
}
