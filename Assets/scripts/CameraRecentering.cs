using UnityEngine;
using Cinemachine;

public class CameraRecentering : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera; // Référence à ta FreeLook Camera
    public float targetYAxis = 0.3f;           // Valeur de recentrage souhaitée sur l'axe Y
    public float recenterDelay = 5f;           // Délai avant que le recentrage ne commence
    public float recenterSpeed = 2f;           // Vitesse de recentrage

    private float idleTime = 0f;               // Temps écoulé sans mouvement d'entrée sur l'axe Y

    void Update()
    {
        // Vérifie si l'utilisateur utilise l'axe Y de la caméra
        if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f)
        {
            // Remet à zéro le temps d'inactivité si la caméra est en mouvement
            idleTime = 0f;
        }
        else
        {
            // Incrémente le temps d'inactivité
            idleTime += Time.deltaTime;

            // Si le temps d'inactivité dépasse le délai, effectue le recentrage
            if (idleTime >= recenterDelay)
            {
                // Interpoler en douceur vers la valeur cible de l'axe Y

                freeLookCamera.m_YAxis.Value = Mathf.Lerp(freeLookCamera.m_YAxis.Value, targetYAxis, recenterSpeed * Time.deltaTime);
            }
        }
    }
}
