using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image m_Panelimage;

    float m_fadespeed = 0.002f;

    float alfa;

    bool m_fadeOut = true;
    bool m_fadeIn = false;

    void Start()
    {
        m_Panelimage = GetComponent<Image>();
    }

    void Update()
    {
        //if (m_fadeOut)
        //{
        //    FadeOut();
        //}
        //if (m_fadeIn)
        //{
        //    FadeIn();
        //}
    }

    /// <summary>
    /// ‚¾‚ñ‚¾‚ñ–¾‚é‚­‚È‚é
    /// </summary>
    public void FadeIn() 
    {
        alfa -= m_fadespeed;
        m_Panelimage.color = new Color(0, 0, 0, alfa);
        if (alfa >= 1)
        {
            m_fadeIn = false;
            m_Panelimage.enabled = false;
        }
    }

    /// <summary>
    /// ‚¾‚ñ‚¾‚ñˆÃ‚­‚È‚é
    /// </summary>
    public void FadeOut() 
    {
        m_Panelimage.enabled = true;
        alfa += m_fadespeed;
        m_Panelimage.color = new Color(0, 0, 0, alfa);
        if (alfa >= 1)
        {
            m_fadeOut = false;
            m_fadeIn = true;
        }
    }
}
