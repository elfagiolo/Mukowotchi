using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class MukiNeeds : MonoBehaviour {

    public static MukiNeeds s_instance;

    [SerializeField]
    UnityEngine.UI.Text debugText;
    [SerializeField]
    private bool debugUseMinutes = false;

    private string savePath;
    private NeedsObject needs;

    public int HungerPoints { get
        {
            return DateTimeDifferenceInHours(true);
        } }
    public int ThirstPoints
    {
        get
        {
            return DateTimeDifferenceInHours(false);
        }
    }

    void Awake()
    {
        if (!s_instance) s_instance = this;
        else Destroy(gameObject);

        savePath = Application.persistentDataPath + "/saveNeeds.dat";
    }

    void Start()
    {
        needs = new NeedsObject();
        needs.eatDateAndTime = new int[5];
        needs.drinkDateAndTime = new int[5];
        LoadNeeds();
    }

    void Update()
    {
        if (debugText != null)
        {
            debugText.text = string.Format(
                "Letzte Mahlzeit: {0}.{1}.{2} {3}:{4}\n" +
                "Letztes Getränk: {5}.{6}.{7} {8}:{9}\n",
                needs.eatDateAndTime[0],
                needs.eatDateAndTime[1],
                needs.eatDateAndTime[2],
                needs.eatDateAndTime[3],
                needs.eatDateAndTime[4],
                needs.drinkDateAndTime[0],
                needs.drinkDateAndTime[1],
                needs.drinkDateAndTime[2],
                needs.drinkDateAndTime[3],
                needs.drinkDateAndTime[4]
                );
        }

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    print("Hungerpunkte: " + HungerPoints);
        //    print("Durstpunkte: " + ThirstPoints);
        //}
    }

    public void UpdateDateAndTime(bool updateHunger)
    {
        int[] dateAndTime = new int[5]
        {
            DateTime.Now.Day,
            DateTime.Now.Month,
            DateTime.Now.Year,
            DateTime.Now.Hour,
            DateTime.Now.Minute
        };
        if (updateHunger) needs.eatDateAndTime = dateAndTime;
        else needs.drinkDateAndTime = dateAndTime;

        SaveNeeds();
    }

    void SaveNeeds()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, needs);
        file.Close();
    }

    void LoadNeeds()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            needs = (NeedsObject)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.LogWarning("Did not find the file Application.persistentDataPath + \" / saveNeeds.dat\"\n Created one");
            UpdateDateAndTime(true);
            UpdateDateAndTime(false);
            SaveNeeds();
        }
    }

    int DateTimeDifferenceInHours(bool hunger)
    {
        int[] oldArr;
        if (hunger) oldArr = needs.eatDateAndTime;
        else oldArr = needs.drinkDateAndTime;

        DateTime oldDate = new DateTime(oldArr[2], oldArr[1], oldArr[0], oldArr[3], oldArr[4], 0);
        TimeSpan t = DateTime.Now - oldDate;
        if (!debugUseMinutes)
            return (int)Math.Floor(t.TotalHours);
        else
            return (int)Math.Floor(t.TotalMinutes);
    }
}

[Serializable]
public struct NeedsObject
{
    [SerializeField]
    public int[] eatDateAndTime; // {tag,monat,jahr,stunde,minute}
    [SerializeField]
    public int[] drinkDateAndTime;
}