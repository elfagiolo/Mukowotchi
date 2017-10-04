using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPassOn : MonoBehaviour
{
    public void PassNameToManager()
    {
        string text = GetComponent<InputField>().text;
        TherapiePlanUI.instance.therapieName = text;
    }

    public void PassHourToManager()
    {
        string text = GetComponent<InputField>().text;
        int i = Mathf.Clamp(int.Parse(text), 0, 23);
        GetComponent<InputField>().text = i.ToString();
        TherapiePlanUI.instance.SetHour(i);
    }

    public void FixMinutes()
    {
        string text = GetComponent<InputField>().text;
        int i = Mathf.Clamp(int.Parse(text), 0, 59);
        GetComponent<InputField>().text = i.ToString("D2");
    }

    public void PassMinuteToManager()
    {
        string text = GetComponent<InputField>().text;
        int i = Mathf.Clamp(int.Parse(text), 0, 59);
        GetComponent<InputField>().text = i.ToString();
        TherapiePlanUI.instance.SetMinute(i);
    }
    public void PassCountToManager()
    {
        string text = GetComponent<InputField>().text;
        int i = int.Parse(text);
        TherapiePlanUI.instance.SetCount(i);
    }

    public void PassDurationMinute()
    {
        string text = GetComponent<InputField>().text;
        int i = Mathf.Max(int.Parse(text), 0);
        TherapiePlanUI.instance.SetDurationMinute(i);
    }

    public void PassDurationSecond()
    {
        string text = GetComponent<InputField>().text;
        int i = Mathf.Clamp(int.Parse(text), 0, 59);
        TherapiePlanUI.instance.SetDurationSeconds(i);
    }

}
