using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGO : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_arrGO;
    [SerializeField]
    private int[] m_scenes;

    private List<int> m_buildIds = new List<int>();

    void Awake()
    {
        foreach (int i in m_scenes) m_buildIds.Add(i);
    }

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool active = m_buildIds.Contains(scene.buildIndex);

        foreach (GameObject go in m_arrGO)
        {
            go.SetActive(active);
        }
    }

    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
