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

    private int m_iKreons = 0; // number of active kreons right now
    private int m_iKreonsToSpawn = 0;
    private Transform m_transKreonSpawn;
    private Coroutine coroutine_KreonSpawner;

    void Awake()
    {
        if (!s_instance) s_instance = this;
        else Destroy(gameObject);

        m_transKreonSpawn = transform.GetChild(0);
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        SpawnKreons(3);
    //    }
    //}

    //void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnLevelFinishedLoading;
    //}

    //void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    //}

    //void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log("Level " + scene.buildIndex + " Loaded");
    //    Debug.Log(scene.name);
    //    Debug.Log(mode);
    //}

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
            m_iKreons++;
            m_iKreonsToSpawn--;
            yield return new WaitForSeconds(m_fSpawnDuration);
        }
    }
}
