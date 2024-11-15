using UnityEngine;

public class CollectibleShell : MonoBehaviour
{
    public Inventory playerInventory; // R�f�rence � l'inventaire du joueur
    public GameObject shellObject;    // R�f�rence � l'objet coquillage

    private bool isPlayerInRange = false; // D�tecte si le joueur est proche du coquillage

    // Cette m�thode est appel�e lorsque le joueur entre dans la zone du trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // Cette m�thode est appel�e lorsque le joueur quitte la zone du trigger
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
            playerInventory.shellCount += 1;  // Ajoute un coquillage � l'inventaire
            playerInventory.UpdateUI();  // Met � jour l'UI de l'inventaire

            shellObject.SetActive(false); // D�sactive le coquillage (ou le d�truit si tu veux)
            Debug.Log("Coquillage collect� !");  // Message de d�bogage
        }
    }
}
