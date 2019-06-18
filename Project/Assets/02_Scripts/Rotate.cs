using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -7.0F, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, (float)100*Time.deltaTime, 0);
    }
}
