using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class questions : MonoBehaviour
{
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
    [SerializeField] private float typingTime;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField, TextArea(4, 6)] private string[] dialogLines;
    private int lineIndex;
    public Canvas canvas; 
    // Start is called before the first frame update
    void Start()
    {
        m_questionBD = GameObject.FindObjectOfType<QuestionBD>();
        m_questionUI = GameObject.FindObjectOfType<QuestionUI>();
        m_audioSource = GetComponent<AudioSource>();
        NextQuestion();
    }
    // Update is called once per frame
    private void NextQuestion(){
        m_questionUI.Construct(m_questionBD.GetRandom(), GiveAnswer);
    }

    private void GiveAnswer(OptionButton optionButton){
        StartCoroutine(GiveAnswerRoutime(optionButton));
    }
    private IEnumerator GiveAnswerRoutime(OptionButton optionButton){

        if(m_audioSource.isPlaying){
            m_audioSource.Stop();
        }

        m_audioSource.clip =optionButton.Option.correct ? m_correctSound : m_incorrectSound;
        optionButton.SetColor(optionButton.Option.correct ? m_correctColor : m_incorrectColor);

        m_audioSource.Play();

        yield return new WaitForSeconds (m_waitTime);

        if(optionButton.Option.correct){
            GameManager.Instance.incScore(30);
            NextQuestion();
        }else{
            GameManager.Instance.incScore(-10);
        }
    }
}
