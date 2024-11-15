using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    public Transform playerTransform; // Référence au joueur
    private NavMeshAgent agent;

    public float followDistance = 2.0f;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > followDistance)
            {
                agent.SetDestination(playerTransform.position);
                animator.SetBool("isWalking", true); // Active l'animation de marche
            }
            else
            {
                agent.ResetPath();
                animator.SetBool("isWalking", false); // Passe à l'animation d'arrêt
            }
        }
    }
}
