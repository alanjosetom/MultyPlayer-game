using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBase
{
    // public Animator anim;

    public override void EnterState(SplWeapon aim)
    {
        aim.anim.SetBool("Aiming", true);
        // aim.currentFov = aim.camFlow;
    }
    public override void UpdateState(SplWeapon aim)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            aim.anim.SetBool("fire", true);

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            aim.anim.SetBool("fire", false);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {

            aim.SwitchState(aim.disArm);
        }
    }
}
