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

    //Variables de la escena de preguntas
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor = Color.black;
    [SerializeField] private float m_waitTime = 0.0f;

    private QuestionBD m_questionBD = null;
    private QuestionUI m_questionUI = null;
    private AudioSource m_audioSource = null;

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
    private void Start(){
        m_questionBD = GameObject.FindObjectOfType<QuestionBD>();
        m_questionUI = GameObject.FindObjectOfType<QuestionUI>();
        m_audioSource = GetComponent<AudioSource>();

        NextQuestion();
    }

    private void NextQuestion(){
        m_questionUI.Construct(m_questionBD.GetRandom(), GiveAnswer);
    }

    private void GiveAnswer(OptionButton optionButton)
    {
        StartCoroutine(GiveAnswerRoutime(optionButton));
    }

    private IEnumerator GiveAnswerRoutime(OptionButton optionButton){

    if(m_audioSource.isPlaying)
        m_audioSource.Stop();

    m_audioSource.clip =optionButton.Option.correct ? m_correctSound : m_incorrectSound;
    optionButton.SetColor(optionButton.Option.correct ? m_correctColor : m_incorrectColor);

    m_audioSource.Play();

    yield return new WaitForSeconds (m_waitTime);

    if(optionButton.Option.correct){
        NextQuestion();
    }else{
        GameOverQuestion();
    }
    }

    //Logica del GameObject aqui podemos poner el metodo para descontar o sumar el score
    private void GameOverQuestion(){
        Debug.Log("Respuesta Mala");
    }
    //*****************************************

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
            //GeneratePlantInstances(); // Genera las instancias al inicio
            Scene scene = SceneManager.GetSceneByName("SampleScene");
            if (scene == SceneManager.GetActiveScene()){
                GeneratePlantInstances();
            }
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
            item.cantidad = 1;
            informacionBuyItems.Add(item);
        }else{
            // Si existe, incrementar la cantidad
            itemExistente.cantidad += 1;
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

}
