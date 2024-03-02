using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjetosComprados : MonoBehaviour
{
    [SerializeField] GameObject plantillaItemsComprados;
    // Start is called before the first frame update
    void Start()
    {
        var plantillaItem = plantillaItemsComprados.GetComponent<PlantillaItemsComprados>();
 

        foreach (var item in GameManager.Instance.testlist())
        {
            plantillaItem.imagen.sprite = item.image;
            plantillaItem.cantidad.text = item.cantidad.ToString();;

            //plantillaItemsComprados.cantidad.text = item.cantidad.ToString();
 
            Instantiate(plantillaItem, transform);
        }
    }   
}
