using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SMap : MonoBehaviour
{
    public RenderTexture floor1, floor2;
    RawImage m_RawImage;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the RawImage component from the GameObject
        m_RawImage = GetComponent<RawImage>();

    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "SceneMap"){
            m_RawImage.texture = floor1;
        }
        else if(SceneManager.GetActiveScene().name == "SceneMap2")
        {
             m_RawImage.texture = floor2;
        }
    }
}
