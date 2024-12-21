using UnityEngine;

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
    public AudioClip checkpont;

    private void Start()
    {
        musicSource.clip = BGAmbiance;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
