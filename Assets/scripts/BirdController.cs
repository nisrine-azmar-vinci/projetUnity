using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateAnimation(bool isWalking, bool isFlying = false)
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isFlying", isFlying);

        // Assurez-vous que si on est en train de voler, on ne marche pas
        if (isFlying)
        {
            animator.SetBool("isWalking", false); // Arrête la marche si on vole
        }
    }
}
