using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionBD : MonoBehaviour
{
   [SerializeField] private List<Question> m_questionList = null;
   [SerializeField] public string scene;
   [SerializeField] public List<Question> m_backup;

   private void Awake(){
      m_backup = m_questionList.ToList();
   }

   public Question GetRandom(bool remove = true){
      Question q = null;
      if(m_questionList.Count == 0)
      {
         //RestoreBackup();
         SceneManager.LoadScene(scene);
      }else{
         int index = Random.Range(0 , m_questionList.Count);
         if(!remove){
            return m_questionList[index];
         }
         q = m_questionList[index];
         m_questionList.RemoveAt(index);
      }
      return q;
   }

   private void RestoreBackup(){
      m_questionList = m_backup.ToList();
   }
}