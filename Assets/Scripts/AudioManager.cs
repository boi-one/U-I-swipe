using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioSource menuMusic;
    public AudioSource sfxClip;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        instance = this;
        musicSlider.value = menuMusic.volume;
        sfxSlider.value = sfxClip.volume;
    }

    private void Update()
    {
        menuMusic.volume = musicSlider.value;
        sfxClip.volume = sfxSlider.value;
    }

    public void PlayMenuMusic()
    {
        menuMusic.Play();
    }

    public void PlaySfx()
    {
        sfxClip.Play();
    }


}
