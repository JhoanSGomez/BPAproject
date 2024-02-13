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
    private int bang = 0;
    public AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = npcVoice;

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
            /*    else
               {
                   StopAllCoroutines();
                   dialogText.text = dialogLines[lineIndex];
               } */

        }
    }
    IEnumerator waitAndLoad(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("DialogueMentor");
    }

    private void startDialog()
    {
        didDialogStart = true;
        dialogPanel.SetActive(true);
        dialogMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
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

        switch (bang)
        {
            case 0:
                PlayAudioClip(audioClip1);
                bang++;
                break;
            case 1:
                PlayAudioClip(audioClip2);
                bang++;
                break;
            case 2:
                PlayAudioClip(audioClip3);
                bang++;
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

        if (bang >= dialogLines.Length)
        {
            bang = 0;
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
