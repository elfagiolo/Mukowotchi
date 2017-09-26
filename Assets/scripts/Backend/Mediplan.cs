using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MedicinePlan", menuName = "Mukowotchi/", order = 1)]

public class Mediplan
{
    public struct NotificationInfo
    {
        public DateTime time;

        public string title;

        public string message;
    }

    public struct Medicine
    {
        //Name des Medikaments
        public string name;

        //Kurze Beschreibung
        public string description;

        //Tage an denen die Medikation eingenommen werden soll
        public bool[] weekdays;

        //Zeit und Anzahl der Tabletteneinnahme
        public Dictionary<float, int> times;
    }

    public Mediplan()
    {
        medicineList = new List<Medicine>();
    }


    private List<Medicine> medicineList;

    public void AddMedicine(string _name, string _description, bool[] _weekdays, int _hours, int _minutes, int _count)
    {
        Medicine newMed = new Medicine();
        newMed.name = _name;
        newMed.description = _description;

        //Init arrays & Lists
        newMed.weekdays = new bool[7];
        newMed.times = new Dictionary<float, int>();

        _weekdays.CopyTo(newMed.weekdays, 0);

        float fTime = _hours + _minutes / 60.0f;
        Dictionary<float, int> tmp = newMed.times;
        if (!tmp.ContainsKey(fTime))
            newMed.times.Add(fTime, _count);
        else
            Debug.LogWarning(Mathf.FloorToInt(fTime) + Mathf.FloorToInt((fTime % 1.0f) * 10.0f) + " is already set for this Medication");

        medicineList.Add(newMed);
    }

    public List<NotificationInfo> CheckMedicineForToday()
    {
        DateTime Now = DateTime.Now;
        List<NotificationInfo> info = new List<NotificationInfo>();
        int dayOfWeek = (int)Now.DayOfWeek;
        foreach(Medicine med in medicineList)
        {
            if(med.weekdays[dayOfWeek-1])
            {
                foreach(float time in med.times.Keys)
                {
                    NotificationInfo newInfo = new NotificationInfo();
                    int[] hourMinute = ReformatTimeToInt(time);
                    newInfo.time = new DateTime(Now.Year, Now.Month, Now.Day, hourMinute[0], hourMinute[1], 0);
                    newInfo.title = "Medikamentenwecker";
                    newInfo.message = "Muki muss " + (med.name) + "einnehmen.";
                    info.Add(newInfo);
                }
            }
        }

        return info;

    }

    public int[] ReformatTimeToInt(float fTime)
    {
        int[] hourMinute = new int[2];
        hourMinute[0] = (int)Mathf.Floor(fTime);
        hourMinute[1] = (int)((fTime - hourMinute[0]) * 60.0f);
        Debug.Log(fTime + "->" + hourMinute[0] + ":" + hourMinute[1]);
        return hourMinute;
    }

    public string ReformatTimeToString(float fTime)
    {
        float hour = Mathf.Floor(fTime);
        float minute = (fTime - hour) * 60.0f;
        string time = hour.ToString("00") + ":" + ((fTime - hour) * 60.0f).ToString("00");
        Debug.Log(fTime + "->" + time);
        return time;
    }
}
