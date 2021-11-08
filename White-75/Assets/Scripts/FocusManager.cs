using System.Collections.Generic;
using UnityEngine;

public class FocusManager: MonoBehaviour
{
   //Sorry for using focus manager to send notification, cause I don't know where to find a NotificationManager, I just hate them and now I have to be the attention graber and I really hate this kind of felling.
    public DataManager dataManager;

    public bool onFocus = false;
    public string oneSignaluserId;

    void Start()
    {
        OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE, OneSignal.LOG_LEVEL.NONE);
        OneSignal.StartInit("1f4b3da5-e6ec-46e0-a4d0-a94c0164e6e4")
   .HandleNotificationOpened(OneSignalHandleNotificationOpened)
   .Settings(new Dictionary<string, bool>() {
      { OneSignal.kOSSettingsAutoPrompt, false },
      { OneSignal.kOSSettingsInAppLaunchURL, false } })
   .EndInit();

        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;

        // iOS - Shows the iOS native notification permission prompt.
        //   - Instead we recomemnd using an In-App Message to prompt for notification 
        //     permission to explain how notifications are helpful to your users.
        OneSignal.PromptForPushNotificationsWithUserResponse(OneSignalPromptForPushNotificationsReponse);

        //See GetPermissionSubscriptionState method: 
        //https://documentation.onesignal.com/v7.0/docs/unity-sdk#getpermissionsubscriptionstate
        var status = OneSignal.GetPermissionSubscriptionState();
        oneSignaluserId= status.subscriptionStatus.userId;
        Debug.Log("OneSignal user id:"+oneSignaluserId);
    }
    private static void OneSignalHandleNotificationOpened(OSNotificationOpenedResult result)
    {
        // Place your app specific notification opened logic here.
    }

    // iOS - Fires when the user anwser the notification permission prompt.
    private void OneSignalPromptForPushNotificationsReponse(bool accepted)
    {
        // Optional callback if you need to know when the user accepts or declines notification permissions.
    }
    private static string oneSignalDebugMessage;

    public void postNotification(string message)
    {
        // Just an example userId, use your own or get it the devices by using the GetPermissionSubscriptionState method
        var notification = new Dictionary<string, object>();
        notification["contents"] = new Dictionary<string, string>() { { "en",message } };

        notification["include_player_ids"] = new List<string>() { oneSignaluserId };
        // Example of scheduling a notification in the future.
        notification["send_after"] = System.DateTime.Now.ToUniversalTime().AddSeconds(30).ToString("U");

        OneSignal.PostNotification(notification, (responseSuccess) => {
            oneSignalDebugMessage = "Notification posted successful! Delayed by about 30 secounds to give you time to press the home button to see a notification vs an in-app alert.\n" + Json.Serialize(responseSuccess);
        }, (responseFailure) => {
            oneSignalDebugMessage = "Notification failed to post:\n" + Json.Serialize(responseFailure);
        });

    }

    void OnApplicationFocus(bool hasFocus)
    {
        onFocus = !hasFocus;
        Debug.Log("Focused");
    }

    void OnApplicationPause(bool pauseStatus)
    {
        onFocus = pauseStatus;
        Debug.Log("Not Focused");
    }

}