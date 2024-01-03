using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantaCortarController : MonoBehaviour
{
    public GameObject elementoReemplazo;

    private void  OnTriggerEnter2D(Collider2D collision)
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
    }
}
