using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canastaLevel5 : MonoBehaviour
{
    private bool jugadorEnContacto;
    public KeyCode teclaActivacion = KeyCode.Space;
    public bool  canastaLlena = false;
    [SerializeField] private AudioClip SonidoIniciar;
    public itemBuyInformation itemInformation;
    [SerializeField] private GameObject canastaLlenaGameObject;

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
        if (jugadorEnContacto && !canastaLlena){
            Transform parentTransform = transform.parent;
            itemBuyInformation itemRacimo = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Racimo");
            if (itemRacimo != null){
                if (itemRacimo.cantidad >= 1){
                    this.canastaLlena = true;
                    // IniciarSonido.Instance.ExecuteSound(SonidoIniciar);
                   
                    PlayerPrefs.SetInt("canastasLlenas", PlayerPrefs.GetInt("canastasLlenas")+1);
                    GameManager.Instance.RestarCantidadPorTitulo(itemRacimo.titulo, 1);
                    GameManager.Instance.startDialogQuestion($"Racimo puesto en la canasta",0.05F);
                    GameObject nuevoElemento = Instantiate(canastaLlenaGameObject, transform.position+ new Vector3(0f,  0f, 0f), Quaternion.identity);
                    nuevoElemento.transform.SetParent(parentTransform);
                    Destroy(gameObject);

                    if (PlayerPrefs.GetInt("canastasLlenas") == 7){
                     GameManager.Instance.startDialogQuestion($"Muy bien ahora recoge las canastas para enviarlas a distribución",0.05F);
                    }
                    refrescarItems();
                }else{
                    GameManager.Instance.startDialogQuestion($"No se encontró las cantidades necesarias, necesitas 1 Racimo",0.05F);
                }
            }else{
                GameManager.Instance.startDialogQuestion($"No se encontró las cantidades necesarias, necesitas 1 Racimo",0.05F);
            }
        }else{
            if (PlayerPrefs.GetInt("canastasLlenas")>=7){
                PlayerPrefs.SetInt("canastasRecogida", PlayerPrefs.GetInt("canastasRecogida")+1);
                 if (PlayerPrefs.GetInt("canastasRecogida") == 7){
                      GameManager.Instance.startDialogQuestionChangeScene($"¡Muy bien...! Ahora responderás una serie de preguntas para probar tu conocimiento en el transcurso del nivel 5 'Cosecha'", 0.12f, "QuestionLevel5");
                    }
                Destroy(gameObject);
                GameManager.Instance.addBuyItems(itemInformation, 1);
                refrescarItems();
            }else{
                GameManager.Instance.startDialogQuestion($"Canasta llena, por favor llena las otras canastas para continuar",0.05F);
            }
        }
    }
}
