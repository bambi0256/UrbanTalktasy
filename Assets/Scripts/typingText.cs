using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typingText : MonoBehaviour
{
    public Text tx;
    private string m_text;

    void Start()
    {
        GetText();
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i <= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);
        }
    }

    void GetText(int id)
    {
        m_text = DialogueParse.GetDialogue(id).;
    }
}
