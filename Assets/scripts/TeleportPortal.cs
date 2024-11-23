using UnityEngine;

public class TeleportPortal : MonoBehaviour
{
    public Transform teleportTarget; // L'emplacement ou l'Empty où téléporter le joueur
    // public AudioClip teleportSound;
    private AudioSource audioSource;
    public BirdController birdController;

    /*
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (birdController.missionsFinished >= 3)
        {
            if (other.CompareTag("Player"))
            {
                //if (teleportSound != null) audioSource.PlayOneShot(teleportSound);
                other.transform.position = teleportTarget.position;
            }
        }
    }

}

