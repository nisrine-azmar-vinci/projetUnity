using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Unity.Services.Analytics.Internal;

public class GardenerDialogueManager : MonoBehaviour
{
    public PnjInfo pnjInfo;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public int requiredPlants = 3;
    private Inventory playerInventory;
    private int dialogueIndex = 0;
    private bool isRewardDialogue = false;
    private bool isBaseDialogueCompleted = false;
    private bool isPlayerInRange = false;

    public GameObject companionPrefab; // Le prefab du compagnon
    private GameObject companionInstance;

    void Start()
    {
        dialogueBox.SetActive(false);
        playerInventory = FindObjectOfType<Inventory>();

        if (companionPrefab != null)
        {
            companionPrefab.SetActive(false);
        }
        if (pnjInfo != null && pnjInfo.initialDialogue.Count > 0)
        {
            ShowBaseDialogue();
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("M key pressed and player is in range."); // Log supplémentaire
            ShowNextDialogueLine();
        }

       
    }

    public void ShowNextDialogueLine()
    {
        Debug.Log("ShowNextDialogueLine called. dialogueIndex: " + dialogueIndex);

        if (!isBaseDialogueCompleted)
        {
            // Dialogue de base
            if (dialogueIndex < pnjInfo.initialDialogue.Count)
            {
                dialogueText.text = pnjInfo.initialDialogue[dialogueIndex];
                Debug.Log("Showing base dialogue: " + pnjInfo.initialDialogue[dialogueIndex]); // Log supplémentaire
                dialogueIndex++;
            }
            else
            {
                isBaseDialogueCompleted = true;
                dialogueIndex = 0;
                CheckForPlantCount();
            }
        }
        else if (isBaseDialogueCompleted && !isRewardDialogue)
        {
            // Dialogue de manque de plantes
            if (playerInventory.plantCount < requiredPlants)
            {
                if (dialogueIndex < pnjInfo.lackOfPlantsDialogue.Count)
                {
                    dialogueText.text = pnjInfo.lackOfPlantsDialogue[dialogueIndex];
                    Debug.Log("Showing lack of plants dialogue: " + pnjInfo.lackOfPlantsDialogue[dialogueIndex]); // Log supplémentaire
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
                ShowRewardDialogue(); // Passe au dialogue de récompense si le joueur a assez de plantes
            }
        }
        else if (isRewardDialogue)
        {
            // Dialogue de récompense
            if (dialogueIndex < pnjInfo.rewardDialogue.Count)
            {
                dialogueText.text = pnjInfo.rewardDialogue[dialogueIndex];
                Debug.Log("Showing reward dialogue: " + pnjInfo.rewardDialogue[dialogueIndex]); // Log supplémentaire
                dialogueIndex++;
            }
            else
            {
                dialogueBox.SetActive(false);
                isRewardDialogue = false;  // Réinitialise l'état de récompense pour éviter de répéter le dialogue
                SpawnCompanion();
            }
        }
    }

    private void SpawnCompanion()
    {
        if (companionPrefab != null && companionInstance == null)
        {
            // Instancier le compagnon si ce n'est pas déjà fait
            companionInstance = Instantiate(companionPrefab, playerInventory.transform.position + Vector3.right * 2, Quaternion.identity);

            // Positionne le compagnon sur le NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(companionInstance.transform.position, out hit, 5f, NavMesh.AllAreas))
            {
                companionInstance.transform.position = hit.position; // Ajuste la position au NavMesh
            }
            else
            {
                Debug.LogError("Le compagnon ne peut pas être placé sur le NavMesh. Vérifie la position initiale.");
            }

            // Active le compagnon
            companionInstance.SetActive(true);

            // Configure le suivi du joueur
            CompanionController companionController = companionInstance.GetComponent<CompanionController>();
            if (companionController != null)
            {
                companionController.playerTransform = playerInventory.transform;
            }
        }
        else if (companionInstance != null)
        {
            // Si le compagnon existe déjà, on l'active
            companionInstance.SetActive(true);

            // Configure le suivi du joueur
            CompanionController companionController = companionInstance.GetComponent<CompanionController>();
            if (companionController != null)
            {
                companionController.playerTransform = playerInventory.transform;
            }
        }
    }



    private void ShowBaseDialogue()
    {
        dialogueIndex = 0;
        dialogueText.text = pnjInfo.initialDialogue[dialogueIndex];
        dialogueBox.SetActive(true);
        dialogueIndex++;
        Debug.Log("Base dialogue started.");
    }

    private void CheckForPlantCount()
    {
        Debug.Log("Checking for plant count: " + playerInventory.plantCount);
        if (playerInventory.plantCount >= requiredPlants)
        {
            ShowRewardDialogue();
        }
        else
        {
            ShowLackOfPlantsDialogue();
        }
    }

    private void ShowRewardDialogue()
    {
        isRewardDialogue = true;
        dialogueIndex = 0;
        dialogueText.text = pnjInfo.rewardDialogue[dialogueIndex];
        dialogueBox.SetActive(true);
        dialogueIndex++;
        Debug.Log("Reward dialogue started.");

        playerInventory.plantCount = 0;  // Remet le compteur de plantes à zéro pour éviter que la condition soit relancée
        playerInventory.UpdateUI();
    }

    private void ShowLackOfPlantsDialogue()
    {
        dialogueIndex = 0;
        dialogueText.text = pnjInfo.lackOfPlantsDialogue[dialogueIndex];
        dialogueBox.SetActive(true);
        dialogueIndex++;
        Debug.Log("Lack of plants dialogue started.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueBox.SetActive(true);
            Debug.Log("Player entered dialogue range.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueBox.SetActive(false);
            dialogueIndex = 0;
            Debug.Log("Player exited dialogue range.");
        }
    }
}
