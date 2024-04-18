using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tienda : MonoBehaviour
{
    [SerializeField] List<itemInformation> informacionItems;
    [SerializeField] GameObject plantillaObjetoTienda;
    [SerializeField] TextMeshProUGUI textoMonedasTotales;
    [SerializeField] public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("monedasTotales"))
        {
            PlayerPrefs.SetInt("monedasTotales", 900);
        }
        var plantillaItem = plantillaObjetoTienda.GetComponent<PlantillaItemTienda>();
 
        foreach (var item in informacionItems)
        {
            
            // plantillaItem.imagen.sprite = item.image;
            // plantillaItem.titulo.text = item.titulo;
            // plantillaItem.textoPrecio.text = item.precio.ToString();
 
            // Instantiate(plantillaItem, transform);
            plantillaItem.imagen.sprite = item.image;
            plantillaItem.titulo.text = item.titulo;
            plantillaItem.textoPrecio.text = item.precio.ToString();
            plantillaItem.cantidad.text = GameManager.Instance.obtenerCantidadPorTitulo(item.titulo).ToString();

            if(item.titulo == "Hacha" &&  GameManager.Instance.getHachasCompradas()==2){

            }else{
                Instantiate(plantillaItem, transform);
            }
        }
    }
 
    // Update is called once per frame
    void Update()
    {
        textoMonedasTotales.text = PlayerPrefs.GetInt("monedasTotales").ToString();
    }

    public void salirTienda()
    {
        SceneManager.LoadScene(sceneName); //"Store"
        GameManager.Instance.setScore();
        GameManager.Instance.LoadPlayerPosition();
    }
}
