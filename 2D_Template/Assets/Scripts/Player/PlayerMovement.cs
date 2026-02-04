using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Space(10)]
    [Range(1f, 10f)]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    
    [Space(10)]
    [Header("Connections")]
    [SerializeField] private GameInput gameInput;
    
    private bool isWalking = false;
    private bool isGrounded;
    private bool isJumping = false;
    
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();      
    }

    void Start()
    {
    }
    
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        isJumping = !isGrounded;
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2 (inputVector.x * moveSpeed, rb.linearVelocity.y);
            isWalking = inputVector != Vector2.zero;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsJumping()
    {
        return isJumping;
    }
}