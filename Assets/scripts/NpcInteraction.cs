using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    private DialogueManager dialogueManager;

    void Start()
    {
        // Trouver le DialogueManager dans la sc�ne
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Active le DialogueManager lorsque le joueur entre en collision
            dialogueManager.dialogueBox.SetActive(true);
            dialogueManager.dialogueText.text = "Je peux t'offrir un cadeau si tu me trouves 5 coquillages !";
            dialogueManager.UpdateDialoguePosition(); // Met � jour la position de la bo�te de dialogue
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // D�sactive la bo�te de dialogue lorsque le joueur s'�loigne
            dialogueManager.dialogueBox.SetActive(false);
        }
    }
}
