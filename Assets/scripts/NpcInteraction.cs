using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    private MonoBehaviour dialogueManager;  // Utilise une r�f�rence g�n�rique

    void Start()
    {
        dialogueManager = GetComponent<MonoBehaviour>(); // Assigne automatiquement le script de dialogue
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogueManager is ShellDialogueManager shellManager)
            {
                shellManager.ShowNextDialogueLine();
            }
            else if (dialogueManager is GardenerDialogueManager gardenerManager)
            {
                gardenerManager.ShowNextDialogueLine();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogueManager is ShellDialogueManager shellManager)
            {
                shellManager.dialogueBox.SetActive(false);
            }
            else if (dialogueManager is GardenerDialogueManager gardenerManager)
            {
                gardenerManager.dialogueBox.SetActive(false);
            }
        }
    }
}
