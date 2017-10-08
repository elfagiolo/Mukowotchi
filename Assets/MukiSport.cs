using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MukiSport : MonoBehaviour {

    public static SportTyp s_selectedSportTyp = SportTyp.GIRAFFE;

    [SerializeField]
    private float m_fSportDuration = 10f;
    [SerializeField]
    private Animator m_animLadebalken;
    [SerializeField]
    private UnityEvent OnEnd;
    [SerializeField]
    private UnityEvent OnSwitchSides;

    private GameObject m_sportObject;
    private float m_fTimer = 0f;
    private Coroutine coroutine_SetTimer = null;
    private bool m_bSwitchedSides = false;
    
    public float Progress { get { return m_fTimer / m_fSportDuration; } }

    void Awake()
    {
        // turn on the right gameobject
        m_sportObject = transform.GetChild((int)s_selectedSportTyp).gameObject;
        m_sportObject.SetActive(true);
    }
	
	public void StartUebung(bool switchSides)
    {
        if (switchSides && !m_bSwitchedSides)
        {
            m_bSwitchedSides = true;
            m_sportObject.transform.GetChild(0).gameObject.SetActive(false);
            m_sportObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (coroutine_SetTimer == null)
        {
            coroutine_SetTimer = StartCoroutine(SetTimer());
        }
    }

    IEnumerator SetTimer()
    {
        // Übung durchlaufen
        m_fTimer = 0f;
        while(Progress < 1f)
        {
            m_fTimer += Time.deltaTime;
            if (m_animLadebalken) m_animLadebalken.SetFloat("progress", Progress);
            yield return null;
        }
        if (m_animLadebalken) m_animLadebalken.SetFloat("progress", Progress);

        // Ende erreicht?
        if (s_selectedSportTyp == SportTyp.RUTSCHE || m_bSwitchedSides)
        {
            Score s = Score.s_instance;
            if (s) s.QueueStars(3, 1);
            OnEnd.Invoke();
        }
        else
            OnSwitchSides.Invoke();

        // cleanup
        coroutine_SetTimer = null;
    }
}
