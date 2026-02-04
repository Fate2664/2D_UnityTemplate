using System;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    public event EventHandler OnPrimaryAttack;
    private Vector2 _moveInput;
    private PlayerInputActions _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInputActions();
        _playerInput.Player.Enable();
        
        _playerInput.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        _playerInput.Player.PrimaryAttack.performed += PrimaryAttack_performed;

        _playerInput.Player.Interact.performed += Interact_performed;
    }

    private void PrimaryAttack_performed(InputAction.CallbackContext context)
    {
        OnPrimaryAttack?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

	public Vector2 GetMovementVector()
	{
		return _moveInput.normalized; 
	}
}
