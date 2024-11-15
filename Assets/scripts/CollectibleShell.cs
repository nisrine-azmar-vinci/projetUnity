using UnityEngine;

public class CollectibleShell : MonoBehaviour
{
    public Inventory playerInventory; // Référence à l'inventaire du joueur
    public GameObject shellObject;    // Référence à l'objet coquillage

    private bool isPlayerInRange = false; // Détecte si le joueur est proche du coquillage

    // Cette méthode est appelée lorsque le joueur entre dans la zone du trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // Cette méthode est appelée lorsque le joueur quitte la zone du trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // Si le joueur est dans la zone et appuie sur 'E'
        {
            CollectShell();
        }
    }

    private void CollectShell()
    {
        if (isPlayerInRange)
        {
            playerInventory.shellCount += 1;  // Ajoute un coquillage à l'inventaire
            playerInventory.UpdateUI();  // Met à jour l'UI de l'inventaire

            shellObject.SetActive(false); // Désactive le coquillage (ou le détruit si tu veux)
            Debug.Log("Coquillage collecté !");  // Message de débogage
        }
    }
}
