using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static Score s_instance;

    [SerializeField]
    private GameObject[] m_arrStars;

    private int m_score = 0;
    private Text m_text;
    private Animator m_anim;

    void Awake()
    {
        if (!s_instance) s_instance = this;
        else Destroy(transform.parent.gameObject);

        m_text = GetComponentInChildren<Text>();
        m_anim = GetComponent<Animator>();
        UpdateText();
    }

    void UpdateText()
    {
        m_text.text = m_score.ToString();
    }

    public void AddStar(int starIndex)
    {
        m_score++;
        SpawnStar(starIndex);
    }

    public void SpawnStar(int starIndex)
    {
        Instantiate(m_arrStars[starIndex]);
    }

    public void StarArrived()
    {
        m_anim.SetTrigger("AddStar");
        UpdateText();
    }
}
