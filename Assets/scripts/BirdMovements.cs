using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerPowerController playerPowerController;
    //public float moveSpeed = 5f;
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
        float moveX = Input.GetAxis("Horizontal") ;
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

        // Mettre à jour l'animation de marche
        bool isWalking = moveX != 0 || moveZ != 0;

        // Si l'oiseau bouge, oriente-le vers la direction de déplacement
        if (isWalking)
        {
            RotateBird(targetVelocity, moveSpeed);
        }
        else
        {
            // Lorsque l'oiseau ne se déplace pas, on peut l'empêcher de tourner
            // Eviter que l'oiseau tourne sur lui-même quand il est immobile
            // L'oiseau ne fait rien en termes de rotation ici
            rb.angularVelocity = Vector3.zero;
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
            // Calculer la rotation de l'oiseau
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Appliquer une rotation en douceur avec un certain facteur de vitesse de rotation
            // Limiter la vitesse de rotation en fonction de la vitesse de déplacement
            float rotationFactor = Mathf.Clamp(rotationSpeed * (moveSpeed / 5f), 1f, 10f); // Le facteur de rotation varie selon la vitesse
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactor * Time.deltaTime);
        }
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