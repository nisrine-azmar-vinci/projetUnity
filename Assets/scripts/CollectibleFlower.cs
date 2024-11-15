using UnityEngine;

public class CollectibleFlower : MonoBehaviour
{
    public Inventory playerInventory; // R�f�rence � l'inventaire du joueur
    public GameObject flowerObject;    // R�f�rence � l'objet fleur

    private bool isPlayerInRange = false; // D�tecte si le joueur est proche de la fleur

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

    void Start()
    {
        // Trouve le script Inventory attach� � l'objet Player
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
            playerInventory.plantCount += 1;  // Ajoute une fleur � l'inventaire
            playerInventory.UpdateUI();  // Met � jour l'UI de l'inventaire

            flowerObject.SetActive(false); // D�sactive la fleur (ou la d�truit si tu veux)
            Debug.Log("Fleur collect�e !");  // Message de d�bogage
        }
    }
}
