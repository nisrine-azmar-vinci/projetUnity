using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    private DialogueManager dialogueManager;

    void Start()
    {
        // Trouver le DialogueManager dans la scène
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Active le DialogueManager lorsque le joueur entre en collision
            dialogueManager.dialogueBox.SetActive(true);
            dialogueManager.dialogueText.text = "Je peux t'offrir un cadeau si tu me trouves 5 coquillages !";
            dialogueManager.UpdateDialoguePosition(); // Met à jour la position de la boîte de dialogue
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Désactive la boîte de dialogue lorsque le joueur s'éloigne
            dialogueManager.dialogueBox.SetActive(false);
        }
    }
}
