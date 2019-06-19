using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonRestart : MonoBehaviour
{
    public GameObject unitychan;
    public GameObject goldText;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick()
    {
        Debug.Log("ButtonRestart Clicked.");
        FileStream fs = new FileStream(Application.persistentDataPath + "/save.txt", FileMode.Open);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(-5.69);
        sw.WriteLine(-0.93);
        sw.WriteLine(-18.53);
        sw.WriteLine(Quaternion.Euler(0f, 0f, 0f));
        sw.WriteLine("SceneMap");
        sw.WriteLine(goldText.GetComponent<Text>().text);
        sw.Close();
        fs.Close();

        GameObject.Find("next").GetComponent<ButtonHome>().OnClick();
    }
}
