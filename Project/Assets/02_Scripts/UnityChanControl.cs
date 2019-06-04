using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;


public class UnityChanControl : MonoBehaviour
{
    private CharacterController UnityChan;
    private Vector3 moveDirection = Vector3.zero;
    public GameObject MainCamera;
    public GameObject BackgroundImage;
    public float smoothing;
    private Vector3 UnityChanPosition;
    private VirtualJoystick joystick;
    private Vector3 CameraFollowVector;
    private Quaternion qua, rotation;
    // Start is called before the first frame update
    void Start()
    {
        FileStream fs = new FileStream(Application.dataPath + "/save.txt", FileMode.Open);
        StreamReader sr = new StreamReader(fs);
 
        UnityChan = this.GetComponent<CharacterController>();
        UnityChanPosition = this.GetComponent<Transform>().position;
        this.transform.position = new Vector3(Convert.ToSingle(sr.ReadLine()), UnityChanPosition.y, Convert.ToSingle(sr.ReadLine()));
        fs.Close();
        Debug.Log(UnityChanPosition.x);
        joystick = BackgroundImage.GetComponent<VirtualJoystick>();
        MainCamera.GetComponent<Transform>().rotation = this.transform.rotation;
        MainCamera.GetComponent<Transform>().eulerAngles = new Vector3(20.0f, MainCamera.GetComponent<Transform>().eulerAngles.y, MainCamera.GetComponent<Transform>().eulerAngles.z);

        UnityChanPosition = this.GetComponent<Transform>().position;
        CameraFollowVector = new Vector3(UnityChanPosition.x, 3.2f, UnityChanPosition.z);
        MainCamera.GetComponent<Transform>().position = CameraFollowVector;
        MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);
    }

    // Update is called once per frame
    void Update()
    {

      //  if (this.GetComponent<AnimateControl>().WalkF_bool == true)
     //   {
            Vector3 dir = Vector3.zero;
            
            dir.x = joystick.Horizontal();
            dir.z = joystick.Vertical();
       // Debug.Log(this.transform.eulerAngles.y);
        rotation = Quaternion.Euler(0, this.transform.eulerAngles.y, 0);
        dir = rotation * dir;
        if (dir.x != 0 && dir.z != 0)
        {
            this.transform.Translate(dir.normalized * Time.deltaTime * 3, Space.World);
            qua = Quaternion.LookRotation(dir.normalized);//※  將Vector3型別轉換四元數型別
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, qua, Time.deltaTime * smoothing);//四元數的插值，實現平滑過渡
            MainCamera.GetComponent<Transform>().rotation = this.transform.rotation;
            MainCamera.GetComponent<Transform>().eulerAngles = new Vector3(20.0f,MainCamera.GetComponent<Transform>().eulerAngles.y, MainCamera.GetComponent<Transform>().eulerAngles.z);
            /*Debug.Log(this.transform.rotation.y);
            Debug.Log(dir.x);
            Debug.Log(dir.z);*/
            UnityChanPosition = this.GetComponent<Transform>().position;
            CameraFollowVector = new Vector3(UnityChanPosition.x , 3.2f, UnityChanPosition.z);
            MainCamera.GetComponent<Transform>().position = CameraFollowVector;
            MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);

        }
       
        
    }
}
