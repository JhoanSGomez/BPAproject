using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterBuy : MonoBehaviour
{
    [SerializeField] private GameObject img_compra_store;
    private bool isPlayerInRange;
    [SerializeField] public string sceneName;


     private void Start()
    {
         img_compra_store.SetActive(false);
    }
     // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
             SceneManager.LoadScene(sceneName); //storeMenu
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            img_compra_store.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            img_compra_store.SetActive(false);
        }
    }
}
