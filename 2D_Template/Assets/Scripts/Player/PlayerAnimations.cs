using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";
    private const string IS_JUMPING = "isJumping";
    private const string ATTACK1 = "Attack1";
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat  playerCombat;
    
    private Animator animator;

    private void Awake()
    {
        animator =  GetComponent<Animator>();
        playerCombat.OnPrimaryAttack += PlayPrimaryAttack;
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, playerMovement.IsWalking());
        animator.SetBool(IS_JUMPING, playerMovement.IsJumping());
      
    }

    private void PlayPrimaryAttack(object sender, EventArgs e)
    {
        animator.SetTrigger(ATTACK1);
    }
}
