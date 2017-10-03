using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muki : MonoBehaviour {

    public static Muki s_instance;

    private Animator m_animator;
    private int m_iPillCounter = 0;

    private void Awake()
    {
        if (!s_instance) s_instance = this;
        else Destroy(gameObject);

        m_animator = GetComponent<Animator>();
    }

    public void addPill()
    {
        m_iPillCounter++;;
        m_animator.SetFloat("tablettenImMund", (float)m_iPillCounter);
    }

    public void SwallowWithWater()
    {
        m_iPillCounter = 0;
        m_animator.SetFloat("tablettenImMund", 0f);
    }
}
