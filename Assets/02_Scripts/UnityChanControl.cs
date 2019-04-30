using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class UnityChanControl : MonoBehaviour
{
    private CharacterController UnityChan;
    private Vector3 moveDirection = Vector3.zero;
    public GameObject MainCamera;
    private Vector3 UnityChanPosition;
    private Vector3 CameraFollowVector;
    private float y, yy = 0;
    // Start is called before the first frame update
    void Start()
    {
        UnityChan = this.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<AnimateControl>().WalkF_bool == true)
        {

             moveDirection = new Vector3(0, 0, 2.5f);
            //moveDirection = new Vector3(2.5f, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);

        }
       /*y = 45 * Time.deltaTime;
        transform.Rotate(0, y, 0);
        MainCamera.GetComponent<Transform>().Rotate(0, y, 0);*/
        UnityChan.Move(moveDirection * Time.deltaTime);
        UnityChanPosition = this.GetComponent<Transform>().position;
        yy += y;
        if (yy >= 360) yy -= 360;
        float a = (float)(Math.Sin(yy * Math.PI / 180.0));
        float b = (float)(Math.Cos(yy * Math.PI / 180.0));
        CameraFollowVector = new Vector3(UnityChanPosition.x - 2.0f * a , 1.15f, UnityChanPosition.z - 2.0f * b );
        MainCamera.GetComponent<Transform>().position = CameraFollowVector;
        

    }
}
