using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MukiInhalation : MonoBehaviour
{

    static int inhaleState = Animator.StringToHash("Base Layer.InhalationLoop");
    static int endState = Animator.StringToHash("Base Layer.idleEnd");
    
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

    bool hasEnded = false;

    private List<float> inhaTimes;

    void Start()
    {
        
        m_anim = GetComponent<Animator>();
        InitInhaTimes();
    }

    //Init the InhaTimes list and fill it with the physiotherapy times from activetherapies
    void InitInhaTimes()
    {
        TherapiePlanManager manager = TherapiePlanManager.instance;
        inhaTimes = new List<float>();
        foreach (float time in manager.AktiveTherapieZeiten)
        {
            int dayOfWeek = (int)System.DateTime.Now.DayOfWeek - 1;
            dayOfWeek = dayOfWeek < 0 ? 6 : dayOfWeek;

            //If the time is from yesterday
            if (time > System.DateTime.Now.Hour)
            {
                dayOfWeek -= 1;
                dayOfWeek = dayOfWeek < 0 ? 6 : dayOfWeek;
            }

            foreach (TherapiePlan.TherapieInfo therapieInfo in manager.TherapiePlan.GetTherapieInfoFor(dayOfWeek, time))
            {
                Therapie t = manager.TherapiePlan.Therapien[therapieInfo.index];
                Inhalation inha = t as Inhalation;
                if (inha != null)
                {
                    inhaTimes.Add(time);
                    int i = inha.Times.IndexOf(time);
                    m_fLengthInhalationMin = inha.Durations[i];
                }
            }
        }
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
        }
        else if (currentBaseState.fullPathHash == endState && !hasEnded)
        {
            hasEnded = true;
            Score s = Score.s_instance;
            if (s) s.QueueStars(m_iStarsEarned, 0);
            if (inhaTimes.Count > 0)
                TherapiePlanManager.instance.RemoveActiveTherapy(inhaTimes[inhaTimes.Count - 1]);
            OnEnd.Invoke();
        }
    }
}
