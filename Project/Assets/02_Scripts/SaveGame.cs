using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
    
public class SaveGame : MonoBehaviour
{
    public CharacterController UnityChan;
    private Vector3 UnityChanPosition;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        
    }
    
    private void OnClick()
    {
        Debug.Log("Button Clicked.SAVE.");
        /* if (System.IO.File.Exists(Application.dataPath + "/save.txt"))
            System.IO.File.Delete(Application.dataPath + "/save.txt");*/
        UnityChanPosition = UnityChan.GetComponent<Transform>().position;
        FileStream fs = new FileStream(Application.persistentDataPath + "/save.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(UnityChanPosition.x);
        sw.WriteLine(UnityChanPosition.y);
        sw.WriteLine(UnityChanPosition.z);
        sw.WriteLine(UnityChan.GetComponent<Transform>().rotation);
        sw.Close();
        fs.Close();

    }

}
