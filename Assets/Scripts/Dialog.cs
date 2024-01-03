using System.Collections;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{

    [SerializeField] private GameObject dialogMark;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField, TextArea(4, 6)] private string[] dialogLines;
    private bool isPlayerInRange;
    private bool didDialogStart;
    private int lineIndex;
    private float typingTime = 0.05f;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Si");
            if (!didDialogStart)
            {
                startDialog();
            }

        }
    }

    private void startDialog()
    {
        didDialogStart = true;
        dialogPanel.SetActive(true);
        dialogMark.SetActive(false);
        lineIndex = 0;
        StartCoroutine(ShowLine());

    }

    private IEnumerator ShowLine()
    {
        dialogText.text = string.Empty;

        foreach (char ch in dialogLines[lineIndex])
        {
            dialogText.text += ch;
            yield return new WaitForSeconds(typingTime);
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
