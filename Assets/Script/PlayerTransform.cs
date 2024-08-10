using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerTransform : MonoBehaviour
{
    [SerializeField] private InputActionReference _MoveAction;
    [SerializeField] private InputActionReference _RotationAction;
    [SerializeField] private float _MoveSpeed ;
    [SerializeField] private float _RotationSpeed ;
    [SerializeField] private Text _MoveSpeedText;
    [SerializeField] private Text _RotationSpeedText;

    private float deltaYaw = 0.0f;
    private Vector3 _moveDirection = Vector3.zero;
    private Camera _MainCamera;

    
    
    void Start()
    {
        _MainCamera = Camera.main;
        _MoveAction.action.performed += OnMove;
        _MoveAction.action.canceled += OnMove;
        _RotationAction.action.performed += OnRotation;
        _RotationAction.action.canceled += OnRotation;
        OffAction();
    }

    private void Update()
    {
        RoteUpdate();
        PosUpdate();
        // PCdebag用コード
        TextUpdata();
    }

    private void TextUpdata()
    {
        _MoveSpeedText.text = _MoveSpeed.ToString();
        _RotationSpeedText.text = _RotationSpeed.ToString();
    }
    
    // 左スティックの入力による移動処理
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();

        if (move == Vector2.zero)
        {
            _moveDirection = Vector3.zero;
        }
        else
        {
            // カメラの前方ベクトルと右方向ベクトルを取得
            Vector3 cameraForward = _MainCamera.transform.forward;
            Vector3 cameraRight = _MainCamera.transform.right;

            // y成分を無視して水平面に投影
            cameraForward.y = 0;
            cameraRight.y = 0;

            // 正規化
            cameraForward.Normalize();
            cameraRight.Normalize();

            // 入力された方向をカメラの向きに基づいて変換
            _moveDirection = cameraForward * move.y + cameraRight * move.x;
        }
    }

    // 右スティックの入力による回転処理
    private void OnRotation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 rotation = context.ReadValue<Vector2>();
            if (rotation.x < -0.5f)
            {
                Debug.Log("左");
                deltaYaw = -1.0f * _RotationSpeed;
            }
            else if (rotation.x > 0.5f)
            {
                Debug.Log("右");
                deltaYaw = 1.0f * _RotationSpeed;
            }
            else
            {
                deltaYaw = 0.0f;
            }
        }

        if (context.canceled)
        {
            Debug.Log("キャンセル");
            deltaYaw = 0.0f;
        }
    }

    private void RoteUpdate()
    {
        transform.eulerAngles += new Vector3(0, deltaYaw, 0);
    }

    private void PosUpdate()
    {
        transform.position += _moveDirection * _MoveSpeed * Time.deltaTime;
    }
    
    public void OnAction()
    {
        _MoveAction.action.Enable();
        _RotationAction.action.Enable();
    }

    public void OffAction()
    {
        _MoveAction.action.Disable();
        _RotationAction.action.Disable();
    }
}