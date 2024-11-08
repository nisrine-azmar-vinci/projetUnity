using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;  // R�f�rence � la bo�te de dialogue (Panel UI)
    public TextMeshProUGUI dialogueText;  // R�f�rence au texte de la bo�te de dialogue
    public Transform npcTransform;  // Position du NPC pour placer la bo�te de dialogue au-dessus
    public string dialogueMessage = "Hello! Can I help you?";  // Message � afficher
    public float dialogueOffset = 2f;  // D�calage de la bo�te de dialogue par rapport au NPC

    private bool isDialogueVisible = false;  // Indique si la bo�te de dialogue est affich�e

    void Start()
    {
        // Au d�part, la bo�te de dialogue est masqu�e
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        // Lorsque le joueur appuie sur la touche M
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleDialogue();
        }

        // Si la bo�te de dialogue est affich�e, elle suit le NPC
        if (isDialogueVisible)
        {
            UpdateDialoguePosition();
        }
    }

    private void ToggleDialogue()
    {
        // Alterne l��tat de visibilit� de la bo�te de dialogue
        isDialogueVisible = !isDialogueVisible;
        dialogueBox.SetActive(isDialogueVisible);

        // Log pour v�rifier l'�tat
        Debug.Log("Dialogue visible : " + isDialogueVisible);

        // Affiche le message s�il y a une interaction
        if (isDialogueVisible)
        {
            dialogueText.text = dialogueMessage;
            UpdateDialoguePosition();
        }
    }


    public void UpdateDialoguePosition()
    {
        // Calcule la position pour afficher la bo�te de dialogue au-dessus du NPC
        Vector3 screenPos = Camera.main.WorldToScreenPoint(npcTransform.position);
        dialogueBox.transform.position = screenPos + new Vector3(0, dialogueOffset, 0);
    }
}
