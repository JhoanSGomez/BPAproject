using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class huecoplanta : MonoBehaviour
{
    private bool jugadorEnContacto; // Variable para rastrear si el jugador está en contacto
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación
    public GameObject colino;
    [SerializeField] private AudioClip SonidoIniciar;
    [SerializeField] private GameObject img_sembrar;

    void Update()
    {
        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion))
        {
            ActivarAnimacionElemento();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            img_sembrar.SetActive(true);

            jugadorEnContacto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            img_sembrar.SetActive(false);

            jugadorEnContacto = false;
        }
    }

    private void ActivarAnimacionElemento()
    {
        if (jugadorEnContacto)
        {
            Transform parentTransform = transform.parent;
            itemBuyInformation itemAbono = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Abono");
            itemBuyInformation itemColino = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Colino");
            
            if (itemAbono != null && itemColino != null){
                if (itemAbono.cantidad >= 1 && itemColino.cantidad >= 1){
                    IniciarSonido.Instance.ExecuteSound(SonidoIniciar);
                    Destroy(gameObject);
                    GameManager.Instance.RestarCantidadPorTitulo(itemAbono.titulo, 1);
                    GameManager.Instance.RestarCantidadPorTitulo(itemColino.titulo, 1);
                    refrescarItems();
                    GameObject nuevoElemento = Instantiate(colino, transform.position+ new Vector3(0f, 1, 0f), Quaternion.identity);
                    if (parentTransform != null){
                        GameManager.Instance.updateColinosPlantados(1);
                        GameManager.Instance.incScore(30);
                        nuevoElemento.transform.SetParent(parentTransform);
                        if( GameManager.Instance.getColinosPlantados()== GameManager.Instance.getCantidadParcelas()){
                            GameManager.Instance.startDialogQuestionChangeScene($"¡Muy bien...! Ahora responderás una serie de preguntas para probar tu conocimiento en el transcurso del nivel 1 'Siembra'", 0.05F, "Question");
                        }
                    }
                }else{
                    GameManager.Instance.startDialogQuestion($"No tiene las cantidades necesarias para sembrar",0.05F);
                }
            }else{
                GameManager.Instance.startDialogQuestion($"Se requiere un colino con su respectivo abono para sembrar",0.05F);
            }
        }
    }

    public void refrescarItems()
    {
        GameManager.Instance.SavePlayerPosition(0);
        SceneManager.LoadScene("SampleScene");
        GameManager.Instance.LoadPlayerPosition();
    }
}
