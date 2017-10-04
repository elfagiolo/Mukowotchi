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
    private bool init = false;

    private string savePath;
    private NeedsObject needs;

    void Awake()
    {
        if (!s_instance) s_instance = this;
        else Destroy(gameObject);

        savePath = Application.persistentDataPath + "/saveNeeds.dat";
    }

    void Start()
    {
        needs = new NeedsObject();
        needs.hunger = 0f;
        needs.thirst = 0f;
        needs.eatDateAndTime = new int[5];
        needs.drinkDateAndTime = new int[5];
        if (init)
            SaveNeeds();
        else
            LoadNeeds();
    }

    void Update()
    {
        needs.hunger += Time.deltaTime * 10f;
        needs.thirst += Time.deltaTime * 10f;

        if (debugText != null)
        {
            debugText.text = string.Format("Hunger: {0}\n" +
                "Letzte Mahlzeit: {1}.{2}.{3} {4}:{5}\n" +
                "Durst: {6}\n" +
                "Letztes Getränk: {7}.{8}.{9} {10}:{11}\n",
                needs.hunger,
                needs.eatDateAndTime[0],
                needs.eatDateAndTime[1],
                needs.eatDateAndTime[2],
                needs.eatDateAndTime[3],
                needs.eatDateAndTime[4],
                needs.thirst,
                needs.drinkDateAndTime[0],
                needs.drinkDateAndTime[1],
                needs.drinkDateAndTime[2],
                needs.drinkDateAndTime[3],
                needs.drinkDateAndTime[4]
                );
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveNeeds();
        }
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
            Debug.LogWarning("Did not find the file Application.persistentDataPath + \" / saveNeeds.dat\"");
    }

    public void Consumes(float value, bool food)
    {
        if (food)
        {
            needs.hunger -= value;
            if (needs.hunger < 0f) needs.hunger = 0f;
        }
        else
        {
            needs.thirst -= value;
            if (needs.thirst < 0f) needs.thirst = 0f;
        }
    }
}

[Serializable]
public struct NeedsObject
{
    [SerializeField]
    public float hunger;
    [SerializeField]
    public float thirst;
    [SerializeField]
    public int[] eatDateAndTime; // {tag,monat,jahr,stunde,minute}
    [SerializeField]
    public int[] drinkDateAndTime;
}