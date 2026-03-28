using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource shootingSoundRifle;
    public AudioSource reloadingSoundRifle;
    public AudioSource emptyMagazineSoundRifle;

    public AudioSource playerChannel;
    public AudioClip playerHurt;
    public AudioClip playerDeath;
    public AudioClip gameOverMusic;

    public AudioSource recordingChannel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
