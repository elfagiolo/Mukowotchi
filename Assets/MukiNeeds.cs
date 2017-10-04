using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MukiNeeds : MonoBehaviour {

    [SerializeField]
    UnityEngine.UI.Text m_textHunger;
    [SerializeField]
    UnityEngine.UI.Text m_textThirst;

    private float m_fHunger = 0f;
    private float m_fThirst = 0f;

    void Update()
    {
        m_fHunger += Time.deltaTime * 10;
        m_fThirst += Time.deltaTime * 10;

        if (m_textHunger != null)
        {
            m_textHunger.text = "Hunger\n" + m_fHunger;
        }
        if (m_textThirst != null)
        {
            m_textThirst.text = "Hunger\n" + m_fThirst;
        }
    }
}
