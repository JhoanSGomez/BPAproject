using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class picudoScript : MonoBehaviour
{
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación

    [SerializeField] private AudioClip SonidoIniciar;

    [SerializeField] private GameObject img_sembrar;
    private string sceneName;

    void Update(){
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            itemBuyInformation itemTrampa = GameManager.Instance.informacionBuyItems.Find(x => x.titulo.Replace(" ", "") == "Trampatipo1");
            if (itemTrampa != null){
                if (itemTrampa.cantidad >= 1){
                    GetComponent<SpriteRenderer>().enabled = false;
                    Destroy(gameObject);
                    GameManager.Instance.incScore(30);
                    GameManager.Instance.RestarCantidadPorTitulo(itemTrampa.titulo, 1);
                    this.refrescarItems();
                }else{
                    GameManager.Instance.incScore(-10);
                    GameManager.Instance.startDialogQuestion($"¡Oh no! Te has encontrado con un picudo que está arruinando tu cultivo. ¡Consigue una trampa en la tienda para atraparlo!",0.05F);
                }
            }else{
                GameManager.Instance.incScore(-10);
                GameManager.Instance.startDialogQuestion($"¡Oh no! Te has encontrado con un picudo que está arruinando tu cultivo. ¡Consigue una trampa en la tienda para atraparlo!",0.05F);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player"))
        {
            // img_sembrar.SetActive(false);
        }
    }

    public void refrescarItems(){
        GameManager.Instance.SavePlayerPosition(0);
        SceneManager.LoadScene("SceneLevel3");
        GameManager.Instance.LoadPlayerPosition();
    }
}
