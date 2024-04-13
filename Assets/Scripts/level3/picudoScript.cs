using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class picudoScript : MonoBehaviour
{
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación

    [SerializeField] private AudioClip SonidoIniciarTrampa;
    [SerializeField] private AudioClip SonidoIniciarColision;
    [SerializeField] private GameObject img_sembrar;
    private string sceneName;

    void Update(){
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            itemBuyInformation itemTrampa = GameManager.Instance.informacionBuyItems.Find(x => x.titulo.Replace(" ", "") == "TrampaDisco");
            if (itemTrampa != null){
                if (itemTrampa.cantidad >= 1){
                    GetComponent<SpriteRenderer>().enabled = false;
                    IniciarSonido.Instance.ExecuteSound(SonidoIniciarTrampa);
                    Destroy(gameObject);
                    GameManager.Instance.incScore(30);
                    GameManager.Instance.RestarCantidadPorTitulo(itemTrampa.titulo, 1);
                    this.refrescarItems();
                    
                    PlayerPrefs.SetInt("picudosAtrapados", PlayerPrefs.GetInt("picudosAtrapados")+1);
                    if (PlayerPrefs.GetInt("picudosAtrapados")==8){
                        GameManager.Instance.startDialogQuestion($"¡Muy bien! Ahora que has atrapado todas las plagas, ve a la tienda y compra plástico transparente. Algunas plantas de plátano han sido afectadas por la enfermedad del moko. Como buena práctica para manejar esta enfermedad, se debe erradicar la planta afectada. Luego, se recomienda solarizar el suelo cubriéndolo con plástico transparente durante un mes.",0.10F);
                    }
                }else{
                    IniciarSonido.Instance.ExecuteSound(SonidoIniciarColision);
                    GameManager.Instance.incScore(-10);
                    GameManager.Instance.startDialogQuestion($"¡Oh no! Te has encontrado con un picudo que está arruinando tu cultivo. ¡Consigue una trampa en la tienda para atraparlo!",0.05F);
                }
            }else{
                IniciarSonido.Instance.ExecuteSound(SonidoIniciarColision);
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
