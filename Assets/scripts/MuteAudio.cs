using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{ 
    public AudioSource audioSource;
    public bool muted = false;
    public void MuteToggle()
    {
        if (!muted)
        {
            audioSource.Pause();
            muted = true;
        }
        else
        {
            audioSource.UnPause();
            muted = false;
        }

    }
}

    