﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUtility : MonoBehaviour {

    [SerializeField]
    private Vector2 spawnPosition = new Vector2(0f, 0f);

	public void SwitchToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ActivateGameobject(GameObject toActivate)
    {
        toActivate.SetActive(true);
    }

    public void DeactivateGameobject(GameObject toActivate)
    {
        toActivate.SetActive(false);
    }

    public void ToggleGameobject(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    public void ReturnToLastScene()
    {
        SceneManager.LoadScene(PasswordScript.lastScene);
    }

    public void SpawnFood(GameObject prefab)
    {
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
