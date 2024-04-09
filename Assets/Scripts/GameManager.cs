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
    public int colinosPodados;
    [SerializeField] public GameObject plantaMala;
    public Transform cultivo;
    private List<GameObject> plantInstances; 
    private int hachasCompradas;
    public int cantidadParcelas;


    //*************************************************

    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private GameObject dialogTextAux;
    [SerializeField, TextArea(4, 6)] private string[] dialogLines;
    private bool didDialogStart= false;
    private int lineIndex;
    public Canvas canvas; 

    //*************************************************

    public Transform picudos;
    [SerializeField] public GameObject picudorayado;
    [SerializeField] public GameObject Picudoamarillo;
    [SerializeField] public GameObject picudoNegro;
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
            this.cantidadParcelas = 1;
            _instance = this;
            DontDestroyOnLoad(this.gameObject); // Evita que se destruya al cambiar de escena
            DontDestroyOnLoad(this.cultivo);
            DontDestroyOnLoad(this.picudos);

            DontDestroyOnLoad(this.canvas);
            plantInstances = new List<GameObject>(); // Inicializa la lista en el Awake
            //GeneratePlantInstances(); // Genera las instancias al inicio
            Scene scene = SceneManager.GetSceneByName("SampleScene");
            if (scene == SceneManager.GetActiveScene()){
                GeneratePlantInstances();
                generarPicudos();
                this.picudos.gameObject.SetActive(false);
            }
        }
        
    }

     void Start(){
        this.startDialogQuestion("¡Bienvenido al nivel 1 'Siembra'! Para empezar, ve hacia el letrero a tu izquierda y presiona la tecla ESPACIO para interactuar.",0.08f);
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

    public void startDialogQuestionChangeScene(string texto,float typingTime,string sceneName)
    {
        if (!didDialogStart)
        {
            startDialogChangeScene(texto,typingTime,sceneName);
        }
    }

    private void startDialogChangeScene(string texto, float typingTime,string sceneName)
    {
        didDialogStart = true;
        dialogPanel.SetActive(true);
        dialogTextAux.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 1f;
        StartCoroutine(ShowLineChangeScene(texto,typingTime,sceneName));
    }

    private IEnumerator ShowLineChangeScene(string texto,float typingTime,string sceneName)
    {
        dialogText.text = string.Empty;

        foreach (char ch in texto){
            dialogText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
        SceneManager.LoadScene(sceneName);
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
    public int getColinosPodados(){
        return this.colinosPodados;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(juegoIniciado){
            if(scene.name!="Store"){
                GameManager.Instance.LoadPlayerPosition();
            }
            if(scene.name=="SceneLevel3"){
                this.picudos.gameObject.SetActive(true);
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
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

    void generarPicudos()
    {
        int numberOfInstances = 4;
        Vector3 initialPositionRow1 = new Vector3(-23, 10, 0f);
        Vector3 initialPositionRow2 = new Vector3(-23, 0, 0f);
        Vector3 initialPositionRow3 = new Vector3(-23, -2, 0f);
        List<GameObject> picudosDisponibles = new List<GameObject>();

        GameObject picudo1 = picudorayado;
        picudosDisponibles.Add(picudo1);
        GameObject picudo2 = Picudoamarillo;
        picudosDisponibles.Add(picudo2);
        GameObject picudo3 = picudoNegro;
        picudosDisponibles.Add(picudo3);

        System.Random rnd = new System.Random();
        for (int i = 0; i < numberOfInstances; i++){
            GameObject randomPicudoFila1 = picudosDisponibles[Random.Range(0, picudosDisponibles.Count)];
            GameObject randomPicudoFila2 = picudosDisponibles[Random.Range(0, picudosDisponibles.Count)];
            int rotacionAleatoria = rnd.Next(0, 360);
            Vector3 spawnPositionRow3 = initialPositionRow3 + new Vector3(rnd.Next(2, 16), rnd.Next(-4, 10), 0f);
            GameObject newPlantInstanceRow1 = Instantiate(randomPicudoFila1, spawnPositionRow3, Quaternion.Euler(0f, 0f, rotacionAleatoria), picudos);
            Vector3 spawnPositionRow2 = initialPositionRow2 + new Vector3(rnd.Next(4, 12), rnd.Next(-4, 10), 0f);
            GameObject newPlantInstanceRow2 = Instantiate(randomPicudoFila2, spawnPositionRow2, Quaternion.Euler(0f, 0f, rotacionAleatoria), picudos);
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

              if(itemExistente.titulo == "Hacha"){
                  this.hachasCompradas = this.hachasCompradas + 1;
                  if(hachasCompradas==2){ 
            PlayerPrefs.SetInt("flagText",1);}
            }
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
    public void updateColinosPodados(int cantidad)
    {
        this.colinosPodados = this.colinosPodados+cantidad;
    }

    public void resetinformacionBuyItems()
    {
        this.informacionBuyItems = new List<itemBuyInformation>();
    }

    public int getCantidadParcelas()
    {
       return this.cantidadParcelas;
    }
    public int getHachasCompradas()
    {
       return this.hachasCompradas;
    }

    public void activarPicudoslevel3()
    {
        this.picudos.gameObject.SetActive(true);
    }  
}
