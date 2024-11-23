using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private Animator chestAnimator; // R�f�rence � l'Animator du coffre
    private bool isChestOpen = false; // V�rifie si le coffre est d�j� ouvert

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
