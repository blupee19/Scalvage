using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip BGAmbiance;
    public AudioClip JumpStart;
    public AudioClip JumpEnd;
    public AudioClip Walk;
    public AudioClip Dash;
    public AudioClip closeAttackSound;
    public AudioClip rangeAttackSound;
    public AudioClip buzzSaw;
    public AudioClip swingTrap;
    public AudioClip playerHurt;
    public AudioClip playerDeath;
    public AudioClip enemyHurt;
    public AudioClip trapTrigger;
    public AudioClip Level2BGM;
    public AudioClip Level1BGM;
    public AudioClip Level3BGM;
    public AudioClip checkpont;
    public AudioClip mouseClick;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainLevel1" || SceneManager.GetActiveScene().name == "MainLevel2" || SceneManager.GetActiveScene().name == "MainLevel3" || SceneManager.GetActiveScene().name == "MainMenu") 
        {
            musicSource.clip = BGAmbiance;
            musicSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "UI1" || SceneManager.GetActiveScene().name == "UI2" || SceneManager.GetActiveScene().name == "UI3")
        {
            musicSource.clip = Level3BGM;
            musicSource.Play();
        }

    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
