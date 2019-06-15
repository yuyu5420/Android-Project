using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateControl : MonoBehaviour
{
    private Animator UnityChanSelf;
    public GameObject BackgroundImage;
    private VirtualJoystick joystick;
    public bool  WalkF_bool/*, WalkB_bool, WalkL_bool, WalkR_bool*/;
    // Start is called before the first frame update
    void Start()
    {
        joystick = BackgroundImage.GetComponent<VirtualJoystick>();
        UnityChanSelf = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;
            
            dir.x = joystick.Horizontal();
            dir.z = joystick.Vertical();
            if (dir.x != 0 && dir.z != 0){
                WalkF_bool = true;
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
