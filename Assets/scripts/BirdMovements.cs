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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        birdController = GetComponent<BirdController>();
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

        // Créer un vecteur de mouvement
        Vector3 targetVelocity = new Vector3(moveX, rb.velocity.y, moveZ);

        // Interpoler la vitesse pour un mouvement plus fluide
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, moveSmoothTime);

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
