using UnityEngine;
using TMPro;

public class SnakeDialogueManager : MonoBehaviour
{
    public PnjInfo pnjInfo;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    public PowerScriptableObject grantedPower;
    private bool hasGivenPower = false;

    private bool isPlayerInRange = false;
    private int dialogueIndex = 0;

    public BirdController birdController;

    void Start()
    {
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.M))
        {
            ShowDialogue();
        }
    }

    private void ShowDialogue()
    {
        if (!hasGivenPower)
        {
            if (dialogueIndex < pnjInfo.initialDialogue.Count)
            {
                dialogueText.text = pnjInfo.initialDialogue[dialogueIndex];
                dialogueBox.SetActive(true);
                dialogueIndex++;
            }
            else
            {
                GivePowerToPlayer();
                hasGivenPower = true;

                if (pnjInfo.rewardDialogue.Count > 0)
                {
                    dialogueText.text = pnjInfo.rewardDialogue[0];
                    dialogueBox.SetActive(true);
                }
                if (pnjInfo.magicPowerGrantedDialogue.Count > 0)
                {
                    dialogueText.text = pnjInfo.magicPowerGrantedDialogue[0];
                    dialogueBox.SetActive(true);
                }
            }
        }
        else
        {
            if (pnjInfo.lackOfShellsDialogue.Count > 0)
            {
                dialogueText.text = pnjInfo.lackOfShellsDialogue[0];
                dialogueBox.SetActive(true);
            }
        }
    }

    private void GivePowerToPlayer()
    {
        PlayerPowerController playerPowerController = FindObjectOfType<PlayerPowerController>();

        if (playerPowerController != null && grantedPower != null)
        {
            playerPowerController.GrantSpeedBoost(grantedPower); // Accorder le boost de vitesse au joueur
            Debug.Log($"Speed boost granted: {grantedPower.powerName}");
            birdController.missionsFinished += 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueBox.SetActive(false);
        }
    }
}
