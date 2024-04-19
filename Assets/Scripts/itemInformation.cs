using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuTienda", menuName = "Plantillas/InfoItemTienda")]
public class itemInformation : ScriptableObject
{
    public string titulo;
    public Sprite image;
    public int precio;
    public int cantidad;
}
