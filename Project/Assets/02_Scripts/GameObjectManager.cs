using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class GameObjectManager : MonoBehaviour
{
   static GameObjectManager instance;
   public GameObject MainCamera;
   public GameObject JoyStick;
   public GameObject JoyStick2;
   public GameObject FB;
   public GameObject UnityChan;
   public GameObject Setting;
    public GameObject Coin;
    public GameObject Pause;
    public GameObject button;
    public GameObject coinsound;
    public GameObject Success;


    void Awake () {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(MainCamera);
            DontDestroyOnLoad(Coin);
            DontDestroyOnLoad(button);
            DontDestroyOnLoad(Coin);
            DontDestroyOnLoad(JoyStick);
            DontDestroyOnLoad(JoyStick2);
            DontDestroyOnLoad(FB);
            DontDestroyOnLoad(UnityChan);
            DontDestroyOnLoad(Setting);
            DontDestroyOnLoad(Pause);
             DontDestroyOnLoad(coinsound);
             DontDestroyOnLoad(Success);

        }

}
}