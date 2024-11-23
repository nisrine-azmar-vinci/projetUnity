using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private Animator chestAnimator; // Référence à l'Animator du coffre
    private bool isChestOpen = false; // Vérifie si le coffre est déjà ouvert

    void Start()
    {
        chestAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isChestOpen)
        {
            chestAnimator.SetBool("isChestOpen", true);
            isChestOpen = true;
        }
    }
}
