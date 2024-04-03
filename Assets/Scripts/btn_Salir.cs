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
        GameManager.Instance.LoadPlayerPosition();
    }
}
