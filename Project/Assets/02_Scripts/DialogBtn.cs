using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogBtn : MonoBehaviour
{
    public GameObject Dialog;
    public TextAsset TxtFile;   //建立TextAsset
    public TextAsset TxtFile2;   //建立TextAsset
    private string Mytxt;       //用来存放文本内容
    private string Mytxt2;       //用来存放文本内容
    public GameObject txt;
    private int count, currentPos;
    // Start is called before the first frame update
    
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        Mytxt = TxtFile.text;
        Mytxt2 = TxtFile2.text;
        count = 0;
        currentPos = 0;
        txt.GetComponent<Text>().text = "點點點";
        
    }
    void Create1(){
        currentPos++;
        txt.GetComponent<Text>().text = Mytxt.Substring(0,currentPos);//刷新文本显示内容

        Debug.Log("YAYAYA");
        if(currentPos<Mytxt.Length) {
            this.Invoke("Create1", 0.2f);
            
        }
        else{
             currentPos = 0;
             Time.timeScale = 0;
        }
    }
    void Create2(){
        currentPos++;
        txt.GetComponent<Text>().text = Mytxt2.Substring(0,currentPos);//刷新文本显示内容

        
        if(currentPos<Mytxt2.Length) {
            this.Invoke("Create2", 0.2f);
        }
        else{
             currentPos = 0;
             Time.timeScale = 0;
        }
    }
    private void OnClick()
    {
        if(count == 0){
            //txt.GetComponent<Text>().text = Mytxt;
            Time.timeScale = 1;
            Create1();

        }
        else if(count == 1){
            Time.timeScale = 1;
             Create2();
        }
        else{
            Debug.Log("???????????????");
            Time.timeScale = 1;
            Dialog.SetActive(false);
            Debug.Log("666666666");
        }
        
         count++;
         
    }


}
