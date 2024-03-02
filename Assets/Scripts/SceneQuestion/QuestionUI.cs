using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class QuestionUI : MonoBehaviour
{
   [SerializeField] private Text m_question = null;
   [SerializeField] private List<OptionButton> m_buttonList = null;

   public void Construct(Question q, Action<OptionButton> callback)
{
    m_question.text = q.text;

    for(int n = 0; n < m_buttonList.Count; n++)
    {
        m_buttonList[n].Construct(q.options[n], callback);
    }
}
}
