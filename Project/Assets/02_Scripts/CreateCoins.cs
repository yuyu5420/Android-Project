using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCoins : MonoBehaviour
{
    public CharacterController UnityChan;
    private Vector3 UnityChanPosition;
    void Start(){
        this.Invoke("Create", 3f);
    }
    void Create()
    {
        UnityChanPosition = UnityChan.GetComponent<Transform>().position;
        float X = UnityChanPosition.x + Random.Range(-15.0F, 15.0F);        
        float Z = UnityChanPosition.z + Random.Range(-15.0F, 15.0F);
        Vector3 A = new Vector3(X, (float)-4.5 , Z);
        Instantiate(GameObject.Find("Coin"),A, transform.rotation);
        Debug.Log("created");
        this.Invoke("Create", 3f);
    }
    
}
