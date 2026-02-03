using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Space(10)]
    [Range(1f, 10f)]
    [SerializeField] private float moveSpeed;    
    
    [Space(10)]
    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactionLayer;
    
    [Space(10)]
    [Header("Connections")]
    [SerializeField] private GameInput gameInput;
    
    private bool isRunning = false;
    
    void Start()
    {
    }

    void Update()
    {
    }
}