using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KreonManager : MonoBehaviour {

    public static KreonManager s_instance;

    [SerializeField]
    private GameObject prefab_miniKreon;
    [SerializeField]
    private float m_fDistance = 1f;
    [SerializeField]
    private float m_fSpawnDuration = 0.5f;

    private int m_iKreons = 0; // number of needed kreons rn
    private int m_iKreonsToSpawn = 0;
    private Transform m_transKreonSpawn;
    private Coroutine coroutine_KreonSpawner;
    private int m_iKreonsInMouth = 0;
    private Stack<GameObject> m_KreonStack = new Stack<GameObject>();

    public int KreonsToConsume { get { return m_iKreons; } }
    public int KreonsInMouth { get { return m_iKreonsInMouth; } }

    void Awake()
    {
        if (!s_instance) s_instance = this;
        else Destroy(gameObject);

        m_transKreonSpawn = transform.GetChild(0);
    }

    public void SpawnKreons(int number)
    {
        if (coroutine_KreonSpawner != null)
        {
            StopCoroutine(coroutine_KreonSpawner);
            coroutine_KreonSpawner = null;
        }
        m_iKreonsToSpawn += number;
        coroutine_KreonSpawner = StartCoroutine(coroutine_spawnKreons());
    }

    IEnumerator coroutine_spawnKreons()
    {
        while (m_iKreonsToSpawn > 0)
        {
            var kreon = Instantiate(prefab_miniKreon);
            kreon.transform.parent = m_transKreonSpawn;
            kreon.transform.localPosition = new Vector3(0f, m_iKreons * m_fDistance, 0f);
            m_KreonStack.Push(kreon);
            m_iKreons++;
            m_iKreonsToSpawn--;
            yield return new WaitForSeconds(m_fSpawnDuration);
        }
    }

    public void AddKreonToMouth()
    {
        m_iKreonsInMouth++;
    }

    public void SwallowKreons()
    {
        if (m_iKreonsInMouth == 0)
            return;

        // update the kreon account
        m_iKreons -= m_iKreonsInMouth;

        // destroy the mini kreons on the side if they are there
        for(int i=0; i<m_iKreonsInMouth; ++i)
        {
            if (m_KreonStack.Count > 0)
            {
                var kreon = m_KreonStack.Pop();
                Destroy(kreon);
            }
            else
                break;
        }

        // give star if the right amount is met
        if (m_iKreons == 0)
        {
            Score.s_instance.AddStar(2);
        }
        // reset to zero if it is under
        else if (m_iKreons < 0)
            m_iKreons = 0;

        // reset kreons in the mouth
        m_iKreonsInMouth = 0;
    }
}
