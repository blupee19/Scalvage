using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        audioManager.PlaySFX(audioManager.Level3BGM);
    }

    public void PlayGame()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu") SceneManager.LoadSceneAsync(1);
        if(SceneManager.GetActiveScene().name == "UI1") SceneManager.LoadSceneAsync(2);
        if(SceneManager.GetActiveScene().name == "UI2") SceneManager.LoadSceneAsync(4);
        if(SceneManager.GetActiveScene().name == "UI3") SceneManager.LoadSceneAsync(6);
        if(SceneManager.GetActiveScene().name == "UI4") SceneManager.LoadSceneAsync(0);


        audioManager.PlaySFX(audioManager.mouseClick);
    }

    public void Quit()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") Application.Quit();
    }
}
