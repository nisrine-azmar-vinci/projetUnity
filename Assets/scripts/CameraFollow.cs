using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Référence à l'oiseau
    public float height = 3f;       // Hauteur de la caméra
    public float distance = 5f;      // Distance de la caméra
    public float smoothSpeed = 0.125f; // Vitesse de lissage
    public float rotationSpeed = 5f; // Vitesse de rotation pour ajuster l'angle

    private void LateUpdate()
    {
        if (target != null)
        {
            // Position de la caméra par rapport à la cible
            Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;

            // Lissage de la position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Faire regarder la caméra vers l'oiseau
            Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
