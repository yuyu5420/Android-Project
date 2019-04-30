using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateControl : MonoBehaviour
{
    private Animator UnityChanSelf;
    public bool  WalkF_bool/*, WalkB_bool, WalkL_bool, WalkR_bool*/;
    // Start is called before the first frame update
    void Start()
    {
        UnityChanSelf = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UnityChanSelf.SetBool("WalkF_bool", WalkF_bool);
      //  UnityChanSelf.SetBool("WalkB_bool", WalkB_bool);
      //  UnityChanSelf.SetBool("WalkL_bool", WalkL_bool);
      //  UnityChanSelf.SetBool("WalkR_bool", WalkR_bool);
    }
}
