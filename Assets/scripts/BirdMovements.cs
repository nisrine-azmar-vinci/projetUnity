using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 5f;
    public float flySpeed = 8f;
    public float moveSmoothTime = 0.1f; // Temps pour adoucir le mouvement
    public float rotationSpeed = 10f; // Vitesse de rotation pour le corps de l'oiseau
    private Vector3 currentVelocity;

    private BirdController birdController;
    public Transform cameraTransform; // Référence à la caméra principale

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        birdController = GetComponent<BirdController>();

        // Assigne la caméra principale si elle n'est pas assignée dans l'inspecteur
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        Move();
        Fly();
    }

    private void Move()
    {
        // Récupérer les entrées de déplacement
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        // Calculer la direction en fonction de la caméra
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // On ignore la composante verticale pour éviter les inclinaisons
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Créer un vecteur de mouvement relatif à la caméra
        Vector3 targetVelocity = (forward * moveZ + right * moveX);

        // Appliquer la vitesse au Rigidbody avec un mouvement fluide
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity * moveSpeed, ref currentVelocity, moveSmoothTime);

        // Mettre à jour l'animation de marche
        bool isWalking = moveX != 0 || moveZ != 0;
        birdController.UpdateAnimation(isWalking);

        // Si l'oiseau bouge, oriente-le vers la direction de déplacement
        if (isWalking)
        {
            RotateBird(targetVelocity);
        }
    }

    private void Fly()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, flySpeed, rb.velocity.z);
            birdController.UpdateAnimation(true, true);
        }
    }

    private void RotateBird(Vector3 direction)
    {
        // Ignore l'axe Y pour que l'oiseau reste horizontal
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            // Créer une rotation cible
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            // Interpoler la rotation pour un mouvement fluide
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
