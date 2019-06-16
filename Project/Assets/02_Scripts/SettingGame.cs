using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingGame : MonoBehaviour
{

    public Transform canvas;
    public Transform canvas2;
    
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Debug.Log("Button Clicked. ClickHandler.");
        Pause();
    }
    public void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false && canvas2.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            
        }
        else if(canvas.gameObject.activeInHierarchy == true || canvas2.gameObject.activeInHierarchy == true)
        {
            canvas.gameObject.SetActive(false);
            canvas2.gameObject.SetActive(false);
            Time.timeScale = 1;        
            }
    }
}
