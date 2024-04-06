using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class picudoScript : MonoBehaviour
{
 private bool jugadorEnContacto; // Variable para rastrear si el jugador está en contacto
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación

    [SerializeField] private AudioClip SonidoIniciar;

    [SerializeField] private GameObject img_sembrar;
    private string sceneName;

    void Update()
    {
        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion))
        {
            ActivarPlantarElemento();
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
        SceneManager.LoadScene("SceneLevel3");
        GameManager.Instance.LoadPlayerPosition();
    }

    private void ActivarPlantarElemento(){
        Debug.Log("Colicion Picudo");
    }

}
