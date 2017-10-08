using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Muki : MonoBehaviour {

    public static Muki s_instance;

    [SerializeField]
    private Vector3 m_v3KitchenPosition = Vector3.zero;

    private const int m_iKitchenID = 1;
    private Animator m_animator;
    private int m_iPillCounter = 0;
    private Vector3 m_v3OriginalPosition;
    private Transform m_transform;

    public bool m_bMouthIsFull { get { return m_iPillCounter > 0; } }

    private void Awake()
    {
        if (!s_instance) s_instance = this;
        else Destroy(gameObject);

        m_animator = GetComponent<Animator>();
        m_transform = transform;
        m_v3OriginalPosition = m_transform.position;
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

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_animator.SetInteger("eating", 0);

        switch (scene.buildIndex)
        {
            case m_iKitchenID:
                m_transform.position = m_v3KitchenPosition;
                break;
            case 3: break;
            default: m_transform.position = m_v3OriginalPosition;
                break;
        }
    }

    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
