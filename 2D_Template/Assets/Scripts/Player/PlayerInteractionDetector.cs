using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractionDetector : MonoBehaviour
{
   [SerializeField] private GameInput gameInput;
   private PlayerManager player;
   private IInteractable currentTarget;
   
   private void Awake()
   {
      player = player.GetComponent<PlayerManager>();
      gameInput.Interact += OnInteract;
      gameInput.EnableActions();
   }


   private void OnTriggerEnter2D(Collider other)
   {
      if (currentTarget == null) return;
      if (!other.TryGetComponent(out MonoBehaviour mb)) return;
      if (mb is not IInteractable interactable) return;
      
      currentTarget = interactable;
      //Show indicator
   }

   private void OnTriggerExit2D(Collider other)
   {
      if (currentTarget == null) return;
      if (!other.TryGetComponent(out MonoBehaviour mb)) return;
      if (mb is not IInteractable interactable) return;
      
      currentTarget = null;
      //Hide indicator
   }

   private void OnInteract(bool pressed)
   {
      currentTarget.Interact(player);
   }
   
   private void OnDisable()
   {
      gameInput.Interact -= OnInteract;
   }
}
