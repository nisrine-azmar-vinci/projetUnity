using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int shellCount = 0;  // Nombre de coquillages collectés
    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        UpdateUI();
    }

    public void AddShell()
    {
        shellCount++;
        UpdateUI();
    }

    public bool RemoveShells(int amount)
    {
        if (shellCount >= amount)
        {
            shellCount -= amount;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Pas assez de coquillages !");
            return false;
        }
    }

    private void UpdateUI()
    {
        uiManager.UpdateShellCounter(shellCount);
    }

    private void OnTriggerStay(Collider other)
    {
        // Vérifie si le joueur est en contact avec un coquillage
        if (other.CompareTag("Shell") && Input.GetKeyDown(KeyCode.E))
        {
            CollectShell(other.gameObject);  // Ramasse le coquillage si E est pressé
        }
    }

    private void CollectShell(GameObject shell)
    {
        AddShell();
        Destroy(shell);  // Supprime le coquillage de la scène
        Debug.Log("Coquillage ramassé ! Nombre total : " + shellCount);
    }
}
