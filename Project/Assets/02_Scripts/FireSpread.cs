using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    public float spreadTimeSec;
    public int type;    
    public float gapPixel;
    private int direction;
    private int isDuplicated;
    private Vector3 selfPosition;
    private Quaternion selfRotation;
    private double createTime;
    // Start is called before the first frame update
    void Start()
    {
        isDuplicated = 0;
        selfPosition = this.GetComponent<Transform>().position;
        selfRotation = this.GetComponent<Transform>().rotation;
        createTime = Time.realtimeSinceStartup;
        
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Fire(Clone)"){
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        // Grow the fire in the X & Y directions
        //transform.localScale += new Vector3(Time.deltaTime/ speed, Time.deltaTime/ speed, 0f);

        if( isDuplicated < 4 && Time.realtimeSinceStartup > createTime + spreadTimeSec) {
            
            
            //Debug.Log("Firrrrre");
            FireSpread newFire;
            Vector3 newPosition = this.setNewPosition(isDuplicated);;
            Vector3[] dir = new Vector3[4]{
                transform.TransformDirection(Vector3.right),
                transform.TransformDirection(Vector3.forward),
                transform.TransformDirection(Vector3.left),
                transform.TransformDirection(Vector3.back)};

            if(Physics.OverlapSphere(newPosition, 1).Length <= 0 && !Physics.Raycast(selfPosition, dir[isDuplicated], gapPixel)) 
                Instantiate(GameObject.Find("Fire"), newPosition, selfRotation);
            
            isDuplicated++;
               
        }

    }

    void setDirection(int dir)
    {
        this.direction = dir;
    }

    Vector3 setNewPosition(int dir)
    {
        Vector3 newPosition = selfPosition;
        switch (dir)
        {
            case 0:
            newPosition.x += gapPixel;
            break;
            case 1:
            newPosition.z += gapPixel;
            break;
            case 2:
            newPosition.x -= gapPixel;
            break;
            case 3:
            newPosition.z -= gapPixel;          
            break;  
        }
        return newPosition;
    }
}
