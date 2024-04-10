using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plantaMalaLevel3 : MonoBehaviour
{
    private bool jugadorEnContacto; // Variable para rastrear si el jugador está en contacto
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación
    public GameObject Plastico;
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

    private void ActivarPlantarElemento()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (jugadorEnContacto)
        {
            Transform parentTransform = transform.parent;
            itemBuyInformation itemPlastico = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Plastico");
            if (itemPlastico != null && PlayerPrefs.GetInt("picudosAtrapados")==GameManager.Instance.getCantidadParcelas()){
                if (itemPlastico.cantidad >= 1 ){
                    refrescarItems();
                    Destroy(gameObject);
                    GameManager.Instance.RestarCantidadPorTitulo(itemPlastico.titulo, 1);

                    GameObject nuevoElemento = Instantiate(Plastico, transform.position+ new Vector3(0f,  -2f, 0f), Quaternion.identity);
                    if (parentTransform != null){
                        GameManager.Instance.incScore(30);
                        nuevoElemento.transform.SetParent(parentTransform);
                        PlayerPrefs.SetInt("plantasCubiertas", PlayerPrefs.GetInt("plantasCubiertas")+1);
                        if (PlayerPrefs.GetInt("plantasCubiertas")==2){
                            GameManager.Instance.startDialogQuestionChangeScene($"¡Muy bien...! Ahora responderás una serie de preguntas para probar tu conocimiento en el transcurso del nivel 3 'Control de plagas'", 0.12f, "QuestionLevel3");
                        }
                    }
                }else{
                    GameManager.Instance.startDialogQuestion($"No se encontró el item plástico. Lo puedes encontrar en la tienda. Ve y búscalo.",0.05F);
                }
            }else{
                GameManager.Instance.startDialogQuestion($"Elimina primero los picudos para tratar tus plantas.",0.05F);
            }
        }
    }
}
