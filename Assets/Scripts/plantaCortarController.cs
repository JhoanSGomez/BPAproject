using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantaCortarController : MonoBehaviour
{
    public GameObject elementoReemplazo;
    public AnimationClip animacion; // Arrastra tu animación aquí desde el Inspector
    private bool jugadorEnContacto; // Variable para rastrear si el jugador está en contacto
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación
    public int toques; // Referencia al componente Animator de tu objeto
    public int plantaCortada;
    public int maxToques = 3;
    [SerializeField] private AudioClip SonidoIniciar;
    [SerializeField] private GameObject hacha;

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
            hacha.SetActive(true);
            jugadorEnContacto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hacha.SetActive(false);
            jugadorEnContacto = false;
        }
    }

    private void ActivarAnimacionElemento()
    {
        Animation prefabAnimation = gameObject.GetComponent<Animation>();
        prefabAnimation.AddClip(animacion, animacion.name);
            
        prefabAnimation.Play(animacion.name);

        IniciarSonido.Instance.ExecuteSound(SonidoIniciar);
        itemBuyInformation itemHacha = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Hacha");
        if (itemHacha.cantidad >= 1){
            toques= toques+1;

            if (toques==maxToques && jugadorEnContacto)
            {
                GameManager.Instance.RestarCantidadPorTitulo("Hacha", 1);
                GameManager.Instance.refrescarItems();
                GameManager.Instance.incScore(50);
                GetComponent<SpriteRenderer>().enabled = false;
                Transform parentTransform = transform.parent; // Obtiene el Transform padre

                GameObject nuevoElemento = Instantiate(elementoReemplazo, transform.position+ new Vector3(0f, -2, 0f), Quaternion.identity);
                if (parentTransform != null){
                    Destroy(gameObject);
                    nuevoElemento.transform.SetParent(parentTransform);
                    plantaCortada= plantaCortada+1;
                    Debug.Log("plantaCortada: "+ plantaCortada);
                    if(plantaCortada == 9){
                        GameManager.Instance.startDialogQuestion("Muy bien ..! Ahora, regresa a la tienda y compra 9 abonos y 9 colinos para sembrar.",0.08f);
                    }
                }            
            
            }
        }else{
            GameManager.Instance.startDialogQuestion("Ya no tienes mas hachas para cortar, dirigite a la tienda y compra 2 hachas que te faltan para terminar de despejar tu terreno",0.08f);
            Debug.Log($"No se encontro el item Hacha");
        }
    }
}