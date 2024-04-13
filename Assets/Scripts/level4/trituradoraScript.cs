using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trituradoraScript : MonoBehaviour
{
    private bool jugadorEnContacto;
    public KeyCode teclaActivacion = KeyCode.Space;
    public itemBuyInformation itemInformationAbono;

    void Update(){
        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion)){
            ActivarPlantarElemento();
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

    private void ActivarPlantarElemento(){
        if (jugadorEnContacto){
            itemBuyInformation itemHoja = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Hoja");
            itemBuyInformation itemTronco = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Tronco");
            if (itemHoja != null && itemTronco != null){
                if (itemHoja.cantidad >= 1 && itemTronco.cantidad >= 1){
                    GameManager.Instance.RestarCantidadPorTitulo(itemHoja.titulo, 1);
                    GameManager.Instance.RestarCantidadPorTitulo(itemTronco.titulo, 1);
                    GameManager.Instance.addBuyItems(itemInformationAbono, 2);
                    refrescarItems();
                }else{
                    // GameManager.Instance.startDialogQuestion($"No se encontró las cantidades necesarias, necesitas 1 Fertilizante y 1 Cubeta con agua",0.05F);
                }
            }else{
                // GameManager.Instance.startDialogQuestion($"No se encontró uno de los items Fertilizante y Cubeta con agua",0.05F);
            }
        }
    }
}
