using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerPowerController playerPowerController;
    public float jumpSpeed = 7f;
    public float moveSmoothTime = 0.1f; // Temps pour adoucir le mouvement
    public float rotationSpeed = 15f; // Vitesse de rotation pour le corps de l'oiseau
    private Vector3 currentVelocity;

    private BirdController birdController;
    public Transform cameraTransform; // Référence à la caméra principale
    private bool isGrounded; // Indicateur pour savoir si l'oiseau est au sol

    // Paramètres pour la gestion des pentes et des escaliers
    public float stepHeight = 0.022f; // Hauteur maximale des marches
    public float stepSmooth = 0.022f; // Vitesse de montée des marches

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        birdController = GetComponent<BirdController>();
        playerPowerController = FindObjectOfType<PlayerPowerController>();

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
        StepClimb();
        Jump();
        UpdateAnimation();
    }

    private void CheckGrounded()
    {
        isGrounded = Mathf.Abs(rb.velocity.y) < 0.1f;
    }

    private void Move()
    {
        // Récupérer les entrées de déplacement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float moveSpeed = playerPowerController.GetCurrentSpeed();

        // Calculer la direction en fonction de la caméra
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // On ignore la composante verticale pour éviter les inclinaisons
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Créer un vecteur de mouvement relatif à la caméra
        Vector3 targetVelocity = (forward * moveZ + right * moveX) * moveSpeed;

        // Applique la vitesse au Rigidbody de manière fluide
        rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z), ref currentVelocity, moveSmoothTime);

        // Si l'oiseau bouge, oriente-le vers la direction de déplacement
        if (moveX != 0 || moveZ != 0)
        {
            RotateBird(targetVelocity, moveSpeed);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void StepClimb()
    {
        // Rayon pour détecter les obstacles directement devant
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, transform.forward, out RaycastHit hit, 0.5f))
        {
            // Si un obstacle est détecté, vérifie s'il s'agit d'une marche
            if (hit.normal.y < 0.1f && hit.distance < 0.5f)
            {
                // Rayon pour vérifier au-dessus de l'obstacle
                if (Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.forward, out RaycastHit stepHit, 0.5f))
                {
                    // Si le rayon détecte un espace libre, monte l'oiseau
                    rb.position += Vector3.up * stepSmooth;
                }
            }
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

    private void RotateBird(Vector3 direction, float moveSpeed)
    {
        direction.y = 0f; // Ignorer la composante Y pour éviter une rotation verticale

        // Si la direction est significative et que la vitesse est suffisamment grande, alors appliquer une rotation
        if (direction.magnitude > 0.1f)
        {
            Vector3 lookPosition = transform.position + direction;
            transform.LookAt(lookPosition);
        }
    }

    private void UpdateAnimation()
    {
        // Si l'oiseau est au sol, on met à jour l'animation de marche
        if (isGrounded)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            bool isWalking = moveX != 0 || moveZ != 0;
            birdController.UpdateAnimation(isWalking, false);
        }
        else
        {
            // Met à jour l'animation en fonction de la vitesse de déplacement dans l'air
            birdController.UpdateAnimation(false, true);
        }
    }
}
