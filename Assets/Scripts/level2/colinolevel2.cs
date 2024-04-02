using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class colinolevel2 : MonoBehaviour
{
    private bool jugadorEnContacto; // Variable para rastrear si el jugador está en contacto
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación
    public GameObject colinoMediano;
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
            itemBuyInformation itemFertilizante = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Fertilizante");
            itemBuyInformation itemCubeta = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Cubeta");
            if (itemFertilizante != null && itemCubeta != null){
                if (itemFertilizante.cantidad >= 1 && itemCubeta.cantidad >= 1){
                    // IniciarSonido.Instance.ExecuteSound(SonidoIniciar);
                    Destroy(gameObject);
                    GameManager.Instance.RestarCantidadPorTitulo(itemFertilizante.titulo, 1);
                    GameManager.Instance.RestarCantidadPorTitulo(itemCubeta.titulo, 1);
                    refrescarItems();
                    GameObject nuevoElemento = Instantiate(colinoMediano, transform.position+ new Vector3(0f, 1, 0f), Quaternion.identity);
                    if (parentTransform != null){
                        GameManager.Instance.updateColinosAbonados(1);
                        GameManager.Instance.incScore(30);
                        nuevoElemento.transform.SetParent(parentTransform);
                        if(GameManager.Instance.getColinosAbonados()==GameManager.Instance.getCantidadParcelas()){
                            GameManager.Instance.startDialogQuestion($"¡Muy bien...! Ahora puedes  encontrar en la tienda un elemento nuevo para podar tu plantas", 0.05F);
                            //Debug.Log($"entre colinos abonados  3 ");
                        }
                    }
                }else{
                    GameManager.Instance.startDialogQuestion($"No se encontro las cantidades necesarias necesitas 1 Fertilizante y 1 Cubeta con agua",0.05F);
                    // Debug.Log($"No se encontro las cantidades necesarias level 2");
                }
            }else{
                GameManager.Instance.startDialogQuestion($"No se encontro uno de los items Fertilizante y Cubeta con agua",0.05F);
                //Debug.Log($"No se encontro uno de los items level 2 ");
            }
        }
    }
}
