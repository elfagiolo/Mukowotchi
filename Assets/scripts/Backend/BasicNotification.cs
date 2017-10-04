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
        savePath = Application.persistentDataPath + "/NotifyID.dat";
    }

    public void Notify(TherapiePlan.NotificationInfo[] infoStack)
    {
        //NotificationManager.CancelAll();
        foreach (TherapiePlan.NotificationInfo info in infoStack)
        {
            Notify(info);
        }
    }

    public void Notify(TherapiePlan.NotificationInfo info)
    {
        Debug.Log("Scheduling Notification: " + info.title + "|" + info.time.DayOfWeek.ToString() + info.time.Hour + info.time.Minute);
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
        notification.SetVibrate(new long[] { 200, 100, 100});

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
