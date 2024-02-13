using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuBuyTienda", menuName = "Plantillas/InfoItemBuyTienda")]

public class itemBuyInformation : ScriptableObject
{
    public string titulo;
    public Sprite image;
    public int cantidad;
}
