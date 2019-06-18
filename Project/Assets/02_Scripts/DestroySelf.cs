using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<Transform>().name == "CoinSound(Clone)")
            Destroy(gameObject, 3f);
    }
}
