using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Animator animator;
    public int missionsFinished = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateAnimation(bool isWalking = false, bool isFlying = false)
    {
        if (isFlying)
        {
            animator.SetBool("isWalking", false);
        }

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isFlying", isFlying);
    }
}