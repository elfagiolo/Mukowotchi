using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PasswordScript : MonoBehaviour {

    private const string PASSWORD = "trueffel";

    public static int lastScene = 0;

    private InputField m_input;

    private void Awake()
    {
        m_input = GetComponentInChildren<InputField>();
        Assert.IsNotNull(m_input);
    }

    public void ComparePassword()
    {
        if (m_input.text.Equals(PASSWORD))
        {
            lastScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(3);
        }
    }
}
