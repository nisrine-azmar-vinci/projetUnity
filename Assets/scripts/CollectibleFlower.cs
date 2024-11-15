using UnityEngine;

public class CollectibleFlower : MonoBehaviour
{
    public Inventory playerInventory; // Référence à l'inventaire du joueur
    public GameObject flowerObject;    // Référence à l'objet fleur

    private bool isPlayerInRange = false; // Détecte si le joueur est proche de la fleur

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

    void Start()
    {
        // Trouve le script Inventory attaché à l'objet Player
        playerInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // Si le joueur est dans la zone et appuie sur 'E'
        {
            CollectFlower();
        }
    }

    private void CollectFlower()
    {
        if (isPlayerInRange && playerInventory != null)
        {
            playerInventory.plantCount += 1;  // Ajoute une fleur à l'inventaire
            playerInventory.UpdateUI();  // Met à jour l'UI de l'inventaire

            flowerObject.SetActive(false); // Désactive la fleur (ou la détruit si tu veux)
            Debug.Log("Fleur collectée !");  // Message de débogage
        }
    }
}
