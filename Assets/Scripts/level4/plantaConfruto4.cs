using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plantaConfruto4 : MonoBehaviour {
 private bool jugadorEnContacto;
    public KeyCode teclaActivacion = KeyCode.Space;
    public GameObject planta;
    [SerializeField] private AudioClip SonidoIniciar;

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
        SceneManager.LoadScene("SceneLevel4");
       GameManager.Instance.LoadPlayerPosition();
    }

    private void ActivarPlantarElemento(){
        sceneName = SceneManager.GetActiveScene().name;
        if (jugadorEnContacto && sceneName == "SceneLevel4"){
            Transform parentTransform = transform.parent;
            itemBuyInformation itemFertilizante =GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Abono");
            if (itemFertilizante != null){
                if (itemFertilizante.cantidad >= 1){
                    IniciarSonido.Instance.ExecuteSound(SonidoIniciar);
                    Destroy(gameObject);
                    GameManager.Instance.RestarCantidadPorTitulo(itemFertilizante.titulo, 1);
                    refrescarItems();
                    GameObject nuevoElemento = Instantiate(planta, transform.position+ new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                    if (parentTransform != null){
                       GameManager.Instance.incScore(30);
                        nuevoElemento.transform.SetParent(parentTransform);
                        PlayerPrefs.SetInt("plantasAbanodasLevel4", PlayerPrefs.GetInt("plantasAbanodasLevel4")+1);
                        if (PlayerPrefs.GetInt("plantasAbanodasLevel4")==7){
                            GameManager.Instance.startDialogQuestionChangeScene($"¡Muy bien...! Ahora responderás una serie de preguntas para probar tu conocimiento en el transcurso del nivel 4 'Control de residuos'", 0.12f, "QuestionLevel4");
                        }
                    }
                }else{
                    GameManager.Instance.startDialogQuestion($"No se encontró las cantidades necesarias, necesitas 1 Fertilizante",0.05F);
                }
            }else{
                GameManager.Instance.startDialogQuestion($"No se encontró uno de los items Fertilizante",0.05F);
            }
        }
    }
}