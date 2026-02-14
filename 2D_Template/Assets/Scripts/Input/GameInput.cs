using System;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    //Player Actions
    public event EventHandler OnInteract;
    public event EventHandler OnPrimaryAttack;
    
    //UI Actions
    public event EventHandler OnApply;
    public event EventHandler OnRestoreDefaults;
    public event EventHandler OnExit;
        
    private Vector2 _moveInput;
    private PlayerInputActions _playerInput;
    private float _verticalNav;
    
    
    private void Awake()
    {
        _playerInput = new PlayerInputActions();
        _playerInput.Player.Enable();
        
        //Player Actions
        _playerInput.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        _playerInput.Player.PrimaryAttack.performed += PrimaryAttack_performed;
        _playerInput.Player.Interact.performed += Interact_performed;
        
        //UI Actions
        _playerInput.UI.Apply.performed += Apply_performed;
        _playerInput.UI.Exit.performed += Exit_performed;
        _playerInput.UI.RestoreDefaults.performed += RestoreDefaults_performed;
        _playerInput.UI.VerticalNavigation.performed += ctx => _verticalNav = ctx.ReadValue<float>();
        _playerInput.UI.VerticalNavigation.canceled += ctx => _verticalNav = 0;
    }

    private void RestoreDefaults_performed(InputAction.CallbackContext obj)
    {
        OnRestoreDefaults.Invoke(this, EventArgs.Empty);
    }

    private void Exit_performed(InputAction.CallbackContext obj)
    {
        OnExit.Invoke(this, EventArgs.Empty);
    }

    private void Apply_performed(InputAction.CallbackContext obj)
    {
        OnApply.Invoke(this, EventArgs.Empty);
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

    public float GetVerticalNav()
    {
        return _verticalNav;
    }
}
