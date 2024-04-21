using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TiendaLevel2 : MonoBehaviour
{
    [SerializeField] List<itemInformation> informacionItems;
    [SerializeField] GameObject plantillaObjetoTienda;
    [SerializeField] TextMeshProUGUI textoMonedasTotales;
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
            plantillaItem.imagen.sprite = item.image;
            plantillaItem.titulo.text = item.titulo;
            plantillaItem.textoPrecio.text = item.precio.ToString();
            plantillaItem.cantidad.text = GameManager.Instance.obtenerCantidadPorTitulo(item.titulo).ToString();

            if(item.titulo == "corta_setos"){

                if (GameManager.Instance.getColinosAbonados()>=GameManager.Instance.getCantidadParcelas())
                {
                    Instantiate(plantillaItem, transform);
                } 
            }else{
                Instantiate(plantillaItem, transform);
            }
        }
    }
 
    void Update()
    {
        textoMonedasTotales.text = PlayerPrefs.GetInt("monedasTotales").ToString();
    }

    public void salirTienda()
    {
        SceneManager.LoadScene("StoreLevel2");
        GameManager.Instance.setScore();
        GameManager.Instance.LoadPlayerPosition();
    }
}
