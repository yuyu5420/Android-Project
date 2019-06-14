using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Transform canvas;
    public Transform settting;

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
        canvas.gameObject.SetActive(false);
        settting.gameObject.SetActive(true);
        
    }
}