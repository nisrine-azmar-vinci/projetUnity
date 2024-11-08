using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private NPC currentNPC;

    void Update()
    {
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E)) // E pour interagir
        {
            currentNPC.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<NPC>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = null;
        }
    }
}
