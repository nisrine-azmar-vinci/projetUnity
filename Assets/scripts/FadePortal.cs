using UnityEngine;

public class PortalVisibility : MonoBehaviour
{
    public ParticleSystem portalParticles;  // Référence au système de particules du portail
    public Transform player;               // Référence au joueur ou à la caméra
    public float fadeDistance = 20f;       // Distance à laquelle les particules commencent à s'arrêter

    void Update()
    {
        // Calculer la distance entre le joueur et le portail
        float distance = Vector3.Distance(player.position, portalParticles.transform.position);

        // Si le joueur est à une distance plus courte que fadeDistance, lancer les particules
        if (distance > fadeDistance)
        {
            if (!portalParticles.isPlaying)  // Si les particules ne sont pas déjà activées
            {
                portalParticles.Play();  // Démarre les particules
            }
        }
        else
        {
            if (portalParticles.isPlaying)  // Si les particules sont déjà en train de jouer
            {
                portalParticles.Stop();  // Arrête les particules
            }
        }
    }
}
