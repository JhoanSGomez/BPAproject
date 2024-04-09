using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class plantaMedia : MonoBehaviour
{
    private bool jugadorEnContacto; // Variable para rastrear si el jugador está en contacto
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación
    public GameObject PlataConFruto;
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
            itemBuyInformation itemTijeras = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "corta_setos");
            if (itemTijeras != null){
                if (itemTijeras.cantidad >= 1){
                    refrescarItems();
                    Destroy(gameObject);
                    GameManager.Instance.RestarCantidadPorTitulo(itemTijeras.titulo, 1);

                    GameObject nuevoElemento = Instantiate(PlataConFruto, transform.position+ new Vector3(0f,  0f, 0f), Quaternion.identity);
                    if (parentTransform != null){
                        GameManager.Instance.incScore(30);
                        GameManager.Instance.updateColinosPodados(1);
                        nuevoElemento.transform.SetParent(parentTransform);
                        if(GameManager.Instance.getColinosPodados()==GameManager.Instance.getCantidadParcelas()){
                            GameManager.Instance.startDialogQuestion("¡Excelente! Ahora regresa a la tienda y compra las bolsas para comenzar el embolsado",0.12f);
                        }
                    }
                }else{
                    GameManager.Instance.startDialogQuestion($"No se encontró las cantidades necesarias, necesitas 1 corta_setos",0.05F);
                }
            }else{
                GameManager.Instance.startDialogQuestion($"No se encontró el item corta_setos ",0.05F);
            }
        }
    }
}
