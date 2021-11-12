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

    float m_fadespeed = 0.1f;

    float alfa;

    bool m_fadeOut = true;
    //bool m_fadeIn = false;

    void Start()
    {
        m_Panelimage = GetComponent<Image>();
    }


    /// <summary>
    /// ‚¾‚ñ‚¾‚ñ–¾‚é‚­‚È‚é
    /// </summary>
    public IEnumerator FadeIn()
    {
        m_Panelimage.enabled = true;
        while (alfa >= 1)
        {
            alfa -= m_fadespeed;
            m_Panelimage.color = new Color(0, 0, 0, alfa);
            yield return new WaitForSeconds(0.1f);
        }
        if (alfa <= 1)
        {
            //m_fadeIn = false;
            m_Panelimage.enabled = false;
        }
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
        }
    }
}
