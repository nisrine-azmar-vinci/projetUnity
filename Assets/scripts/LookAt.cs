using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform playerTransform; // R�f�rence au joueur

    void Update()
    {
        if (playerTransform != null)
        {
            // Calcule une direction en ignorant l'axe Y
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0; // Ignore la hauteur (axe Y)

            // Si la direction n'est pas nulle, oriente la bo�te de dialogue
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
