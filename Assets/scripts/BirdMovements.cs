using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;
    public float moveSmoothTime = 0.1f; // Temps pour adoucir le mouvement
    public float rotationSpeed = 15f; // Vitesse de rotation pour le corps de l'oiseau
    private Vector3 currentVelocity;

    private BirdController birdController;
    public Transform cameraTransform; // Référence à la caméra principale
    private bool isGrounded; // Indicateur pour savoir si l'oiseau est au sol

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
        CheckGrounded();
        Move();
        Jump();
        UpdateAnimation();
    }

    private void CheckGrounded()
    {
        isGrounded = Mathf.Abs(rb.velocity.y) < 0.01f;
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

        // Applique la vitesse au Rigidbody de manière fluide
        rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z), ref currentVelocity, moveSmoothTime);

        // Mettre à jour l'animation de marche
        bool isWalking = moveX != 0 || moveZ != 0;

        // Si l'oiseau bouge, oriente-le vers la direction de déplacement
        if (isWalking)
        {
            RotateBird(targetVelocity);
        }
    }

    private void Jump()
    {
        // Vérifie si le joueur appuie sur "Espace" pour sauter
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            isGrounded = false; // Empêche un nouveau saut jusqu'à toucher le sol
        }
    }

    private void RotateBird(Vector3 direction)
    {
        direction.y = 0f; // On ignore la composante Y pour garder l'orientation horizontale
        Vector3 lookPosition = transform.position + direction;

        // Oriente l'oiseau vers la direction de déplacement
        transform.LookAt(lookPosition);
    }

    private void UpdateAnimation()
    {
        // Si l'oiseau est au sol, on met à jour l'animation de marche
        if (isGrounded)
        {
            bool isWalking = rb.velocity.x != 0 || rb.velocity.z != 0;
            birdController.UpdateAnimation(isWalking);
        }
        else
        {
            // Met à jour l'animation en fonction de la vitesse de déplacement dans l'air
            birdController.UpdateAnimation(false, true);
        }
    }
}
