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
    public Transform cameraTransform; // R�f�rence � la cam�ra principale
    private bool isGrounded; // Indicateur pour savoir si l'oiseau est au sol

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        birdController = GetComponent<BirdController>();

        // Assigne la cam�ra principale si elle n'est pas assign�e dans l'inspecteur
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        CheckGrounded();
        if (isGrounded)
        {
            Move();
            Jump();
        }
        if (!isGrounded)
        {
            // Met � jour l'animation de vol quand l'oiseau est en l'air
            birdController.UpdateAnimation(false, true);
        }
    }

    private void CheckGrounded()
    {
        // V�rifie si l'oiseau est en contact avec le sol en utilisant un raycast
        isGrounded = isGrounded = Mathf.Abs(rb.velocity.y) < 0.05f;
    }

    private void Move()
    {
        // R�cup�rer les entr�es de d�placement
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        // Calculer la direction en fonction de la cam�ra
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // On ignore la composante verticale pour �viter les inclinaisons
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Cr�er un vecteur de mouvement relatif � la cam�ra
        Vector3 targetVelocity = (forward * moveZ + right * moveX);

        // Applique la vitesse au Rigidbody de mani�re fluide
        rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z), ref currentVelocity, moveSmoothTime);

        // Mettre � jour l'animation de marche
        bool isWalking = moveX != 0 || moveZ != 0;
        birdController.UpdateAnimation(isWalking);

        // Si l'oiseau bouge, oriente-le vers la direction de d�placement
        if (isWalking)
        {
            RotateBird(targetVelocity);
        }
    }

    private void Jump()
    {
        // V�rifie si le joueur appuie sur "Espace" pour sauter
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            isGrounded = false; // Emp�che un nouveau saut jusqu'� toucher le sol
        }
    }

    private void RotateBird(Vector3 direction)
    {
        direction.y = 0f; // On ignore la composante Y pour garder l'orientation horizontale
        Vector3 lookPosition = transform.position + direction;

        // Oriente l'oiseau vers la direction de d�placement
        transform.LookAt(lookPosition);
    }
}
