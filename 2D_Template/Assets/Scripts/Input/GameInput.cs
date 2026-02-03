using System;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    private Vector2 _moveInput;
    private PlayerInputActions _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInputActions();
        _playerInput.Player.Enable();
        
        _playerInput.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled += ctx => _moveInput = Vector2.zero;

        _playerInput.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }
}
