using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
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
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync(2);
        
    }
}
