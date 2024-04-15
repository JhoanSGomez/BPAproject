using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnIniciar : MonoBehaviour
{
      [SerializeField] public string scene;

   public void goLevel()
    {
     SceneManager.LoadScene(scene);
    }
}

