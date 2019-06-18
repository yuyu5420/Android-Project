using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateControl : MonoBehaviour
{
    private Animator UnityChanSelf;
    private Vector3 UnityChanPosition;
    public GameObject MainCamera;
    public GameObject BackgroundImage;
    public GameObject BackgroundImage2;
    private VirtualJoystick joystick;
    private VirtualJoystick2 joystick2;
    public float speed;
    public float smoothing;

    private Vector3 CameraFollowVector;
    private Quaternion qua, rotation;
    public bool  WalkF_bool/*, WalkB_bool, WalkL_bool, WalkR_bool*/;
    // Start is called before the first frame update
    void Start()
    {
        joystick = BackgroundImage.GetComponent<VirtualJoystick>();
        joystick2 = BackgroundImage2.GetComponent<VirtualJoystick2>();
        UnityChanSelf = this.GetComponent<Animator>();
        Debug.Log("HELO");
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;
        
        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();
        rotation = Quaternion.Euler(0, this.transform.eulerAngles.y, 0);
        dir = rotation * dir;
        if (dir.x != 0 && dir.z != 0){
            WalkF_bool = true;
            //Debug.Log("WTF= =");
            this.transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
            qua = Quaternion.LookRotation(dir.normalized);//※  將Vector3型別轉換四元數型別
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, qua, Time.deltaTime * smoothing);//四元數的插值，實現平滑過渡
             MainCamera.GetComponent<Transform>().rotation = Quaternion.Lerp(MainCamera.GetComponent<Transform>().rotation, qua, Time.deltaTime * smoothing);
            MainCamera.GetComponent<Transform>().eulerAngles = new Vector3(20.0f,MainCamera.GetComponent<Transform>().eulerAngles.y, MainCamera.GetComponent<Transform>().eulerAngles.z);
            UnityChanPosition = this.GetComponent<Transform>().position;
            CameraFollowVector = new Vector3(UnityChanPosition.x , 3.2f, UnityChanPosition.z);
            MainCamera.GetComponent<Transform>().position = CameraFollowVector;
            MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);
         }
         else{
            WalkF_bool = false;
                
        }
       UnityChanSelf.SetBool("WalkF_bool", WalkF_bool);
      //  UnityChanSelf.SetBool("WalkB_bool", WalkB_bool);
      //  UnityChanSelf.SetBool("WalkL_bool", WalkL_bool);
      //  UnityChanSelf.SetBool("WalkR_bool", WalkR_bool);
    }
}
