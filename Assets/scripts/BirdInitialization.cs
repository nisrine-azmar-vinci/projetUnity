using System.Collections;
using UnityEngine;

public class BirdInitialization : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Commence l'animation de s'asseoir dès le début
        animator.Play("Sitting");

        // Démarre une coroutine pour le délai avant de passer à la position normale
        StartCoroutine(StandUpAfterDelay());
    }

    private IEnumerator StandUpAfterDelay()
    {
        // Attend 2 secondes
        yield return new WaitForSeconds(5);

        // Déclenche la transition vers l'animation "idle"
        animator.SetTrigger("StandUp");
    }
}
