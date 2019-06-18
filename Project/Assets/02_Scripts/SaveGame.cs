using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    private Vector3 UnityChanPosition;
    public GameObject UnityChan;
    public GameObject RealGold;
    
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        
    }
    
    private void save(){

        UnityChanPosition = UnityChan.GetComponent<Transform>().position;
        FileStream fs = new FileStream(Application.persistentDataPath + "/save.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(UnityChanPosition.x);
        sw.WriteLine(UnityChanPosition.y);
        sw.WriteLine(UnityChanPosition.z);
        sw.WriteLine(UnityChan.GetComponent<Transform>().rotation);
        sw.WriteLine(SceneManager.GetActiveScene().name);
        sw.WriteLine(RealGold.GetComponent<Text>().text);
        sw.Close();
        fs.Close();

    }
    private void OnClick()
    {
        Debug.Log("Button Clicked.SAVE.");
        /* if (System.IO.File.Exists(Application.dataPath + "/save.txt"))
            System.IO.File.Delete(Application.dataPath + "/save.txt");*/
        
        save();
        GameObject.Destroy(GameObject.Find("Camera2"));
        GameObject.Destroy(GameObject.Find("VirtualJoystick"));
        GameObject.Destroy(GameObject.Find("Coin"));
        GameObject.Destroy(GameObject.Find("FB"));
        GameObject.Destroy(GameObject.Find("unitychan"));
        GameObject.Destroy(GameObject.Find("Setting"));
        GameObject.Destroy(GameObject.Find("Pause"));
        GameObject.Destroy(GameObject.Find("GameController"));
        Application.Quit();
    }

}
