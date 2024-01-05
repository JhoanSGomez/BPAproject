using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantaCortarController : MonoBehaviour
{
    public GameObject elementoReemplazo;
    public Animator animator; // Referencia al componente Animator de tu objeto
    private bool jugadorEnContacto; // Variable para rastrear si el jugador está en contacto
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación
    public int toques; // Referencia al componente Animator de tu objeto
    public int maxToques=3; 
    void Update()
    {
        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion))
        {
            toques= toques+1;
            ActivarAnimacionElemento();
        }
    }

    /*private void  OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            GameManager.Instance.incScore(5);
            GetComponent<SpriteRenderer>().enabled = false;
            Transform parentTransform = transform.parent; // Obtiene el Transform padre
            
            Destroy(gameObject);
            GameObject nuevoElemento = Instantiate(elementoReemplazo, transform.position+ new Vector3(0f, -2, 0f), Quaternion.identity);
            if (parentTransform != null){
                nuevoElemento.transform.SetParent(parentTransform);
            }            
            Debug.Log("Colision con player");
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnContacto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnContacto = false;
            DesactivarAnimacionElemento();
        }
    }

    private void ActivarAnimacionElemento()
    {

        if (toques==maxToques && jugadorEnContacto)
        {
            GameManager.Instance.incScore(5);
            GetComponent<SpriteRenderer>().enabled = false;
            Transform parentTransform = transform.parent; // Obtiene el Transform padre
            Destroy(gameObject);
            GameObject nuevoElemento = Instantiate(elementoReemplazo, transform.position+ new Vector3(0f, -2, 0f), Quaternion.identity);
            if (parentTransform != null){
                nuevoElemento.transform.SetParent(parentTransform);
            }            
        }
    }

    private void DesactivarAnimacionElemento()
    {
        if (animator != null)
        {
           
        }
    }
}