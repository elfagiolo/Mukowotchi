using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    [SerializeField]
    private Edible[] m_arrEdiblePrefabs;

    private Vector3[] m_arrSpawns;
    private List<int> indeces;

    // fetch data of the spawnpoints
    private void Awake()
    {
        // fetch data for spawns
        var spawns = GetComponentsInChildren<Transform>();
        m_arrSpawns = new Vector3[spawns.Length-1];
        for (int i=1; i<spawns.Length; ++i)
        {
            m_arrSpawns[i-1] = spawns[i].position;
        }

        InitSpawning();
    }

    private void InitSpawning()
    {
        indeces = new List<int>(m_arrEdiblePrefabs.Length);
        int index = 0;
        foreach (Edible e in m_arrEdiblePrefabs)
        {
            indeces.Add(index++);
        }
    }

    private void Start()
    {
        // spawn the shit
        for (int i=0; i<m_arrSpawns.Length; ++i)
        {
            // fetch a random index
            int indexId = Random.Range(0, indeces.Count - 1);
            int index = indeces[indexId];
            indeces.RemoveAt(indexId);

            // instantiate that mf
            GameObject go = Instantiate(m_arrEdiblePrefabs[index].gameObject);

            // move it to the right spot
            go.transform.position = m_arrSpawns[i];
        }
    }
}
