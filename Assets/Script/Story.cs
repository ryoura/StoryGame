using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    [SerializeField]
    private Text m_messageView = null;

    [SerializeField]
    private Text m_nameView = null;

    [SerializeField]
    private GameObject[] m_player;

    float m_fadespeed = 0.05f;

    float alfa;

    //���̃Z���t
    int m_serifu = 1;

    //��b��
    int m_conversational = 0;

    //�R���[�`���̕ϐ�
    Coroutine m_coroutine;

    //���̃X�g�[���[�e�L�X�g
    string m_textLoad;

    //�X�g�[���[�̕�����
    string[] m_textList;

    //���O�ƃZ���t
    string[] m_nameList;

    [SerializeField]
    Fade fade;
    private void Start()
    {
        m_textLoad = (Resources.Load("Story", typeof(TextAsset)) as TextAsset).text;
        m_textList = m_textLoad.Split(char.Parse("/"));
        Caiwa(m_textList[m_conversational]);
    }

    private IEnumerator ShowMessagesAsync(string[] messages)
    {
        yield return null;
        m_messageView.text = "";
        int a = 0;
        for (int k = m_serifu; k < messages.Length; k++)
        {
            string m = CommandCheck(messages[k]);
            for (int i = 0; i < m.Length;)
            {
                if (a >= 10)
                {
                    m_messageView.text += m[i]; // �ꕶ���ǉ�
                    a = 0;
                    i++;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    break;
                }
                a++;
                yield return null;
            }
            m_messageView.text = m;
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    m_messageView.text = "";
                    break;
                }
                yield return null;
            }
            yield return null;
        }
        yield return null;
        m_conversational++;
        m_serifu = 1;
        Caiwa(m_textList[m_conversational]);
    }

    void Command(string command)
    {
        //c.fi,0 �R�}���h.�t�F�[�h�C��,�v���C���[�z��̉��Ԗڂ�
        string[] f = command.Split(char.Parse(","));
        switch (f[0])
        {
            case "fi":
                StartCoroutine(FadeIn(m_player[int.Parse(f[1])].GetComponent<Image>()));
                break;
            case "fo":
                StartCoroutine(FadeOut(m_player[int.Parse(f[1])].GetComponent<Image>()));
                break;
            case "face":
                m_player[int.Parse(f[1])].GetComponent<Player>().Face(int.Parse(f[2]));

                break;
        }
    }

    string CommandCheck(string messages)
    {
        string[] s = messages.Split(char.Parse(" "));
        if (s.Length == 1)
        {
            return messages;
        }
        string[] f = s[1].Split(char.Parse("."));
        Command(f[1]);

        return s[0];
    }
    void Caiwa(string c)
    {
        string[] f = c.Split(char.Parse("."));
        if (f[0] == "end")
        {
            StartCoroutine(fade.FadeOut());
        }
        else if (f[0] == "c")
        {
            Command(f[1]);
        }
        else
        {
            m_nameList = c.Split(char.Parse("\n"));
            m_nameView.text = m_nameList[0].Trim();
            m_coroutine = StartCoroutine(ShowMessagesAsync(m_nameList));
        }
    }
    /// <summary>
    /// ���񂾂񖾂邭�Ȃ�
    /// </summary>
    public IEnumerator FadeIn(Image image)
    {
        float fadeIn = 0;
        int tien = 0;
        while (fadeIn <= 1)
        {
            if (tien >= 20)
            {
                fadeIn += m_fadespeed;
                image.color = new Color(1, 1, 1, fadeIn);
                tien = 0;
            }
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            tien++;
            yield return null;
        }
        image.color = new Color(1, 1, 1, 1);
        NextScene();
    }
    /// <summary>
    /// ���񂾂�Â��Ȃ�
    /// </summary>
    public IEnumerator FadeOut(Image image)
    {
        int tien = 0;
        while (alfa >= 0)
        {
            if (tien >= 20)
            {
                alfa -= m_fadespeed;
                image.color = new Color(1, 1, 1, alfa);
                tien = 0;
            }
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            tien++;
            yield return null;
        }
        image.color = new Color(1, 1, 1, 0);
        NextScene();
    }
    public void NextScene()
    {
        m_conversational++;
        Caiwa(m_textList[m_conversational]);
    }
}