using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
    
public class ButtonMap: MonoBehaviour
{
    public GameObject camera2;
    public GameObject unitychan;
    public GameObject controller;
    public GameObject fire;
    public GameObject FB;
    public GameObject joystick;
    public GameObject pause;
    public GameObject set;
    public GameObject btn;
    public GameObject coinsound;


    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        Time.timeScale = 0;
    }
    
    private void OnClick()
    {
        camera2.SetActive(true);
        unitychan.SetActive(true);
        joystick.SetActive(true);
        controller.SetActive(true);
        FB.SetActive(true);
        btn.SetActive(true);
        coinsound.SetActive(true);
        //set.SetActive(true);
        //pause.SetActive(true);
        Time.timeScale = 1;
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("SceneMap");
    }

}
