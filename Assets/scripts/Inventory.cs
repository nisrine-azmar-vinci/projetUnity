using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int plantCount = 0;   // Compteur de plantes collect�es
    public int shellCount = 0;   // Compteur de coquillages collect�s
    public TextMeshProUGUI plantCounterText;  // Texte UI pour les plantes
    public TextMeshProUGUI shellCounterText;  // Texte UI pour les coquillages

    void Start()
    {
        UpdateUI();  // Met � jour l'affichage de l'inventaire d�s le d�but du jeu
    }

    void Update()
    {
        // D�tecte si le joueur appuie sur 'E' pour ramasser un objet
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("La touche E a �t� press�e.");
            // V�rifie si un objet ramassable est � port�e
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
            {
                // Si l'objet ramass� est un coquillage ou une fleur, on l'ajoute � l'inventaire
                Debug.Log("Raycast touche l'objet : " + hit.collider.gameObject.name);
                CollectItem(hit.collider);
            }
        }
    }

    private void CollectItem(Collider itemCollider)
    {
        // V�rifie si l'objet est un coquillage ou une fleur
        if (itemCollider.CompareTag("Shell"))
        {
            AddShells(1);  // Ajoute un coquillage � l'inventaire
            Destroy(itemCollider.gameObject);  // D�truit l'objet ramass�
        }
        else if (itemCollider.CompareTag("Flower"))
        {
            AddPlants(1);  // Ajoute une fleur � l'inventaire
            Destroy(itemCollider.gameObject);  // D�truit l'objet ramass�
        }
    }

    // M�thode pour ajouter des plantes et mettre � jour l'UI
    public void AddPlants(int amount)
    {
        plantCount += amount;
        UpdateUI();
    }

    // M�thode pour ajouter des coquillages et mettre � jour l'UI
    public void AddShells(int amount)
    {
        shellCount += amount;
        UpdateUI();
    }

    // M�thode pour mettre � jour l'affichage de l'inventaire
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
