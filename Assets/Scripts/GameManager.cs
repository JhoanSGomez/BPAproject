using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public List<itemBuyInformation> informacionBuyItems;
    private TMP_Text scoreText;
    private bool juegoIniciado = false;
    public int score;
    private int nApples;
    public GameObject plantaMala;
    public Transform cultivo;
    private List<GameObject> plantInstances; 
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.Log("Game Manager is null!!");

            return _instance;
        }
    }

    void Awake()
    {
        //score = 0;

        if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{

            _instance = this;
            DontDestroyOnLoad(this.gameObject); // Evita que se destruya al cambiar de escena
            DontDestroyOnLoad(this.cultivo);
            plantInstances = new List<GameObject>(); // Inicializa la lista en el Awake
            GeneratePlantInstances(); // Genera las instancias al inicio
        
        }
        
    }

    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public List<itemBuyInformation> testlist(){
        return informacionBuyItems;
    }
    

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(juegoIniciado){
            if(scene.name!="Store"){
                GameManager.Instance.LoadPlayerPosition();

            }
             
        }else{
            PlayerPrefs.DeleteAll();
            PlayerPrefs.DeleteKey("monedasTotales");

            //PlayerPrefs.DeleteKey("PlayerPosX");
        }
        setScore();
        nApples = GameObject.FindGameObjectsWithTag("Tree").Length;
        juegoIniciado = true;
    }

    public void LoadPlayerPosition(){
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY")){
            float posX = PlayerPrefs.GetFloat("PlayerPosX");
            float posY = PlayerPrefs.GetFloat("PlayerPosY") -1;
            float posZ = PlayerPrefs.GetFloat("PlayerPosZ");
            Vector3 playerPosition = new Vector3(posX, posY, posZ);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null){
                player.transform.position = playerPosition;
            }else{
                 Debug.Log("Player is null!!");
            }
        }
    }

    public void SavePlayerPosition(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
            Vector3 playerPosition = player.transform.position;
            PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);
        }
    }

    public void incScore(int inc)
    {
        GameObject go = GameObject.FindWithTag("score");
        scoreText = go.GetComponent<TMP_Text>();
        score += inc;
        PlayerPrefs.SetInt("monedasTotales", score);
        scoreText.text = "Score: " + score.ToString();
    }

    public void setScore()
    {
        GameObject go = GameObject.FindWithTag("score");
        //PlayerPrefs.SetInt("monedasTotales", score);
        scoreText = go.GetComponent<TMP_Text>();
        score = PlayerPrefs.GetInt("monedasTotales");
        Debug.Log("Contenido setScore: "+PlayerPrefs.GetInt("monedasTotales"));
        scoreText.text = "Score: " +  PlayerPrefs.GetInt("monedasTotales").ToString();
    }

     void GeneratePlantInstances()
    {
        int numberOfInstances = 3;
        Vector3 initialPositionRow1 = new Vector3(-23, 10, 0f);
        Vector3 initialPositionRow2 = new Vector3(-23, 4, 0f);
        Vector3 initialPositionRow3 = new Vector3(-23, -2, 0f);

        for (int i = 0; i < numberOfInstances; i++){
            Vector3 spawnPositionRow1 = initialPositionRow1 + new Vector3(i*5, 0, 0f);
            GameObject newPlantInstanceRow1 = Instantiate(plantaMala, spawnPositionRow1, Quaternion.identity,cultivo);
            Vector3 spawnPositionRow2 = initialPositionRow2 + new Vector3(i*5, 0, 0f);
            GameObject newPlantInstanceRow2 = Instantiate(plantaMala, spawnPositionRow2, Quaternion.identity,cultivo);
            Vector3 spawnPositionRow3 = initialPositionRow3 + new Vector3(i*5, 0, 0f); // Cambia 2 por el espacio deseado entre instancias
            GameObject newPlantInstanceRow3 = Instantiate(plantaMala, spawnPositionRow3, Quaternion.identity,cultivo);
            List<GameObject> plantInstances = new List<GameObject>();
            plantInstances.Add(newPlantInstanceRow1);
            plantInstances.Add(newPlantInstanceRow2);
            plantInstances.Add(newPlantInstanceRow3);
        }
    }

    public void addBuyItems(itemBuyInformation item){
        // Buscar el item por título


        itemBuyInformation itemExistente = informacionBuyItems.Find(x => x.titulo == item.titulo);

        if (itemExistente == null){

            // Si no existe, agregar el nuevo item a la lista
            item.cantidad = 1;
            informacionBuyItems.Add(item);
        }else{
            // Si existe, incrementar la cantidad
            itemExistente.cantidad += 1;
        }
        ImprimirLista();

    }

    private void ImprimirLista()
    {
        Debug.Log("Contenido de la lista:");

        foreach (var item in informacionBuyItems)
        {
            Debug.Log($"Título: {item.titulo}, Cantidad:{item.cantidad}");
        }
    }

}
