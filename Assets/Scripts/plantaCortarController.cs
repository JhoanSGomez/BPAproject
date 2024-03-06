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
    public int maxToques = 3;
    [SerializeField] private AudioClip SonidoIniciar;
    [SerializeField] private GameObject hacha;

    void Update()
    {
        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion))
        {
            toques= toques+1;
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
            if (toques==maxToques && jugadorEnContacto)
            {
                GameManager.Instance.RestarCantidadPorTitulo("Hacha", 1);
                GameManager.Instance.refrescarItems();
                GameManager.Instance.incScore(50);
                GetComponent<SpriteRenderer>().enabled = false;
                Transform parentTransform = transform.parent; // Obtiene el Transform padre
                Destroy(gameObject);

                GameObject nuevoElemento = Instantiate(elementoReemplazo, transform.position+ new Vector3(0f, -2, 0f), Quaternion.identity);
                if (parentTransform != null){
                    nuevoElemento.transform.SetParent(parentTransform);
                }            
            
            }
        }else{
            Debug.Log($"No se encontro el item Hacha");
        }
    }
}