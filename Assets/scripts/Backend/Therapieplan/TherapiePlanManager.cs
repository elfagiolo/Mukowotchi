using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


//Informationen des Therapieplans, runtergebrochen auf speicherbare Daten
[Serializable]
public struct DataToSave
{
    public TherapiePlan plan;
    public List<float> actives;
    public int savingDay;
    public int savingMonth;
    public int savingYear;
    public float latestTime;
}



[RequireComponent(typeof(BasicNotification))]
//TherapiePlanManager.cs kümmert sich um das speichern und laden des Therapieplans
public class TherapiePlanManager : MonoBehaviour
{
    public static TherapiePlanManager instance;

    //Notifier Referenz
    private BasicNotification notifier;
    private const int numOfDaysToSchedule = 7;

    //Save and Load
    private string savePath;
    public bool needsToSave = false;

    //Therapieplan
    [SerializeField]
    private TherapiePlan therapiePlan;

    private bool hasScheduledOnStart = false;


    //Aktive Therapien
    private float checkInterval = 60.0f; //Once per Minute

    public List<float> aktiveTherapieZeiten;
    public List<float> AktiveTherapieZeiten
    {
        get
        {
            return aktiveTherapieZeiten;
        }

    }

    public float latestTimeCheckedToday = 0;


    public TherapiePlan TherapiePlan
    {
        get
        {
            return therapiePlan;
        }
    }

    // Use this for initialization
    void Awake ()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        //DontDestroyOnLoad(gameObject);
        savePath = Application.persistentDataPath + "/PlanData.dat";
        //init new List
        therapiePlan = new TherapiePlan();
        aktiveTherapieZeiten = new List<float>();
        //fill new List with SavedData
        LoadDataFromDisk();
        //Init Inheritance List
        therapiePlan.InitTherapieListe();
        therapiePlan.BuildCalendar();
    }

    private void Start()
    {
        notifier = GetComponent<BasicNotification>();
        if (!hasScheduledOnStart)
            ScheduleTherapyForSetDays();

        StartCoroutine(CoroutineCheckTherapyPlan());
    }

    //Speichert den Therapieplan als Binärdatei

    public void SaveDataToDisk()
    {
        Debug.Log("Saving PlanData");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        DataToSave data;
        data.plan = therapiePlan;
        data.actives = aktiveTherapieZeiten;
        data.latestTime = latestTimeCheckedToday;
        DateTime today = DateTime.Now;
        data.savingDay = today.Day;
        data.savingMonth = today.Month;
        data.savingYear = today.Year;
        bf.Serialize(file, data);
        file.Close();
        needsToSave = false;
    }

    //Lädt den Therapieplan aus einer Binärdatei
    public void LoadDataFromDisk()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            DataToSave data = (DataToSave)bf.Deserialize(file);
            if (data.plan != null)
                therapiePlan = data.plan;
            else
                Debug.LogWarning("Die Daten enthalten keinen Therapieplan.");
            if (data.actives != null)
                aktiveTherapieZeiten = data.actives;
            else
                Debug.LogWarning("Die Daten enthalten keine aktiven Therapiezeiten.");
            if (DateTime.Today == new DateTime(data.savingYear, data.savingMonth, data.savingDay))
            {
                latestTimeCheckedToday = data.latestTime;
            }
            else
            {
                latestTimeCheckedToday = 0;
            }
            file.Close();
        }
        else
        {
            Debug.LogWarning("Es gibt keinen gespeicherten Therapieplan.");
        }
    }

    public void ScheduleTherapyForSetDays()
    {
        notifier.Notify(therapiePlan.GetTherapieForXDaysFromNow(numOfDaysToSchedule));
    }

    public void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SaveDataToDisk();
            ScheduleTherapyForSetDays();
        }
    }
    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveDataToDisk();
            ScheduleTherapyForSetDays();
        }
    }

    public void OnApplicationQuit()
    {
        SaveDataToDisk();
        ScheduleTherapyForSetDays();

    }

    IEnumerator CoroutineCheckTherapyPlan()
    {
        DateTime today = DateTime.Today;
        int dayOfWeek = (int)today.DayOfWeek;
        dayOfWeek = (dayOfWeek - 1) < 0 ? 6 : dayOfWeek - 1;

        CheckTherapy(today, dayOfWeek);
        yield return new WaitForSeconds(60.0f - DateTime.Now.Second);

        while (true)
        {
            CheckTherapy(today, dayOfWeek);
            yield return new WaitForSeconds(checkInterval);
        }
    }

    public void CheckTherapy(DateTime today, int dayOfWeek)
    {
        Debug.Log("Checking for active Therapies");
        //If the day has changed while playing, reset the day
        if (today != DateTime.Today)
        {
            today = DateTime.Today;
            dayOfWeek = (int)today.DayOfWeek;
            dayOfWeek = (dayOfWeek - 1) < 0 ? 6 : dayOfWeek - 1;
            latestTimeCheckedToday = -1;
        }

        if (therapiePlan.calendar[dayOfWeek].Count > 0)
        {
            float currentTime = TherapiePlan.TimeIntToFloat(DateTime.Now.Hour, DateTime.Now.Minute);

            float tmpLatest = latestTimeCheckedToday;
            foreach (float time in therapiePlan.calendar[dayOfWeek].Keys)
            {
                Debug.Log("checking " + time + " at " + currentTime + "with latest" + latestTimeCheckedToday + "res:" + (time <= currentTime) + "|" + (time >= currentTime - 1.0f) + "|" + (time > latestTimeCheckedToday));
                //if the time is younger than the current time by less than an hour (current: 14:30 time:13:30 - 14:30)
                if (time > latestTimeCheckedToday && time <= currentTime && time >= currentTime - 1.0f)
                {
                    Debug.Log("Adding " + time + "to actives" + (time > latestTimeCheckedToday) + latestTimeCheckedToday);
                    aktiveTherapieZeiten.Add(time);
                    if(time > tmpLatest)
                        tmpLatest = time;
                }
            }
            latestTimeCheckedToday = tmpLatest;
        }
        ButtonWiggleBlinkManager.instance.CheckActiveTherapies();
        ButtonWiggleBlinkManager.instance.CheckNeeds();
        ButtonWiggleBlinkManager.instance.CheckKreon();
    }

    public void RemoveActiveTherapy(float time)
    {
        aktiveTherapieZeiten.Remove(time);
    }

}
