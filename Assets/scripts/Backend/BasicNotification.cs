using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNotification : MonoBehaviour
{
    public const int NotificationID = 1;

    public string _contentTitle = "";
    public string _contentText = "";
    public Texture2D LargeIcon;
    public int _number = 1;
    public Color32 _notifyColor;
    private bool _sticky = false;

    public void Notify(TherapiePlan.NotificationInfo[] infoStack)
    {
        NotificationManager.CancelAll();
        foreach (TherapiePlan.NotificationInfo info in infoStack)
        {
            Notify(info);
        }
    }

    public void Notify(TherapiePlan.NotificationInfo info)
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
        notification.SetLights(_notifyColor, 500, 500);

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
