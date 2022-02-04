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

    //���̃Z���t
    int m_serifu = 1;

    //��b��
    int m_conversational = 0;

    //�������X�L�b�v����
    bool m_skip = true;

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

    bool m_end = false;

    private void Start()
    {
        m_textLoad = (Resources.Load("Story", typeof(TextAsset)) as TextAsset).text;
        m_textList = m_textLoad.Split(char.Parse("/"));
        Caiwa(m_textList[m_conversational]);
    }

    private IEnumerator ShowMessagesAsync(string messages)
    {
        m_messageView.text = "";
        foreach (var ch in messages) // ������̐擪����1����������
        {
            m_messageView.text += ch; // �ꕶ���ǉ�

            // ����҂��Ă���ԁA�X�L�b�v�ł��Ȃ�
            yield return new WaitForSeconds(0.5F); // �w��b�҂�
        }
        m_skip = false;
        m_serifu++;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !m_end)
        {
            if (m_serifu < m_nameList.Length)
            {
                if (m_skip)
                {
                    StopCoroutine(m_coroutine);
                    m_messageView.text = m_nameList[m_serifu];
                    m_serifu++;
                    m_skip = false;
                }
                else
                {
                    m_skip = true;
                    m_coroutine = StartCoroutine(ShowMessagesAsync(m_nameList[m_serifu]));

                }
            }
            else
            {
                Next();
            }
        }
    }

    void Caiwa(string c)
    { 
        if (m_textList[m_conversational] == "end")
        {
            StartCoroutine(fade.FadeOut());
            //StartCoroutine(fade.FadeIn());
            m_end = true;
        }
        else
        {
            m_nameList = c.Split(char.Parse("\n"));
            m_nameView.text = m_nameList[0];
            m_coroutine = StartCoroutine(ShowMessagesAsync(m_nameList[m_serifu]));
        }

    }
    void Next()
    {
        m_skip = true;
        m_conversational++;
        m_serifu = 1;
        Caiwa(m_textList[m_conversational]);
    }
}
