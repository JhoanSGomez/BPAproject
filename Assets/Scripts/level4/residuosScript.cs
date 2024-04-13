using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class residuosScript : MonoBehaviour
{
    private bool jugadorEnContacto;
    public KeyCode teclaActivacion = KeyCode.Space;
    public itemBuyInformation itemInformation;

     void Update()
    {
        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion))
        {
            activarAnimacion();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // img_sembrar.SetActive(true);
            jugadorEnContacto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // img_sembrar.SetActive(false);
            jugadorEnContacto = false;
        }
    }
    public void refrescarItems()
    {
        GameManager.Instance.SavePlayerPosition(0);
        SceneManager.LoadScene("SceneLevel4");
        GameManager.Instance.LoadPlayerPosition();
    }

    private void activarAnimacion(){
        if (jugadorEnContacto){
            Transform parentTransform = transform.parent;
            Destroy(gameObject);
            GameManager.Instance.addBuyItems(itemInformation, 1);
            refrescarItems();
        }
    }
}
