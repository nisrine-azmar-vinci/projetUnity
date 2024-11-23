using UnityEngine;

public class PortalController : MonoBehaviour
{
    public ParticleSystem bigPortal;
    public ParticleSystem smallPortal;
    public BirdController birdController;

    void Start()
    {
        bigPortal.Stop();
        smallPortal.Stop();
    }

    void Update()
    {
        if (birdController.missionsFinished >= 3 && !bigPortal.isPlaying && !smallPortal.isPlaying)
        {
            bigPortal.Play();
            smallPortal.Play();
        }
    }
}
