using System.Collections;
using UnityEngine;

public class BirdInitialization : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Commence l'animation de s'asseoir d�s le d�but
        animator.Play("Sitting");

        // D�marre une coroutine pour le d�lai avant de passer � la position normale
        StartCoroutine(StandUpAfterDelay());
    }

    private IEnumerator StandUpAfterDelay()
    {
        // Attend 2 secondes
        yield return new WaitForSeconds(5);

        // D�clenche la transition vers l'animation "idle"
        animator.SetTrigger("StandUp");
    }
}
