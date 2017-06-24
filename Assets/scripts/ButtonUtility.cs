using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUtility : MonoBehaviour {

	public void SwitchToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
