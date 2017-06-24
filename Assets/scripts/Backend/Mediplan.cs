using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MedicinePlan", menuName = "Mukowotchi/", order = 1)]
public class Mediplan : ScriptableObject
{
    public struct Medicine
    {
        public string name;

        public string description;

        public List<float>[] weekdays;
    }

    private List<Medicine> medicinePlan;

    public void AddMedicine(string name, string description)
    {
        Medicine newMed = new Medicine();
        newMed.name = name;
        newMed.description = description;

        //Init lists for times
        newMed.weekdays = new List<float>[7];
        for(int i = 0; i < 7; i++)
        {
            newMed.weekdays[i] = new List<float>();
        }
    }

    public void AddTime(int iMedicineIndex, int iWeekDay, int iHours, int iMinutes)
    {
        float fTime = iHours + iMinutes / 60;
        List<float> tmp = medicinePlan[iMedicineIndex].weekdays[iWeekDay];
        if (!tmp.Contains(fTime))
            medicinePlan[iMedicineIndex].weekdays[iWeekDay].Add(fTime);
        else
            Debug.LogWarning( Mathf.FloorToInt(fTime) + Mathf.FloorToInt((fTime % 1.0f) * 10) +  " is already set for the given day");
    }

    public Medicine GetMedicine(int iMedicineIndex)
    {
        return medicinePlan[iMedicineIndex];
    }

    public string ReformatTime(float fTime)
    {
        float hour = Mathf.Floor(fTime);
        float minute = (fTime - hour) * 60.0f;
        string time = hour.ToString("00") + ":" + ((fTime - hour) * 60.0f).ToString("00");

        Debug.Log(fTime + "->" + time);
        return time;
    }
}
