using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Harvest : MonoBehaviour
{
    [SerializeField] private InputActionReference _LeftStickAction;
    [SerializeField] private InputActionReference _RightStickAction;
    [SerializeField] private InputActionReference _LeftTriggerAction;
    [SerializeField] private InputActionReference _RightTriggerAction;


    // Start is called before the first frame update
    void Start()
    {
        // ボタンの押下と解放に対するイベントを登録する
        _LeftStickAction.action.performed += OnLeftStickPressed;
        _RightStickAction.action.performed += OnRightStickPressed;
        _LeftTriggerAction.action.performed += OnLeftTriggerPressed;
        _RightTriggerAction.action.performed += OnRightTriggerPressed;
        _LeftStickAction.action.Enable();
        _RightStickAction.action.Enable();
        _LeftTriggerAction.action.Enable();
        _RightTriggerAction.action.Enable();
    }

    private void OnLeftStickPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OnLeftStickPressed");
        }
    }

    private void OnRightStickPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OnRightStickPressed");
        }
    }

    private void OnRightTriggerPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OnRightTriggerPressed");
        }
    }

    private void OnLeftTriggerPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OnLeftTriggerPressed");
        }
    }
}