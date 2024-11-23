using UnityEngine;
using System.Collections; // Ajout pour utiliser IEnumerator

public class PlayerPowerController : MonoBehaviour
{
    private float baseSpeed = 5f; // Vitesse de base du joueur
    private float boostedSpeed = 10f; // Vitesse augment�e pendant le boost
    private float speedBoostDuration = 4f; // Dur�e du boost de vitesse
    private float speedBoostCooldown = 10f; // Dur�e du cooldown entre deux boosts

    private float currentSpeed;
    private bool isSpeedBoostActive = false;
    private float speedBoostEndTime;
    private bool canActivateSpeedBoost = true; // Indicateur pour v�rifier si le boost peut �tre activ�

    // Variables pour la d�tection de double press
    private float timeBetweenPresses = 0.5f; // Temps maximal entre deux pressions pour �tre consid�r� comme un "double press"
    private float lastPressTime = 0f; // Temps de la derni�re pression

    private bool hasReceivedSpeedBoost = false; // Indicateur pour savoir si le joueur a re�u le pouvoir du serpent

    void Start()
    {
        currentSpeed = baseSpeed; // La vitesse par d�faut au d�marrage
    }

    void Update()
    {
        // V�rifiez si le boost de vitesse est actif et s'il a expir�
        if (isSpeedBoostActive && Time.time >= speedBoostEndTime)
        {
            DeactivateSpeedBoost();
        }

        // D�tecter la double pression de la fl�che haut (fl�che avant)
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Changez la touche si n�cessaire
        {
            if (Time.time - lastPressTime <= timeBetweenPresses)
            {
                if (canActivateSpeedBoost && hasReceivedSpeedBoost) // V�rifiez que le pouvoir a �t� re�u
                {
                    ActivateSpeedBoost(); // Activer le boost de vitesse
                    lastPressTime = 0f; // R�initialiser le temps de la derni�re pression
                }
            }
            else
            {
                lastPressTime = Time.time; // Mettre � jour le temps de la derni�re pression
            }
        }
    }

    // M�thode pour activer le boost de vitesse
    public void ActivateSpeedBoost()
    {
        if (!isSpeedBoostActive)
        {
            currentSpeed = boostedSpeed; // Appliquer la vitesse boost�e
            isSpeedBoostActive = true;
            speedBoostEndTime = Time.time + speedBoostDuration; // D�finir le moment o� le boost expirera
            canActivateSpeedBoost = false; // Emp�cher un nouvel activement avant le cooldown
            Debug.Log("Speed boost activated!");
        }
    }

    // M�thode pour d�sactiver le boost de vitesse
    private void DeactivateSpeedBoost()
    {
        currentSpeed = baseSpeed; // Revenir � la vitesse de base
        isSpeedBoostActive = false;
        Debug.Log("Speed boost deactivated.");

        // Commencer le cooldown avant de pouvoir r�activer le boost
        StartCoroutine(RestoreBoostCooldown());
    }

    // Cette m�thode permet aux autres scripts d'acc�der � la vitesse actuelle
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    // Coroutine pour g�rer le cooldown entre les boosts
    private IEnumerator RestoreBoostCooldown()
    {
        yield return new WaitForSeconds(speedBoostCooldown); // Attendre le d�lai de recharge
        canActivateSpeedBoost = true; // R�activer la possibilit� d'activer un boost
        Debug.Log("Boost cooldown ended.");
    }

    // Applique le boost de vitesse � partir du multiplicateur d�fini dans le PowerScriptableObject
    public void GrantSpeedBoost(PowerScriptableObject speedPower)
    {
        if (speedPower != null)
        {
            // Appliquez ici le multiplicateur de vitesse (par exemple en modifiant la vitesse de marche du joueur)
            float speedMultiplier = speedPower.flightBoostMultiplier; // Utilisez flightBoostMultiplier pour la vitesse
            ApplySpeedBoost(speedMultiplier);
            Debug.Log($"Speed boost activated with multiplier: {speedMultiplier}");
            hasReceivedSpeedBoost = true; // Le joueur a maintenant re�u le pouvoir
        }
    }

    // Applique un multiplicateur de vitesse � la vitesse du joueur
    private void ApplySpeedBoost(float multiplier)
    {
        // Augmente la vitesse de base du joueur en fonction du multiplicateur
        currentSpeed = baseSpeed * multiplier;
        // Applique le boost de vitesse pendant une dur�e sp�cifique
        isSpeedBoostActive = true;
        speedBoostEndTime = Time.time + speedBoostDuration;
        Debug.Log($"Speed Boost Applied: {currentSpeed}");
    }
}
