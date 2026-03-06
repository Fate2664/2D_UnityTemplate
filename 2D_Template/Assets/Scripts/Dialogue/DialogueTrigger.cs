using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
   public DialogueBase Dialogue;


   private void OnTriggerEnter2D(Collider2D other)
   {
      if (!other.CompareTag("Player") /* || HasStartedDialogue*/) return;
      //HasStartedDialogue = true;
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (!other.CompareTag("Player")) return;
      
      //HasStartedDialogue = true;
   }
}
