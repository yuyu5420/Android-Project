using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

using OneSignalPush.MiniJSON;


public class GameObjectManager : MonoBehaviour
{
   static GameObjectManager instance;
   public GameObject MainCamera;
   public GameObject JoyStick;
   public GameObject JoyStick2;
   public GameObject FB;
   public GameObject UnityChan;
   public GameObject Setting;
   public GameObject Smap;
   public GameObject dialog;
    public GameObject Coin;
    public GameObject Pause;
    public GameObject button;
    public GameObject coinsound, LoseSound, WinSound, AttackSound;
    public GameObject Success;
    public GameObject cube1, cube2,goal1, goal2, goal3;

    private static string extraMessage;
   public string email = "Email Address";
   public string externalId = "External User ID";

   private static bool requiresUserPrivacyConsent = false;


    void Awake () {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(MainCamera);
            DontDestroyOnLoad(dialog);
            DontDestroyOnLoad(Coin);
            DontDestroyOnLoad(button);
            DontDestroyOnLoad(Smap);
            DontDestroyOnLoad(JoyStick);
            DontDestroyOnLoad(JoyStick2);
            DontDestroyOnLoad(FB);
            DontDestroyOnLoad(UnityChan);
            DontDestroyOnLoad(Setting);
            DontDestroyOnLoad(Pause);
             DontDestroyOnLoad(coinsound);
             DontDestroyOnLoad(LoseSound);
             DontDestroyOnLoad(WinSound);
             DontDestroyOnLoad(AttackSound);
             DontDestroyOnLoad(Success);
             DontDestroyOnLoad(cube1);
             DontDestroyOnLoad(cube2);
             DontDestroyOnLoad(goal1);
             DontDestroyOnLoad(goal2);
             DontDestroyOnLoad(goal3);

        }

    }

    void Start () {
      extraMessage = null;

      // Enable line below to debug issues with setuping OneSignal. (logLevel, visualLogLevel)
      OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE, OneSignal.LOG_LEVEL.NONE);

      // If you set to true, the user will have to provide consent
      // using OneSignal.UserDidProvideConsent(true) before the
      // SDK will initialize
      OneSignal.SetRequiresUserPrivacyConsent(requiresUserPrivacyConsent);

      // The only required method you need to call to setup OneSignal to receive push notifications.
      // Call before using any other methods on OneSignal (except setLogLevel or SetRequiredUserPrivacyConsent)
      // Should only be called once when your app is loaded.
      // OneSignal.Init(OneSignal_AppId);
      OneSignal.StartInit("241adf49-a128-4b4b-830a-5edd482602b3")
               .HandleNotificationReceived(HandleNotificationReceived)
               .HandleNotificationOpened(HandleNotificationOpened)
               .HandleInAppMessageClicked(HandlerInAppMessageClicked)
               .EndInit();
      
      OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
      OneSignal.permissionObserver += OneSignal_permissionObserver;
      OneSignal.subscriptionObserver += OneSignal_subscriptionObserver;
      OneSignal.emailSubscriptionObserver += OneSignal_emailSubscriptionObserver;

      var pushState = OneSignal.GetPermissionSubscriptionState();

      OneSignalInAppMessageTriggerExamples();
   }
 private void OneSignalInAppMessageTriggerExamples() {
      // Add a single trigger
      OneSignal.AddTrigger("key", "value");

      // Get the current value to a trigger by key
      var triggerValue = OneSignal.GetTriggerValueForKey("key");

      // Add multiple triggers
      OneSignal.AddTriggers(new Dictionary<string, object>() { { "key1", "value1" }, { "key2", 2 } });

      // Delete a trigger
      OneSignal.RemoveTriggerForKey("key");

      // Delete a list of triggers
      OneSignal.RemoveTriggersForKeys(new List<string>() { "key1", "key2" });

      // Temporarily puase In-App messages; If true is passed in.
      // Great to ensure you never interrupt your user while they are in the middle of a match in your game.
      OneSignal.PauseInAppMessages(false);
   }

   private void OneSignal_subscriptionObserver(OSSubscriptionStateChanges stateChanges) {
	  Debug.Log("SUBSCRIPTION stateChanges: " + stateChanges);
	  Debug.Log("SUBSCRIPTION stateChanges.to.userId: " + stateChanges.to.userId);
	  Debug.Log("SUBSCRIPTION stateChanges.to.subscribed: " + stateChanges.to.subscribed);
   }

	private void OneSignal_permissionObserver(OSPermissionStateChanges stateChanges) {
	  Debug.Log("PERMISSION stateChanges.from.status: " + stateChanges.from.status);
	  Debug.Log("PERMISSION stateChanges.to.status: " + stateChanges.to.status);
   }

	private void OneSignal_emailSubscriptionObserver(OSEmailSubscriptionStateChanges stateChanges) {
		Debug.Log("EMAIL stateChanges.from.status: " + stateChanges.from.emailUserId + ", " + stateChanges.from.emailAddress);
		Debug.Log("EMAIL stateChanges.to.status: " + stateChanges.to.emailUserId + ", " + stateChanges.to.emailAddress);
	}

   // Called when your app is in focus and a notificaiton is recieved.
   // The name of the method can be anything as long as the signature matches.
   // Method must be static or this object should be marked as DontDestroyOnLoad
   private static void HandleNotificationReceived(OSNotification notification) {
      OSNotificationPayload payload = notification.payload;
      string message = payload.body;

      print("GameControllerExample:HandleNotificationReceived: " + message);
      print("displayType: " + notification.displayType);
      extraMessage = "Notification received with text: " + message;

   Dictionary<string, object> additionalData = payload.additionalData;
   if (additionalData == null) 
      Debug.Log ("[HandleNotificationReceived] Additional Data == null");
   else
      Debug.Log("[HandleNotificationReceived] message "+ message +", additionalData: "+ Json.Serialize(additionalData) as string);
   }
   
   // Called when a notification is opened.
   // The name of the method can be anything as long as the signature matches.
   // Method must be static or this object should be marked as DontDestroyOnLoad
   public static void HandleNotificationOpened(OSNotificationOpenedResult result) {
       SceneManager.LoadScene("OneSignalExampleScene");
      OSNotificationPayload payload = result.notification.payload;
      string message = payload.body;
      string actionID = result.action.actionID;
GameObject.Destroy(GameObject.Find("Camera2"));
        GameObject.Destroy(GameObject.Find("VirtualJoystick"));
        GameObject.Destroy(GameObject.Find("VirtualJoystick (1)"));
        GameObject.Destroy(GameObject.Find("Coin"));
        GameObject.Destroy(GameObject.Find("FB"));
        ///GameObject.Destroy(GameObject.Find("unitychan"));
        GameObject.Destroy(GameObject.Find("Facebook"));
        GameObject.Destroy(GameObject.Find("Pause"));
        GameObject.Destroy(GameObject.Find("CoinSound"));
        GameObject.Destroy(GameObject.Find("Fire"));
        GameObject.Destroy(GameObject.Find("Success"));
        GameObject.Destroy(GameObject.Find("status"));
        GameObject.Destroy(GameObject.Find("Cube"));
        GameObject.Destroy(GameObject.Find("Cube (1)"));
        GameObject.Destroy(GameObject.Find("FireAttackSound"));
        GameObject.Destroy(GameObject.Find("LoseSound"));
        GameObject.Destroy(GameObject.Find("WinSound"));
        GameObject.Destroy(GameObject.Find("goal1"));
        GameObject.Destroy(GameObject.Find("goal2"));
        GameObject.Destroy(GameObject.Find("goal3"));
      print("GameControllerExample:HandleNotificationOpened: " + message);
      extraMessage = "Notification opened with text: " + message;
      
      Dictionary<string, object> additionalData = payload.additionalData;
      if (additionalData == null) 
         Debug.Log ("[HandleNotificationOpened] Additional Data == null");
      else
         Debug.Log("[HandleNotificationOpened] message "+ message +", additionalData: "+ Json.Serialize(additionalData) as string);

      if (actionID != null) {
         // actionSelected equals the id on the button the user pressed.
         // actionSelected will equal "__DEFAULT__" when the notification itself was tapped when buttons were present.
         extraMessage = "Pressed ButtonId: " + actionID;
      }
      
	}

   public static void HandlerInAppMessageClicked(OSInAppMessageAction action) {
      String logInAppClickEvent = "In-App Message opened with action.clickName " + action.clickName;
      print(logInAppClickEvent);
      extraMessage = logInAppClickEvent;
      
   }
    
}