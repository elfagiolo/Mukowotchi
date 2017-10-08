using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MukiInhalation : MonoBehaviour {

    static int inhaleState = Animator.StringToHash("Base Layer.InhalationLoop");
    static int endState = Animator.StringToHash("Base Layer.idleEnd");

    [SerializeField]
    private float m_fLengthInhalationMin = 5;
    [SerializeField]
    private Animator m_animLadebalken;
    [SerializeField]
    private UnityEvent OnInhaleEnd;
    [SerializeField]
    private UnityEvent OnEnd;
    [SerializeField]
    private int m_iStarsEarned = 3;

    private float m_fTimer = 0f;
    private Animator m_anim;
    public float Progress { get
        {
            return (m_fTimer / 60f) / m_fLengthInhalationMin;
        } }

    void Start()
    {
        
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        var currentBaseState = m_anim.GetCurrentAnimatorStateInfo(0);
        if (currentBaseState.fullPathHash == inhaleState)
        {
            // update timer
            m_fTimer += Time.deltaTime;

            // update ladebalken
            if (m_animLadebalken)
            {
                m_animLadebalken.SetFloat("progress", Progress);
            }

            // check endcondition
            if (Progress >= 1f)
            {
                OnInhaleEnd.Invoke();
            }
        }else if (currentBaseState.fullPathHash == endState)
        {
            Score s = Score.s_instance;
            if (s) s.QueueStars(m_iStarsEarned, 0);
            OnEnd.Invoke();
        }
    }
}
