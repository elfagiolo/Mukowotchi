using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static Score s_instance;

    private int m_score = 0;
    private Text m_text;

    void Awake()
    {
        if (!s_instance) s_instance = this;
        else Destroy(transform.parent.gameObject);

        m_text = GetComponentInChildren<Text>();
        UpdateText();
    }

    void UpdateText()
    {
        m_text.text = m_score.ToString();
    }

    public void AddStars(int stars)
    {
        m_score += stars;
        UpdateText();
    }
}
