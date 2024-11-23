using UnityEngine;
using System.Collections; // Ajout pour utiliser IEnumerator

public class PlayerPowerController : MonoBehaviour
{
    private float baseSpeed = 5f; // Vitesse de base du joueur
    private float boostedSpeed = 10f; // Vitesse augmentée pendant le boost
    private float speedBoostDuration = 4f; // Durée du boost de vitesse
    private float speedBoostCooldown = 10f; // Durée du cooldown entre deux boosts

    private float currentSpeed;
    private bool isSpeedBoostActive = false;
    private float speedBoostEndTime;
    private bool canActivateSpeedBoost = true; // Indicateur pour vérifier si le boost peut être activé

    // Variables pour la détection de double press
    private float timeBetweenPresses = 0.5f; // Temps maximal entre deux pressions pour être considéré comme un "double press"
    private float lastPressTime = 0f; // Temps de la dernière pression

    private bool hasReceivedSpeedBoost = false; // Indicateur pour savoir si le joueur a reçu le pouvoir du serpent

    void Start()
    {
        currentSpeed = baseSpeed; // La vitesse par défaut au démarrage
    }

    void Update()
    {
        // Vérifiez si le boost de vitesse est actif et s'il a expiré
        if (isSpeedBoostActive && Time.time >= speedBoostEndTime)
        {
            DeactivateSpeedBoost();
        }

        // Détecter la double pression de la flèche haut (flèche avant)
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Changez la touche si nécessaire
        {
            if (Time.time - lastPressTime <= timeBetweenPresses)
            {
                if (canActivateSpeedBoost && hasReceivedSpeedBoost) // Vérifiez que le pouvoir a été reçu
                {
                    ActivateSpeedBoost(); // Activer le boost de vitesse
                    lastPressTime = 0f; // Réinitialiser le temps de la dernière pression
                }
            }
            else
            {
                lastPressTime = Time.time; // Mettre à jour le temps de la dernière pression
            }
        }
    }

    // Méthode pour activer le boost de vitesse
    public void ActivateSpeedBoost()
    {
        if (!isSpeedBoostActive)
        {
            currentSpeed = boostedSpeed; // Appliquer la vitesse boostée
            isSpeedBoostActive = true;
            speedBoostEndTime = Time.time + speedBoostDuration; // Définir le moment où le boost expirera
            canActivateSpeedBoost = false; // Empêcher un nouvel activement avant le cooldown
            Debug.Log("Speed boost activated!");
        }
    }

    // Méthode pour désactiver le boost de vitesse
    private void DeactivateSpeedBoost()
    {
        currentSpeed = baseSpeed; // Revenir à la vitesse de base
        isSpeedBoostActive = false;
        Debug.Log("Speed boost deactivated.");

        // Commencer le cooldown avant de pouvoir réactiver le boost
        StartCoroutine(RestoreBoostCooldown());
    }

    // Cette méthode permet aux autres scripts d'accéder à la vitesse actuelle
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    // Coroutine pour gérer le cooldown entre les boosts
    private IEnumerator RestoreBoostCooldown()
    {
        yield return new WaitForSeconds(speedBoostCooldown); // Attendre le délai de recharge
        canActivateSpeedBoost = true; // Réactiver la possibilité d'activer un boost
        Debug.Log("Boost cooldown ended.");
    }

    // Applique le boost de vitesse à partir du multiplicateur défini dans le PowerScriptableObject
    public void GrantSpeedBoost(PowerScriptableObject speedPower)
    {
        if (speedPower != null)
        {
            // Appliquez ici le multiplicateur de vitesse (par exemple en modifiant la vitesse de marche du joueur)
            float speedMultiplier = speedPower.flightBoostMultiplier; // Utilisez flightBoostMultiplier pour la vitesse
            ApplySpeedBoost(speedMultiplier);
            Debug.Log($"Speed boost activated with multiplier: {speedMultiplier}");
            hasReceivedSpeedBoost = true; // Le joueur a maintenant reçu le pouvoir
        }
    }

    // Applique un multiplicateur de vitesse à la vitesse du joueur
    private void ApplySpeedBoost(float multiplier)
    {
        // Augmente la vitesse de base du joueur en fonction du multiplicateur
        currentSpeed = baseSpeed * multiplier;
        // Applique le boost de vitesse pendant une durée spécifique
        isSpeedBoostActive = true;
        speedBoostEndTime = Time.time + speedBoostDuration;
        Debug.Log($"Speed Boost Applied: {currentSpeed}");
    }
}
