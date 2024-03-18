using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterStore_level2 : MonoBehaviour
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

        if (sceneName == "StoreLevel2"){     
            GameManager.Instance.setScore();
            SceneManager.LoadScene("SceneLevel2");
            
            GameManager.Instance.LoadPlayerPosition();
        }else {
            GameManager.Instance.SavePlayerPosition(1);
            GameManager.Instance.setScore();
            SceneManager.LoadScene("StoreLevel2");
            GameManager.Instance.ResetPlayerPositionFixed(34F,1F);
        }
    }
}
