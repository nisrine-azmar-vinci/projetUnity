using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;  // Référence à la boîte de dialogue (Panel UI)
    public TextMeshProUGUI dialogueText;  // Référence au texte de la boîte de dialogue
    public Transform npcTransform;  // Position du NPC pour placer la boîte de dialogue au-dessus
    public string dialogueMessage = "Hello! Can I help you?";  // Message à afficher
    public float dialogueOffset = 2f;  // Décalage de la boîte de dialogue par rapport au NPC

    private bool isDialogueVisible = false;  // Indique si la boîte de dialogue est affichée

    void Start()
    {
        // Au départ, la boîte de dialogue est masquée
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        // Lorsque le joueur appuie sur la touche M
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleDialogue();
        }

        // Si la boîte de dialogue est affichée, elle suit le NPC
        if (isDialogueVisible)
        {
            UpdateDialoguePosition();
        }
    }

    private void ToggleDialogue()
    {
        // Alterne l’état de visibilité de la boîte de dialogue
        isDialogueVisible = !isDialogueVisible;
        dialogueBox.SetActive(isDialogueVisible);

        // Log pour vérifier l'état
        Debug.Log("Dialogue visible : " + isDialogueVisible);

        // Affiche le message s’il y a une interaction
        if (isDialogueVisible)
        {
            dialogueText.text = dialogueMessage;
            UpdateDialoguePosition();
        }
    }


    public void UpdateDialoguePosition()
    {
        // Calcule la position pour afficher la boîte de dialogue au-dessus du NPC
        Vector3 screenPos = Camera.main.WorldToScreenPoint(npcTransform.position);
        dialogueBox.transform.position = screenPos + new Vector3(0, dialogueOffset, 0);
    }
}
