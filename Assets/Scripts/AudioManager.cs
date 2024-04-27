using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] AudioClip collectingSound;
    [SerializeField] [Range(0f, 1f)] public float collectingVolume = 1f;

    [Header("Music")]
    [SerializeField] AudioClip musicSound;
    [SerializeField] [Range(0f, 1f)] public float musicVolume = 1f;
    [SerializeField] private AudioSource musicSource; 
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    private float musicValue;
    private float soundValue;

    public static AudioManager instance;

    void Awake()
    {
        ManageSingleton();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicSound;
        musicSource.volume = musicVolume;
        musicSource.loop = true; 
    }

    void Start()
    {
        PlayMusic(); 
        musicSlider.onValueChanged.AddListener(HandleMusicVolumeChange);
        soundSlider.onValueChanged.AddListener(HandleSoundVolumeChange);
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySoundClip()
    {
        PlayClip(collectingSound, collectingVolume);
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
    private void HandleMusicVolumeChange(float value)
    {
        musicVolume = value;
        musicSource.volume = musicVolume;  
    }

    private void HandleSoundVolumeChange(float value)
    {
        collectingVolume = value;  
    }
}
