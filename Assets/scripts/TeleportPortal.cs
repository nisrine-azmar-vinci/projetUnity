using UnityEngine;

public class TeleportPortal : MonoBehaviour
{
    public Transform teleportTarget; // L'emplacement ou l'Empty où téléporter le joueur
    public AudioClip teleportSound; // TODO : Ajouter un son pour l'effet
    private AudioSource audioSource;
    
    /*
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (teleportSound != null) audioSource.PlayOneShot(teleportSound);
            other.transform.position = teleportTarget.position;
        }
    }

}

