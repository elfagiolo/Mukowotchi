using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class BasicNotification : MonoBehaviour
{
    public const int NotificationID = 1;
    
    public Texture2D LargeIcon;
    private int _number = 1;
    public Color32 _notifyColor;
    private bool _sticky = false;

    //Save and Load
    private string savePath;
    private int highestID;

    public void Awake()
    {
        savePath = Application.persistentDataPath + "/Notification.dat";
        LoadDataFromDisk();
    }

    /**
     * Saves the save data to the disk
     */
    public void SaveDataToDisk()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, highestID);
        file.Close();
        //Debug.Log("Saved HighestID " + highestID);
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
            highestID = (int)bf.Deserialize(file);
            file.Close();
            //Debug.Log("Loaded HighestID " + highestID);
        }
    }


    public void Notify(TherapiePlan.NotificationInfo[] infoStack)
    {
        _number = 1;
        for (int i = 1; i < highestID;i++)
        {
            NotificationManager.Cancel(i);
        }

        foreach (TherapiePlan.NotificationInfo info in infoStack)
        {
            Notify(info);
        }
        highestID = _number;
        SaveDataToDisk();
    }

    public void Notify(TherapiePlan.NotificationInfo info)
    {
        //Debug.Log("Scheduling Notification: " + info.title + "|" + info.time.DayOfWeek.ToString() + info.time.Hour + info.time.Minute);
        /*
		 * Unfortunately, from Unity 5.0, providing Android resources became obsolete.
		 * Before Unity 5.0, you can provide the name of the drawable in the folder Plugins/Android/res/drawable.
		 */
        Notification notification = new Notification(info.title, info.message);
        //notification.SetContentInfo("");
        notification.EnableSound(true);

        /*
		 * Requires VIBRATE permission.
		 */
        notification.SetVibrate(new long[] {200, 400, 100, 400, 100});

        /*
		 * Lights or LED notification are only working when screen is off.
		 */
        notification.SetLights(_notifyColor, 500, 500);

        /*
		 * If you pass a texture, it has to be readable. 
		 * Tick Read/Write Enabled option for the Texture in the inspector
		 */
        notification.SetLargeIcon(LargeIcon);

        if (_number > 1)
            //notification.SetNumber(_number);

        notification.SetSticky(_sticky);
        NotificationManager.ShowNotification(_number, notification, info.time);
        ++_number;
    }
}
