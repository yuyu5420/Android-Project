using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Grow the fire in the X & Y directions
        transform.localScale += new Vector3(Time.deltaTime/ speed, Time.deltaTime/ speed, 0f);
    }
}
