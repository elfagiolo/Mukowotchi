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
    private Stack<int> m_stackQueuedStars = new Stack<int>(); //first amount, then starindex
    private Coroutine coroutine_ProcessStack;

    void Awake()
    {
        if (!s_instance) s_instance = this;
        else Destroy(transform.parent.gameObject);

        m_text = GetComponentInChildren<Text>();
        m_anim = GetComponent<Animator>();
        UpdateText();
    }

    void Update()
    {
        if (m_stackQueuedStars.Count > 0 && coroutine_ProcessStack == null)
        {
            coroutine_ProcessStack = StartCoroutine(ProcessStack());
        }
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

    public void QueueStars(int amount, int starIndex)
    {
        // check for traps
        if (amount < 1 || starIndex < 0 || starIndex > 2)
        {
            throw new System.ArgumentException("Falsche Parameter!");
        }

        m_stackQueuedStars.Push(amount);
        m_stackQueuedStars.Push(starIndex);
    }

    IEnumerator ProcessStack()
    {
        while (m_stackQueuedStars.Count > 0)
        {
            int starIndex = m_stackQueuedStars.Pop();
            int amount = m_stackQueuedStars.Pop();
            while (amount > 0)
            {
                AddStar(starIndex);
                amount--;
                yield return new WaitForSeconds(0.3f);
            }
        }

        coroutine_ProcessStack = null;
    }
}
