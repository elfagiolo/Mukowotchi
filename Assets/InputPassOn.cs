using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPassOn : MonoBehaviour
{
    public void PassHourToManager()
    {
        string text = GetComponent<InputField>().text;
        int i = int.Parse(text);
        TherapieplanManager.instance.SetHour(i);
    }

    public void PassMinuteToManager()
    {
        string text = GetComponent<InputField>().text;
        int i = int.Parse(text);
        TherapieplanManager.instance.SetMinute(i);
    }
    public void PassCountToManager()
    {
        string text = GetComponent<InputField>().text;
        int i = int.Parse(text);
        TherapieplanManager.instance.SetCount(i);
    }

}
