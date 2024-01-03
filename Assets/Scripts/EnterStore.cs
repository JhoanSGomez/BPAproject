using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterStore : MonoBehaviour
{
    private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            StartCoroutine(waitAndLoad(1.5F));
        }
    }

    IEnumerator waitAndLoad(float waitTime){
        yield return new WaitForSeconds(waitTime);
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Store"){     
            GameManager.Instance.setScore();
            SceneManager.LoadScene("SampleScene");
            GameManager.Instance.LoadPlayerPosition();
        }else {
            GameManager.Instance.SavePlayerPosition();
            GameManager.Instance.setScore();
            SceneManager.LoadScene("Store");
        }
    }
}
