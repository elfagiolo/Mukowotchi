using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[RequireComponent(typeof(BasicNotification))]
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
        savePath = Application.persistentDataPath + "/therapiePlan.dat";
        //init new List
        therapiePlan = new TherapiePlan();
        //fill new List with
        LoadDataFromDisk();
        //Init Inheritance List
        therapiePlan.InitTherapieListe();
    }

    private void Start()
    {
        notifier = GetComponent<BasicNotification>();
        if (!hasScheduledOnStart)
            ScheduleTherapyForSetDays();
    }

    /**
     * Saves the save data to the disk
     */
    public void SaveDataToDisk()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, therapiePlan);
        file.Close();
        needsToSave = false;
    }

    /**
     * Loads the save data from the disk
     */
    public void LoadDataFromDisk()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            therapiePlan = (TherapiePlan)bf.Deserialize(file);
            file.Close();
        }
    }

    public void ScheduleTherapyForSetDays()
    {
        notifier.Notify(therapiePlan.GetTherapieForXDaysFromNow(numOfDaysToSchedule));
    }

    public void OnApplicationFocus(bool focus)
    {
        if(!focus && needsToSave)
        {
            SaveDataToDisk();
            ScheduleTherapyForSetDays();
        }
    }
    public void OnApplicationPause(bool pause)
    {
        if (pause && needsToSave)
        {
            SaveDataToDisk();
            ScheduleTherapyForSetDays();
        }
    }

}
