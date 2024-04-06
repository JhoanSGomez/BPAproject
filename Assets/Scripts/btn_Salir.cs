using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btn_Salir : MonoBehaviour
{

   [SerializeField] public string scene;
   [SerializeField] public string mensaje;

   public void goLevel()
    {
        GameManager.Instance.resetinformacionBuyItems();
        GameManager.Instance.ResetPlayerPosition();
        SceneManager.LoadScene(scene);
        GameManager.Instance.startDialogQuestion(mensaje, 0.08f);

        GameManager.Instance.LoadPlayerPosition();
    }
}
