using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;


public class UnityChanControl : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    public GameObject MainCamera;
    public GameObject BackgroundImage;
    public GameObject BackgroundImage2;
    public float speed;
    public float smoothing;
    private Vector3 UnityChanPosition;
    private VirtualJoystick joystick;
    private VirtualJoystick2 joystick2;
    private Vector3 CameraFollowVector;
    private Quaternion qua, rotation;
    private bool run = true;
    // Start is called before the first frame update
    void Start()
    {
         if (System.IO.File.Exists(Application.persistentDataPath + "/save.txt"))
        {
                FileStream fs = new FileStream(Application.persistentDataPath + "/save.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                this.transform.position = new Vector3(Convert.ToSingle(sr.ReadLine()), Convert.ToSingle(sr.ReadLine()), Convert.ToSingle(sr.ReadLine()));
                this.transform.rotation = QuaternionParse(sr.ReadLine());
                sr.Close();
                fs.Close();
        }
        
        joystick = BackgroundImage.GetComponent<VirtualJoystick>();
        joystick2 = BackgroundImage2.GetComponent<VirtualJoystick2>();
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
        Vector3 dir2 = Vector3.zero;

        dir2.x = joystick.Horizontal();
        dir2.z = joystick.Vertical();

        dir.x = joystick2.Horizontal();
        dir.z = joystick2.Vertical();

        //Debug.Log(this.transform.eulerAngles.y);
        rotation = Quaternion.Euler(0, this.transform.eulerAngles.y, 0);
        dir = rotation * dir;
        if (dir.x != 0 && dir.z != 0 && run)
        {
            
            //this.transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
            qua = Quaternion.LookRotation(dir.normalized);//※  將Vector3型別轉換四元數型別
            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, qua, Time.deltaTime * smoothing);//四元數的插值，實現平滑過渡
            MainCamera.GetComponent<Transform>().rotation = Quaternion.Lerp(MainCamera.GetComponent<Transform>().rotation, qua, Time.deltaTime * smoothing);
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

    void OnTriggerExit(Collider collisio) {
	    run = true;
        Debug.Log("EXIT");
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Coin(Clone)"){
            Destroy(collision.gameObject);
        }
        else
        {
            //run = false;
        }
        
    }
    
    Quaternion QuaternionParse(string name)
    {
        name = name.Replace("(", "").Replace(")", "");
        string[] s = name.Split(',');
        return new Quaternion(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));
    }
}
