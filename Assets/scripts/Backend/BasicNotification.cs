using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNotification : MonoBehaviour
{
    public const int NotificationID = 1;

    public string _contentTitle = "Hey!";
    public string _contentText = "Mukki is hungry!";
    public Texture2D LargeIcon;
    public int _number = 1;
    private bool _sticky = false;
    
    public void Notify()
    {
        /*
		 * Unfortunately, from Unity 5.0, providing Android resources became obsolete.
		 * Before Unity 5.0, you can provide the name of the drawable in the folder Plugins/Android/res/drawable.
		 */
        Notification notification = new Notification("food", _contentTitle, _contentText);
        notification.SetContentInfo("By Mukowotchi9");
        notification.EnableSound(true);

        /*
		 * Requires VIBRATE permission.
		 */
        notification.SetVibrate(new long[] { 200, 100, 100, 200, 200, 100 });

        /*
		 * Lights or LED notification are only working when screen is off.
		 */
        notification.SetLights(new Color32(255, 0, 0, 255), 500, 500);

        /*
		 * If you pass a texture, it has to be readable. 
		 * Tick Read/Write Enabled option for the Texture in the inspector
		 */
        notification.SetLargeIcon(LargeIcon);

        if (_number > 1)
            notification.SetNumber(_number);

        notification.SetSticky(_sticky);

        NotificationManager.ShowNotification(NotificationID, notification);
        System.DateTime time = new System.DateTime(2017, 6, 24, 22, 40, 0);
        NotificationManager.ShowNotification(NotificationID, notification, time);
        ++_number;
    }

    public void Notify(Mediplan.NotificationInfo[] infoStack)
    {
        foreach(Mediplan.NotificationInfo info in infoStack)
        {
            Notify(info);
        }
    }

    public void Notify(Mediplan.NotificationInfo info)
    {
        /*
		 * Unfortunately, from Unity 5.0, providing Android resources became obsolete.
		 * Before Unity 5.0, you can provide the name of the drawable in the folder Plugins/Android/res/drawable.
		 */
        Notification notification = new Notification("food", info.title, info.message);
        notification.SetContentInfo("By Mukowotchi!");
        notification.EnableSound(true);

        /*
		 * Requires VIBRATE permission.
		 */
        notification.SetVibrate(new long[] { 200, 100, 100, 200, 200, 100 });

        /*
		 * Lights or LED notification are only working when screen is off.
		 */
        notification.SetLights(new Color32(255, 0, 0, 255), 500, 500);

        /*
		 * If you pass a texture, it has to be readable. 
		 * Tick Read/Write Enabled option for the Texture in the inspector
		 */
        notification.SetLargeIcon(LargeIcon);

        if (_number > 1)
            notification.SetNumber(_number);

        notification.SetSticky(_sticky);

        NotificationManager.ShowNotification(_number, notification);
        NotificationManager.ShowNotification(_number, notification, info.time);
        ++_number;
    }
}
