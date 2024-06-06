using UnityEngine;
using UnityEngine.UI;

public class volumeSettings : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider volumeSlider;

    public void SetMusicVolume()
    {
        audioSource.volume = volumeSlider.value;
    }
}
