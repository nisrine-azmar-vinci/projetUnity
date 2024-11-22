using UnityEngine;

public class PortalVisibility : MonoBehaviour
{
    public ParticleSystem portalParticles;  // R�f�rence au syst�me de particules du portail
    public Transform player;               // R�f�rence au joueur ou � la cam�ra
    public float fadeDistance = 20f;       // Distance � laquelle les particules commencent � s'arr�ter

    void Update()
    {
        // Calculer la distance entre le joueur et le portail
        float distance = Vector3.Distance(player.position, portalParticles.transform.position);

        // Si le joueur est � une distance plus courte que fadeDistance, lancer les particules
        if (distance > fadeDistance)
        {
            if (!portalParticles.isPlaying)  // Si les particules ne sont pas d�j� activ�es
            {
                portalParticles.Play();  // D�marre les particules
            }
        }
        else
        {
            if (portalParticles.isPlaying)  // Si les particules sont d�j� en train de jouer
            {
                portalParticles.Stop();  // Arr�te les particules
            }
        }
    }
}
