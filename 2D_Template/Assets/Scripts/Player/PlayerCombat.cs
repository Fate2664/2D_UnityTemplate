using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
   [Header("Combat Settings")]
   
   [Space(10)]
   [Header("Connections")]
   [SerializeField] private GameInput gameInput;

   public event EventHandler OnPrimaryAttack;

   
   private void Awake()
   {
      gameInput.OnPrimaryAttack += GameInput_OnPrimaryAttack;
   }

  

   private void GameInput_OnPrimaryAttack(object sender, EventArgs e)
   {
      OnPrimaryAttack?.Invoke(this, EventArgs.Empty);
   }

   
}
