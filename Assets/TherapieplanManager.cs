using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TherapieplanManager : MonoBehaviour
{
    public static TherapieplanManager instance;
    public Mediplan therapiePlan;

    public int hour = 0, minute = 0, count = 1;

    public string mediName = "Medikament A";

    public bool[] weekdays;

    public BasicNotification notifier;

    public void Start()
    {
        instance = this;
        weekdays = new bool[7];
        therapiePlan = new Mediplan();
    }

    public void OKAY()
    {
        therapiePlan.AddMedicine(mediName, "blablub", weekdays, hour, minute, count);
    }

    public void SetWeekDay(int i, bool b)
    {
        weekdays[i] = b;
    }

    public void SetHour(int i)
    {
        hour = i;
    }

    public void SetMinute(int i)
    {
        minute = i;
    }

    public void SetCount(int i)
    {
        count = i;
    }

    public void ScheduleAllNotificationsForToday()
    {
        notifier.Notify(therapiePlan.CheckMedicineForToday().ToArray());
    }
}
