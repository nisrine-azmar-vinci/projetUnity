using System.Collections;
using UnityEngine;

public class BirdInitialization : MonoBehaviour
{
    private Animator animator;
    private BirdMovement birdMovement; // R�f�rence au script de mouvement
    private bool isSitting = true; // Indique si l'oiseau est encore assis
    public float sitDuration = 10f; // Dur�e avant que l'oiseau se l�ve automatiquement

    void Start()
    {
        animator = GetComponent<Animator>();
        birdMovement = GetComponent<BirdMovement>();

        // D�sactive le mouvement au d�but
        birdMovement.enabled = false;

        // Commence l'animation de s'asseoir d�s le d�but
        animator.SetBool("isSitting", isSitting);
        animator.Play("Sitting");

        // D�marre une coroutine pour surveiller les entr�es ou le d�lai
        StartCoroutine(HandleSittingState());
    }

    private IEnumerator HandleSittingState()
    {
        float elapsedTime = 0f;

        while (isSitting)
        {
            // Si le joueur fournit une entr�e, d�clenche "StandUp"
            if (Input.anyKeyDown)
            {
                StandUp();
                StopCoroutine(HandleSittingState());
                yield break;
            }

            // Si le d�lai est atteint, d�clenche "StandUp"
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= sitDuration)
            {
                StandUp();
                StopCoroutine(HandleSittingState());
                yield break;
            }

            yield return null; // Attendre la prochaine frame
        }
    }

    private void StandUp()
    {
        isSitting = false;
        animator.SetBool("isSitting", isSitting);

        // R�active le mouvement
        birdMovement.enabled = true;
    }
}
