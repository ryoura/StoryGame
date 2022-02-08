using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image m_Panelimage;

    public Image m_nextBackGround;

    public Image m_backGround;

    [SerializeField]
    Story story;

    float m_fadespeed = 0.05f;

    float alfa;

    float m_fadeIn = 1;
    void Start()
    {
        m_Panelimage = GetComponent<Image>();
        story = FindObjectOfType<Story>();
    }


    /// <summary>
    /// ‚¾‚ñ‚¾‚ñ–¾‚é‚­‚È‚é
    /// </summary>
    public IEnumerator FadeIn()
    {
        m_Panelimage.enabled = true;
        while (m_fadeIn >= 0)
        {
            m_fadeIn -= m_fadespeed;
            m_Panelimage.color = new Color(0, 0, 0, m_fadeIn);
            yield return new WaitForSeconds(0.1f);
        }
        m_Panelimage.enabled = false;
        story.NextScene();
    }

    /// <summary>
    /// ‚¾‚ñ‚¾‚ñˆÃ‚­‚È‚é
    /// </summary>
    public IEnumerator FadeOut()
    {
        m_Panelimage.enabled = true;
        while (alfa <= 1)
        {
            alfa += m_fadespeed;
            m_Panelimage.color = new Color(0, 0, 0, alfa);
            yield return new WaitForSeconds(0.1f);
        }
        if (alfa >= 1)
        {
            m_Panelimage.enabled = false;
            m_backGround.enabled = false;
            m_nextBackGround.enabled = true;
            yield return FadeIn();
        }
    }

    public IEnumerator BackFedeOut()
    {
        float alfa = 1;
        while (alfa <= 1)
        {
            alfa -= m_fadespeed;
            m_nextBackGround.color = new Color(1, 1, 1, alfa);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
