using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plantaconFruto5 : MonoBehaviour
{
    private bool jugadorEnContacto;
    public KeyCode teclaActivacion = KeyCode.Space;
    public GameObject planta;
    [SerializeField] private AudioClip SonidoIniciar;
    public itemBuyInformation itemInformation;

    [SerializeField] private GameObject img_sembrar;
    private string sceneName;

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
        SceneManager.LoadScene("SceneLevel5");
       GameManager.Instance.LoadPlayerPosition();
    }

    private void ActivarPlantarElemento(){
        sceneName = SceneManager.GetActiveScene().name;
        if (jugadorEnContacto && sceneName == "SceneLevel5"){ //"
            Transform parentTransform = transform.parent;
            itemBuyInformation itemMachete = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Machete");
            if (itemMachete != null){
                if (itemMachete.cantidad >= 1){
                    // IniciarSonido.Instance.ExecuteSound(SonidoIniciar);
                    Destroy(gameObject);
                    GameManager.Instance.RestarCantidadPorTitulo(itemMachete.titulo, 1);
                    GameManager.Instance.addBuyItems(itemInformation, 1);
                    refrescarItems();
                    PlayerPrefs.SetInt("racimosCortados", PlayerPrefs.GetInt("racimosCortados")+1);
                    if (PlayerPrefs.GetInt("racimosCortados")==7){
                    GameManager.Instance.startDialogQuestion($"¡Muy bien...! Ahora, localiza las canastas y deposita los racimos que has recolectado.", 0.12f);
                    }
                }else{
                    GameManager.Instance.startDialogQuestion($"No se encontró las cantidades necesarias, necesitas 1 Machete",0.05F);
                }
            }else{
                GameManager.Instance.startDialogQuestion($"No se encontró uno de los items Machete",0.05F);
            }
        }
    }
}
