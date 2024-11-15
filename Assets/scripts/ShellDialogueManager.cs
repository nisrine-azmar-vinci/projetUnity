using UnityEngine;
using TMPro;

public class ShellDialogueManager : MonoBehaviour
{
    public PnjInfo pnjInfo;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public int requiredShells = 5;
    public GameObject crownObject;
    private Inventory playerInventory;
    private int dialogueIndex = 0;
    private bool isRewardDialogue = false;
    private bool isBaseDialogueCompleted = false;
    private bool isPlayerInRange = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        playerInventory = FindObjectOfType<Inventory>();

        crownObject.SetActive(false);
        if (pnjInfo != null && pnjInfo.initialDialogue.Count > 0)
        {
            ShowBaseDialogue();
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.M))
        {
            ShowNextDialogueLine();
        }
    }

    public void ShowNextDialogueLine()
    {
        if (!isBaseDialogueCompleted)
        {
            // Dialogue de base
            if (dialogueIndex < pnjInfo.initialDialogue.Count)
            {
                dialogueText.text = pnjInfo.initialDialogue[dialogueIndex];
                dialogueIndex++;
            }
            else
            {
                isBaseDialogueCompleted = true;
                dialogueIndex = 0;
                CheckForShellCount();
            }
        }
        else if (isBaseDialogueCompleted && !isRewardDialogue)
        {
            // Dialogue de manque de coquillages
            if (playerInventory.shellCount < requiredShells)
            {
                if (dialogueIndex < pnjInfo.lackOfShellsDialogue.Count)
                {
                    dialogueText.text = pnjInfo.lackOfShellsDialogue[dialogueIndex];
                    dialogueIndex++;
                }
                else
                {
                    dialogueBox.SetActive(false);
                    dialogueIndex = 0; // Réinitialise pour le prochain dialogue
                }
            }
            else
            {
                ShowRewardDialogue(); // Passe au dialogue de récompense si le joueur a assez de coquillages
            }
        }
        else if (isRewardDialogue)
        {
            // Dialogue de récompense
            if (dialogueIndex < pnjInfo.rewardDialogue.Count)
            {
                dialogueText.text = pnjInfo.rewardDialogue[dialogueIndex];
                dialogueIndex++;
            }
            else
            {
                dialogueBox.SetActive(false);

                // Place et active la couronne au-dessus de la tête du joueur
                Vector3 crownPosition = playerInventory.transform.position + new Vector3(0, 0.8f, 0); // Ajuste la hauteur
                crownObject.transform.position = crownPosition;
                crownObject.SetActive(true); // Active la couronne existante
                isRewardDialogue = false;  // Réinitialise l'état de récompense pour éviter de répéter le dialogue
            }
        }
    }

    private void ShowBaseDialogue()
    {
        dialogueIndex = 0;
        dialogueText.text = pnjInfo.initialDialogue[dialogueIndex];
        dialogueBox.SetActive(true);
        dialogueIndex++;
    }

    private void CheckForShellCount()
    {
        if (playerInventory.shellCount >= requiredShells)
        {
            ShowRewardDialogue();
        }
        else
        {
            ShowLackOfShellsDialogue();
        }
    }

    private void ShowRewardDialogue()
    {
        isRewardDialogue = true;
        dialogueIndex = 0;
        dialogueText.text = pnjInfo.rewardDialogue[dialogueIndex];
        dialogueBox.SetActive(true);
        dialogueIndex++;

        // Réinitialise le nombre de coquillages pour éviter que la condition soit relancée
        playerInventory.shellCount = 0;
        playerInventory.UpdateUI();
    }

    private void ShowLackOfShellsDialogue()
    {
        dialogueIndex = 0;
        dialogueText.text = pnjInfo.lackOfShellsDialogue[dialogueIndex];
        dialogueBox.SetActive(true);
        dialogueIndex++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueBox.SetActive(true); // Active la boîte de dialogue dès que le joueur entre
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueBox.SetActive(false);
            dialogueIndex = 0; // Réinitialise pour le prochain dialogue
        }
    }
}
