using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterBuy : MonoBehaviour
{
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            StartCoroutine(waitAndLoad(1.5F));
        }
    }

    IEnumerator waitAndLoad(float waitTime){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("storeMenu");
    }
}
