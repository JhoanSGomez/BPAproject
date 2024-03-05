using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarSonido : MonoBehaviour
{
    public static IniciarSonido Instance;
    private AudioSource audioSource;

     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        audioSource = GetComponent<AudioSource>();
    }

    public void ExecuteSound(AudioClip sound)
    {

        audioSource.PlayOneShot(sound);
    }
}

