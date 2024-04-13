using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ControlTextosLevel5 : MonoBehaviour
{
    [SerializeField] PlantillaTextos plantilla;
    [SerializeField] PlantillaTextos[] arrayPlantillas;

    [SerializeField] TextMeshProUGUI textoNarracion;
    [SerializeField] TextMeshProUGUI textoRespuestaUno;
    [SerializeField] TextMeshProUGUI textoRespuestaDos;
    [SerializeField] TextMeshProUGUI textoRespuestaTres;
    [SerializeField] TextMeshProUGUI textoRespuestaCuatro;
    [SerializeField] TextMeshProUGUI textoRespuestaCinco;
    [SerializeField] TextMeshProUGUI textoRespuestaSeis;
    [SerializeField] TextMeshProUGUI btnBack;

    [SerializeField] GameObject[] arrayBotones;

    public Button miBoton;


    // Start is called before the first frame update
    private void Start()
    {
        plantilla = arrayPlantillas[57];
        muestraTexto();
    }

    void muestraTexto()
    {
        textoNarracion.text = plantilla.textoNarrativo;
        textoRespuestaUno.text = plantilla.respuestaUno;
        textoRespuestaDos.text = plantilla.respuestaDos;
        textoRespuestaTres.text = plantilla.respuestaTres;
        textoRespuestaCuatro.text = plantilla.respuestaCuatro;
        textoRespuestaCinco.text = plantilla.respuestaCinco;
        textoRespuestaSeis.text = plantilla.respuestaSeis;
        btnBack.text = plantilla.btnBack;
        validarBoton();
    }
    public void validarBoton()
    {
        if (textoRespuestaUno.text == "")
        {
            miBoton = arrayBotones[0].GetComponent<Button>();
            miBoton.interactable = false;
        }
        else
        {
            miBoton = arrayBotones[0].GetComponent<Button>();
            miBoton.interactable = true;
        }
        if (textoRespuestaDos.text == "")
        {
            miBoton = arrayBotones[1].GetComponent<Button>();
            miBoton.interactable = false;
        }
        else
        {
            miBoton = arrayBotones[1].GetComponent<Button>();
            miBoton.interactable = true;
        }
        if (textoRespuestaTres.text == "")
        {
            miBoton = arrayBotones[2].GetComponent<Button>();
            miBoton.interactable = false;
        }
        else
        {
            miBoton = arrayBotones[2].GetComponent<Button>();
            miBoton.interactable = true;
        }
        if (textoRespuestaCuatro.text == "")
        {
            miBoton = arrayBotones[3].GetComponent<Button>();
            miBoton.interactable = false;
        }
        else
        {
            miBoton = arrayBotones[3].GetComponent<Button>();
            miBoton.interactable = true;
        }
        if (textoRespuestaCinco.text == "")
        {
            miBoton = arrayBotones[4].GetComponent<Button>();
            miBoton.interactable = false;
        }
        else
        {
            miBoton = arrayBotones[4].GetComponent<Button>();
            miBoton.interactable = true;
        }
        if (textoRespuestaSeis.text == "")
        {
            miBoton = arrayBotones[5].GetComponent<Button>();
            miBoton.interactable = false;
        }
        else
        {
            miBoton = arrayBotones[5].GetComponent<Button>();
            miBoton.interactable = true;
        }
        if (btnBack.text == "")
        {
            miBoton = arrayBotones[6].GetComponent<Button>();
            miBoton.interactable = false;
        }
        else
        {
            miBoton = arrayBotones[6].GetComponent<Button>();
            miBoton.interactable = true;
        }
    }

    public void controlBotones(int indice)
    {
        plantilla = arrayPlantillas[plantilla.arrayReferencias[indice]];
        muestraTexto();

    }

    public void backScene(int indice)
    {
        if (btnBack.text == "Inicio" && indice != 0)
        {
            SceneManager.LoadScene("StoreLevel5");
        }
    }
}
