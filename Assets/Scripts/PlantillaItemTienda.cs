using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantillaItemTienda : MonoBehaviour
{
    public Image imagen;
    public TextMeshProUGUI textoPrecio;
    public TextMeshProUGUI titulo;
    public Button botonComprar;
    int precio;
    int monedaTotales;
    // Start is called before the first frame update
    void Start()
    {
        precio = int.Parse(textoPrecio.text);
    }
 
    // Update is called once per frame
    void Update()
    {
        monedaTotales = PlayerPrefs.GetInt("monedasTotales");
        if (precio > monedaTotales)
        {
            botonComprar.interactable = false;
        }
    }
   public void Comprar()
    {
        //itemBuyInformation newItem = new itemBuyInformation();
        itemBuyInformation newItem = ScriptableObject.CreateInstance<itemBuyInformation>();
        
        // Obtén el texto del objeto titulo y asígnalo al nuevo item
        newItem.titulo = titulo.text;

        newItem.image = imagen.sprite;

        GameManager.Instance.addBuyItems(newItem);
        monedaTotales -= precio;
        PlayerPrefs.SetInt("monedasTotales", monedaTotales);
    }
}
