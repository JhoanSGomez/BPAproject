using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plantaConFruto : MonoBehaviour
{
    private bool jugadorEnContacto; // Variable para rastrear si el jugador está en contacto
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación
    public GameObject plantaConFruto2;

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
        SceneManager.LoadScene("SceneLevel2");
        GameManager.Instance.LoadPlayerPosition();
    }

    private void ActivarPlantarElemento()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (jugadorEnContacto && sceneName == "SceneLevel2")
        {
            Transform parentTransform = transform.parent;
            itemBuyInformation itemBolsa = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Bolsa");
            if (itemBolsa != null){
                if (itemBolsa.cantidad >= 1){
                    // IniciarSonido.Instance.ExecuteSound(SonidoIniciar);
                    Destroy(gameObject);
                    GameManager.Instance.RestarCantidadPorTitulo(itemBolsa.titulo, 1);
                    refrescarItems();
                    GameObject nuevoElemento = Instantiate(plantaConFruto2, transform.position+ new Vector3(0f, 0f, 0f), Quaternion.identity);
                    if (parentTransform != null){
                        GameManager.Instance.updateColinosEmbolsados(1);
                        GameManager.Instance.incScore(30);
                        nuevoElemento.transform.SetParent(parentTransform);
                        if(GameManager.Instance.getColinosEmbolsados()==GameManager.Instance.getCantidadParcelas()){
                            GameManager.Instance.startDialogQuestionChangeScene($"¡Muy bien...! Ahora responderás una serie de preguntas para probar tu conocimiento en el transcurso del nivel 2 'Mantenimiento del cultivo'", 0.05F, "QuestionLevel2");
                        }
                    }
                }else{
                    GameManager.Instance.startDialogQuestion($"No se encontro las cantidades necesarias necesitas 1 Bolsa",0.05F);
                }
            }else{
                GameManager.Instance.startDialogQuestion($"No se encontro uno de los items Bolsa",0.05F);
            }
        }
    }
}
