using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("ButtonHome Clicked.");
        GameObject.Find("Success").SetActive(false);
        GameObject.Find("goal1").GetComponent<BoxCollider>().isTrigger = false;
        GameObject.Find("goal2").GetComponent<BoxCollider>().isTrigger = false;
        GameObject.Find("goal3").GetComponent<BoxCollider>().isTrigger = false;
        PlayerPrefs.SetString("step", "0");
        GameObject.Destroy(GameObject.Find("Camera2"));
        GameObject.Destroy(GameObject.Find("VirtualJoystick"));
        GameObject.Destroy(GameObject.Find("VirtualJoystick (1)"));
        GameObject.Destroy(GameObject.Find("Coin"));
        GameObject.Destroy(GameObject.Find("FB"));
        GameObject.Destroy(GameObject.Find("unitychan"));
        GameObject.Destroy(GameObject.Find("Setting"));
        GameObject.Destroy(GameObject.Find("Pause"));
        GameObject.Destroy(GameObject.Find("GameController"));
        GameObject.Destroy(GameObject.Find("Cube"));
        GameObject.Destroy(GameObject.Find("Cube (1)"));
        SceneManager.LoadScene("SceneMenu");
    }
}
