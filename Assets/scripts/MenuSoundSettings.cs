using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider MenuMusicSlider;

    public void SetMusicVolume() {
        float volume = MenuMusicSlider.value;
        myMixer.SetFloat("MenuMusic", volume);
    }
}
