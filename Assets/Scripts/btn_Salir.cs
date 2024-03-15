using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btn_Salir : MonoBehaviour
{
   public void goLevel()
    {
        SceneManager.LoadScene("Store");
    }
}
