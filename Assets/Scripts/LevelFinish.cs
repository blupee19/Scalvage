using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{

    public GameObject eyeEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(WaitForLevelChange());
        }
    }

    private IEnumerator WaitForLevelChange()
    {
        yield return new WaitForSeconds(2f);
        if (SceneManager.GetActiveScene().name == "MainLevel1") SceneManager.LoadSceneAsync(3);
        if (SceneManager.GetActiveScene().name == "MainLevel2") SceneManager.LoadSceneAsync(5);

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainLevel3" && eyeEnemy == null) SceneManager.LoadSceneAsync(7);
    }
}
