using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterBuy : MonoBehaviour
{
    /* public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            StartCoroutine(waitAndLoad(1.5F));
        }
    }

    IEnumerator waitAndLoad(float waitTime){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("storeMenu");
    } */

    [SerializeField] private GameObject dialogMark;
     private bool isPlayerInRange;


     private void Start()
    {
         dialogMark.SetActive(false);
    }
     // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
             SceneManager.LoadScene("storeMenu");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogMark.SetActive(false);
        }
    }
}
