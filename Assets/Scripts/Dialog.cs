using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{

    //[SerializeField] private AudioClip npcVoice;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    [SerializeField] private float typingTime;
    [SerializeField] private GameObject dialogMark;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField, TextArea(4, 6)] private string[] dialogLines;
    private bool isPlayerInRange;
    private bool didDialogStart;
    private int lineIndex;
    private int flag = 0;
    public AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = npcVoice;
        PlayerPrefs.SetInt("flagText", 0);

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (!didDialogStart)
            {
                startDialog();
            }
            else if (dialogText.text == dialogLines[lineIndex])
            {
                NextDialogLine();

                if (lineIndex == 2)
                {
                    StartCoroutine(waitAndLoad(0.5F));
                }
            }
                else if(PlayerPrefs.GetInt("flagDialog")==1)
               {
                   StopAllCoroutines();
                   dialogText.text = dialogLines[lineIndex];
                   StartCoroutine(waitAndLoad(0.5F));
                   }
                

        }
    }
    IEnumerator waitAndLoad(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("DialogueMentor");
        PlayerPrefs.SetInt("flagDialog", 1);
    }

    private void startDialog()
    {
        if(PlayerPrefs.GetInt("flagText")==1){
        didDialogStart = true;
        dialogPanel.SetActive(true);
        dialogMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
        }else{
        GameManager.Instance.startDialogQuestion($"Hola..! ve y compra primero tus hachas y termina de cortar todas las plantas", 0.05F);
        }
    }

    private void NextDialogLine()
    {
        lineIndex++;
        if (lineIndex < dialogLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogStart = false;
            dialogPanel.SetActive(false);
            dialogMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogText.text = string.Empty;
        //audioSource.Play();

        switch (flag)
        {
            case 0:
                PlayAudioClip(audioClip1);
                flag++;
                break;
            case 1:
                PlayAudioClip(audioClip2);
                flag++;
                break;
            case 2:
                PlayAudioClip(audioClip3);
                flag++;
                break;
            default:
                Debug.Log("Opción no válida");
                break;
        }

        foreach (char ch in dialogLines[lineIndex])
        {
            dialogText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        if (flag >= dialogLines.Length)
        {
            flag = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogMark.SetActive(false);
        }
    }

    void PlayAudioClip(AudioClip clip)
    {
        // Asignar el nuevo audioClip al AudioSource y reproducir
        audioSource.clip = clip;
        audioSource.Play();
    }
}
