using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int plantCount = 0;   // Compteur de plantes collectées
    public int shellCount = 0;   // Compteur de coquillages collectés
    public TextMeshProUGUI plantCounterText;  // Texte UI pour les plantes
    public TextMeshProUGUI shellCounterText;  // Texte UI pour les coquillages

    void Start()
    {
        UpdateUI();  // Met à jour l'affichage de l'inventaire dès le début du jeu
    }

    void Update()
    {
        // Détecte si le joueur appuie sur 'E' pour ramasser un objet
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("La touche E a été pressée.");
            // Vérifie si un objet ramassable est à portée
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
            {
                // Si l'objet ramassé est un coquillage ou une fleur, on l'ajoute à l'inventaire
                Debug.Log("Raycast touche l'objet : " + hit.collider.gameObject.name);
                CollectItem(hit.collider);
            }
        }
    }

    private void CollectItem(Collider itemCollider)
    {
        // Vérifie si l'objet est un coquillage ou une fleur
        if (itemCollider.CompareTag("Shell"))
        {
            AddShells(1);  // Ajoute un coquillage à l'inventaire
            Destroy(itemCollider.gameObject);  // Détruit l'objet ramassé
        }
        else if (itemCollider.CompareTag("Flower"))
        {
            AddPlants(1);  // Ajoute une fleur à l'inventaire
            Destroy(itemCollider.gameObject);  // Détruit l'objet ramassé
        }
    }

    // Méthode pour ajouter des plantes et mettre à jour l'UI
    public void AddPlants(int amount)
    {
        plantCount += amount;
        UpdateUI();
    }

    // Méthode pour ajouter des coquillages et mettre à jour l'UI
    public void AddShells(int amount)
    {
        shellCount += amount;
        UpdateUI();
    }

    // Méthode pour mettre à jour l'affichage de l'inventaire
    public void UpdateUI()
    {
        if (plantCounterText != null)
        {
            plantCounterText.text = "Plants: " + plantCount;
        }

        if (shellCounterText != null)
        {
            shellCounterText.text = "Shells: " + shellCount;
        }
    }
}
