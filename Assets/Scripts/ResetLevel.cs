using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") 
        {
            StartCoroutine(WaitForDeath());
        }
    }

    private IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
