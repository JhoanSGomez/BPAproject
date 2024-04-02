using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public List<itemBuyInformation> informacionBuyItems;
    public itemBuyInformation hachaBuyItems;
    private TMP_Text scoreText;
    private bool juegoIniciado = false;
    public int score;
    public int colinosPlantados;
    public int colinosAbonados;
    public int colinosEmbolsados;
    [SerializeField] public GameObject plantaMala;
    public Transform cultivo;
    private List<GameObject> plantInstances; 


    //*************************************************

    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private GameObject dialogTextAux;
    [SerializeField, TextArea(4, 6)] private string[] dialogLines;
    private bool didDialogStart= false;
    private int lineIndex;
    public Canvas canvas; 

    //*************************************************

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.Log("Game Manager is null!!");

            return _instance;
        }
    }
    //escena de preguntas
    //*****************************************

    void Awake()
    {
        //score = 0;

        if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            this.addBuyItems(hachaBuyItems, 7);
            _instance = this;
            DontDestroyOnLoad(this.gameObject); // Evita que se destruya al cambiar de escena
            DontDestroyOnLoad(this.cultivo);
            DontDestroyOnLoad(this.canvas);
            plantInstances = new List<GameObject>(); // Inicializa la lista en el Awake
            //GeneratePlantInstances(); // Genera las instancias al inicio
            Scene scene = SceneManager.GetSceneByName("SampleScene");
            if (scene == SceneManager.GetActiveScene()){
                GeneratePlantInstances();
            }
        }
        
    }

     void Start(){
        this.startDialogQuestion("¡Bienvenido! Para comenzar, dirígete al letrero que se encuentra a tu izquierda y presiona la tecla ESPACIO para interactuar ",0.08f);
    }

    //metodos para el dialogo y poder cambiar a escena de preguntas
    public void startDialogQuestion(string texto,float typingTime)
    {
        if (!didDialogStart)
        {
            startDialog(texto,typingTime);
        }
    }

     private void startDialog(string texto, float typingTime)
    {
        didDialogStart = true;
        dialogPanel.SetActive(true);
        dialogTextAux.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 1f;
        StartCoroutine(ShowLine(texto,typingTime));
    }

    private void NextDialogLine(string texto, float typingTime)
    {
        lineIndex++;
        if (lineIndex < dialogLines.Length)
        {
            StartCoroutine(ShowLine(texto,typingTime));
        }
        else
        {
            didDialogStart = false;
            dialogTextAux.SetActive(false);
            dialogPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

   private IEnumerator ShowLine(string texto,float typingTime)
{
    dialogText.text = string.Empty;

    foreach (char ch in texto)
    {
        dialogText.text += ch;
        yield return new WaitForSecondsRealtime(typingTime);
    }
    dialogTextAux.SetActive(false);
    dialogPanel.SetActive(false);
    didDialogStart = false;
}

    //*******************************************************************************

    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public List<itemBuyInformation> testlist(){
        return informacionBuyItems;
    }
    public int getColinosPlantados(){
        return this.colinosPlantados;
    }
    public int getColinosAbonados(){
        return this.colinosAbonados;
    }
    public int getColinosEmbolsados(){
        return this.colinosEmbolsados;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(juegoIniciado){
            if(scene.name!="Store"){
                GameManager.Instance.LoadPlayerPosition();
            }   
        }else{
            PlayerPrefs.DeleteAll();
            PlayerPrefs.DeleteKey("monedasTotales");
        }
        setScore();
        juegoIniciado = true;
    }

    public void LoadPlayerPosition(){
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY")){
            float posX = PlayerPrefs.GetFloat("PlayerPosX");
            float posY = PlayerPrefs.GetFloat("PlayerPosY");
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

    public void SavePlayerPosition(int inc){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
            Vector3 playerPosition = player.transform.position;
            PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y-inc);
            PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);
        }
    }
    public void ResetPlayerPosition(){
        Debug.Log("pase ṕor aqui ");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
                        Debug.Log("pase ṕor aqui  condicion");

            Vector3 playerPosition = player.transform.position;
            PlayerPrefs.SetFloat("PlayerPosX", 0);
            PlayerPrefs.SetFloat("PlayerPosY", 0);
            PlayerPrefs.SetFloat("PlayerPosZ", 0);
        }
    }
    public void ResetPlayerPositionFixed(float posx,float posy){

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
            Vector3 playerPosition = player.transform.position;
            PlayerPrefs.SetFloat("PlayerPosX", posx);
            PlayerPrefs.SetFloat("PlayerPosY", posy);
            PlayerPrefs.SetFloat("PlayerPosZ", 0);
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

    public void addBuyItems(itemBuyInformation item,int cantidad){
        // Buscar el item por título


        itemBuyInformation itemExistente = informacionBuyItems.Find(x => x.titulo == item.titulo);

        if (itemExistente == null){
            item.cantidad = cantidad;
            informacionBuyItems.Add(item);
        }else{
            // Si existe, incrementar la cantidad
            itemExistente.cantidad += cantidad;
        }
        ImprimirLista();

    }

    public void RestarCantidadPorTitulo(string titulo, int cantidadARestar)
    {
        int index = informacionBuyItems.FindIndex(x => x.titulo == titulo);
        if(index>=0){
            informacionBuyItems[index].cantidad -= cantidadARestar;
        }else{
            Debug.Log("No se encontró ningún elemento con el título '" + titulo + "'.");
        }
    }

    private void ImprimirLista()
    {
        Debug.Log("Contenido de la lista:");

        foreach (var item in informacionBuyItems)
        {
            Debug.Log($"Título: {item.titulo}, Cantidad:{item.cantidad}");
        }
    }
    public void refrescarItems()
    {
        this.SavePlayerPosition(0);
        SceneManager.LoadScene("SampleScene");
        this.LoadPlayerPosition();
    }
    public void updateColinosPlantados(int cantidad)
    {
        this.colinosPlantados = this.colinosPlantados+cantidad;
    }

    public void updateColinosAbonados(int cantidad)
    {
        this.colinosAbonados = this.colinosAbonados+cantidad;
    }
    public void updateColinosEmbolsados(int cantidad)
    {
        this.colinosEmbolsados = this.colinosEmbolsados+cantidad;
    }

    public void resetinformacionBuyItems()
    {
        this.informacionBuyItems = new List<itemBuyInformation>();
    }
}
