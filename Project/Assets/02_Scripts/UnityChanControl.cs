using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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
    private float y = 0, yy = 0, theta = 0, A_dot_B, ALen_mul_BLen;
    private Quaternion qua, rotation;
    // Start is called before the first frame update
    void Start()
    {
        UnityChan = this.GetComponent<CharacterController>();
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
       
        /* Debug.Log("dirx=" + dir.x);
         Debug.Log("dirz=" + dir.z);
         Vector2 A = new Vector2(dir.x, dir.z);
         Vector2 B = new Vector2(1, 0);
         A_dot_B = Vector2.Dot(A, B);
         ALen_mul_BLen = A.magnitude + B.magnitude;
         // 基本上A & B皆不應該為零，就略過檢查分母為零的狀況
         if(ALen_mul_BLen != 0) {

             theta = (float)Math.Acos(A_dot_B / ALen_mul_BLen);
             Debug.Log("theta=" + theta);
             y = (float)(theta * 180.0 / Math.PI);
             Debug.Log(y);
         }


         moveDirection = new Vector3((float)(Math.Cos(theta) * Math.Abs(dir.x)), 0, (float)(Math.Sin(theta)* Math.Abs(dir.z)));
           moveDirection = transform.TransformDirection(moveDirection);
       y = (float)(theta / Math.PI * 180.0 * Time.deltaTime);

       UnityChan.Move(moveDirection * Time.deltaTime);*/

        //  }
        /* 
         if (y != 0)
         {transform.Rotate(0, 45, 0);
             
             MainCamera.GetComponent<Transform>().Rotate(0, y, 0);

             
             yy += y;
             if (yy >= 360) yy -= 360;
             
             
         }

     */
    }
}
