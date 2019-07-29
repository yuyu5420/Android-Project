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
   public GameObject Smap;
    public GameObject Coin;
    public GameObject Pause;
    public GameObject button;
    public GameObject coinsound, LoseSound, WinSound, AttackSound;
    public GameObject Success;
    public GameObject cube1, cube2,goal1, goal2, goal3;


    void Awake () {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(MainCamera);
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
    
}