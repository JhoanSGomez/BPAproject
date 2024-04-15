using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class residuosScript : MonoBehaviour
{
    private bool jugadorEnContacto;
    public KeyCode teclaActivacion = KeyCode.Space;
    public itemBuyInformation itemInformation;
    [SerializeField] private AudioClip SonidoIniciar;

     void Update()
    {
        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion))
        {
            activarAnimacion();
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
        SceneManager.LoadScene("SceneLevel4");
        GameManager.Instance.LoadPlayerPosition();
    }

    private void activarAnimacion(){
        if (jugadorEnContacto){
            Transform parentTransform = transform.parent;
            IniciarSonido.Instance.ExecuteSound(SonidoIniciar);
            Destroy(gameObject);
            GameManager.Instance.addBuyItems(itemInformation, 1);
            PlayerPrefs.SetInt("residuosRecogidos", PlayerPrefs.GetInt("residuosRecogidos")+1);
            if (PlayerPrefs.GetInt("residuosRecogidos")==12){
                GameManager.Instance.startDialogQuestion($"¡Muy bien...! Sal de tu cultivo y encontrarás un bote de basura donde podrás desechar los residuos no reutilizables, como el plástico", 0.12f);
            }
            refrescarItems();
        }
    }
}
