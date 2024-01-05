using System.Collections;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{

    [SerializeField] private AudioClip npcVoice;
    [SerializeField] private float typingTime;
    [SerializeField] private GameObject dialogMark;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField, TextArea(4, 6)] private string[] dialogLines;
    private bool isPlayerInRange;
    private bool didDialogStart;
    private int lineIndex;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = npcVoice;
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
            }
            else
            {
                StopAllCoroutines();
                dialogText.text = dialogLines[lineIndex];
            }

        }
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
        audioSource.Play();

        foreach (char ch in dialogLines[lineIndex])
        {
            dialogText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
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
}
