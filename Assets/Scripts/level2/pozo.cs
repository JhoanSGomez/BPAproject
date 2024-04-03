using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pozo : MonoBehaviour
{
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación
    private bool jugadorEnContacto; // Variable para rastrear si el jugador está en contacto
    [SerializeField] private GameObject img_cubeta;
    public itemBuyInformation cubetaBuyItems;

    void Update()
    {

        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion))
        {
            ActivarAnimacionElemento();
        }
    }
    void Start(){
        img_cubeta.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            img_cubeta.SetActive(true);

            jugadorEnContacto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            img_cubeta.SetActive(false);
            jugadorEnContacto = false;
        }
    }

    public void refrescarItems()
    {
        GameManager.Instance.SavePlayerPosition(0);
        SceneManager.LoadScene("SceneLevel2");
        GameManager.Instance.LoadPlayerPosition();
    }

     private void ActivarAnimacionElemento()
    {
        if (jugadorEnContacto)
        {
            GameManager.Instance.addBuyItems(cubetaBuyItems, 1);
            this.refrescarItems();
        }
    }
}
