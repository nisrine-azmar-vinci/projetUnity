using UnityEngine;
using Cinemachine;

public class CameraRecentering : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera; // R�f�rence � ta FreeLook Camera
    public float targetYAxis = 0.3f;           // Valeur de recentrage souhait�e sur l'axe Y
    public float recenterDelay = 5f;           // D�lai avant que le recentrage ne commence
    public float recenterSpeed = 2f;           // Vitesse de recentrage

    private float idleTime = 0f;               // Temps �coul� sans mouvement d'entr�e sur l'axe Y

    void Update()
    {
        // V�rifie si l'utilisateur utilise l'axe Y de la cam�ra
        if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f)
        {
            // Remet � z�ro le temps d'inactivit� si la cam�ra est en mouvement
            idleTime = 0f;
        }
        else
        {
            // Incr�mente le temps d'inactivit�
            idleTime += Time.deltaTime;

            // Si le temps d'inactivit� d�passe le d�lai, effectue le recentrage
            if (idleTime >= recenterDelay)
            {
                // Interpoler en douceur vers la valeur cible de l'axe Y

                freeLookCamera.m_YAxis.Value = Mathf.Lerp(freeLookCamera.m_YAxis.Value, targetYAxis, recenterSpeed * Time.deltaTime);
            }
        }
    }
}
