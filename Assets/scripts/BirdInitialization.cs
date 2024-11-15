using System.Collections;
using UnityEngine;

public class BirdInitialization : MonoBehaviour
{
    private Animator animator;
    private BirdMovement birdMovement; // Référence au script de mouvement
    private bool isSitting = true; // Indique si l'oiseau est encore assis

    public float sitDuration = 2f; // Durée avant que l'oiseau se lève automatiquement

    void Start()
    {
        animator = GetComponent<Animator>();
        birdMovement = GetComponent<BirdMovement>();

        // Désactive le mouvement au début
        birdMovement.enabled = false;

        // Commence l'animation de s'asseoir dès le début
        animator.Play("Sitting");

        // Démarre une coroutine pour surveiller les entrées ou le délai
        StartCoroutine(HandleSittingState());
    }

    private IEnumerator HandleSittingState()
    {
        float elapsedTime = 0f;

        while (isSitting)
        {
            // Si le joueur fournit une entrée, déclenche "StandUp"
            if (Input.anyKeyDown)
            {
                StandUp();
                yield break;
            }

            // Si le délai est atteint, déclenche "StandUp"
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= sitDuration)
            {
                StandUp();
                yield break;
            }

            yield return null; // Attendre la prochaine frame
        }
    }

    private void StandUp()
    {
        isSitting = false;

        // Déclenche la transition vers l'animation "idle"
        animator.SetTrigger("StandUp");

        // Réactive le mouvement
        birdMovement.enabled = true;
    }
}
