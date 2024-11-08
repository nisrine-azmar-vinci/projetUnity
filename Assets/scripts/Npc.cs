using UnityEngine;

public class NPC : MonoBehaviour
{
    public int shellsRequired = 5;  // Nombre de coquillages nécessaires pour l'échange
    public string reward = "Cape de vol";  // Récompense pour le joueur
    private Inventory playerInventory; // Référence à l'inventaire du joueur

    private void Start()
    {
        // Récupère l'inventaire du joueur. Assurez-vous que l'inventaire est sur le même GameObject que le joueur.
        playerInventory = FindObjectOfType<Inventory>();
    }

    // Cette méthode sera appelée lorsqu'on interagit avec le PNJ
    public void Interact()
    {
        if (playerInventory.RemoveShells(shellsRequired))
        {
            GiveReward();
        }
        else
        {
            Debug.Log("Vous n'avez pas assez de coquillages !");
        }
    }

    // Fonction qui donne la récompense au joueur
    private void GiveReward()
    {
        Debug.Log("Vous avez reçu : " + reward);
        // Ajoute ici la logique pour donner la récompense spécifique
    }
}
