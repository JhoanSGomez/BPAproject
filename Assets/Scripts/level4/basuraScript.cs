using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class basuraScript : MonoBehaviour
{
    private bool jugadorEnContacto;
    public KeyCode teclaActivacion = KeyCode.Space;
    [SerializeField] private AudioClip SonidoIniciar;

    void Update(){
        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion)){
            activarAnimacion();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            // img_sembrar.SetActive(true);
            jugadorEnContacto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            // img_sembrar.SetActive(false);
            jugadorEnContacto = false;
        }
    }
    public void refrescarItems(){
        GameManager.Instance.SavePlayerPosition(0);
        SceneManager.LoadScene("SceneLevel4");
        GameManager.Instance.LoadPlayerPosition();
    }

    private void activarAnimacion(){
        if (jugadorEnContacto) {
            itemBuyInformation itemBasura = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Basura");
            if (itemBasura != null){
                if (itemBasura.cantidad >=1){
                    IniciarSonido.Instance.ExecuteSound(SonidoIniciar);
                    GameManager.Instance.RestarCantidadPorTitulo(itemBasura.titulo, 1);
                    PlayerPrefs.SetInt("basuraTirada", PlayerPrefs.GetInt("basuraTirada")+1);
                    this.refrescarItems();
                    if(itemBasura.cantidad == 0){
                        GameManager.Instance.startDialogQuestion($"¡Excelente! Ahora dirígete a la tienda y conversa con el mentor", 0.12f);
                    }

                }
            }
        }
    }
}
