using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btn_Salir : MonoBehaviour
{

   [SerializeField] public string scene;

   public void goLevel()
    {
        GameManager.Instance.resetinformacionBuyItems();
        GameManager.Instance.ResetPlayerPosition();
        SceneManager.LoadScene(scene);
        GameManager.Instance.startDialogQuestion("¡Bienvenido al nivel 2! Para comenzar, Necesitaras  buscar agua para regar tus colinos busca un pozo cerca alli podras encontra agua ",0.08f);

        GameManager.Instance.LoadPlayerPosition();
    }
}
