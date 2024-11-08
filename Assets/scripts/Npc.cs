using UnityEngine;

public class NPC : MonoBehaviour
{
    public int shellsRequired = 5;  // Nombre de coquillages n�cessaires pour l'�change
    public string reward = "Cape de vol";  // R�compense pour le joueur
    private Inventory playerInventory; // R�f�rence � l'inventaire du joueur

    private void Start()
    {
        // R�cup�re l'inventaire du joueur. Assurez-vous que l'inventaire est sur le m�me GameObject que le joueur.
        playerInventory = FindObjectOfType<Inventory>();
    }

    // Cette m�thode sera appel�e lorsqu'on interagit avec le PNJ
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

    // Fonction qui donne la r�compense au joueur
    private void GiveReward()
    {
        Debug.Log("Vous avez re�u : " + reward);
        // Ajoute ici la logique pour donner la r�compense sp�cifique
    }
}
