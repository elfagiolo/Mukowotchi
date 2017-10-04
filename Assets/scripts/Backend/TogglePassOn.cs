using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePassOn : MonoBehaviour
{
    public void PassToManager(int i)
    {
        bool isOn = GetComponent<Toggle>().isOn;
        TherapiePlanUI.instance.SetWeekDay(i, isOn);
    }
}
