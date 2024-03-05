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
    public GridLayoutGroup gridLayout;
    [SerializeField] GameObject plantillaItemsComprados;
    [SerializeField] private AudioClip SonidoIniciar;
    public List<GameObject> itemList = new List<GameObject>();

    [SerializeField] private GameObject dialogMark;

    void Update()
    {

        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion))
        {
            ActivarAnimacionElemento();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hice colicion OnTriggerEnter2D");

        if (collision.CompareTag("Player"))
        {
            dialogMark.SetActive(true);

            jugadorEnContacto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("hice colicion OnTriggerExit2D");
        if (collision.CompareTag("Player"))
        {
            dialogMark.SetActive(false);

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
                        nuevoElemento.transform.SetParent(parentTransform);
                    }
                }else{
                    Debug.Log($"No se encontro las cantidades necesarias");
                }
            }else{
                Debug.Log($"No se encontro uno de los items");
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
