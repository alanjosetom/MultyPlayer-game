using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisarmState : AimBase
{

    public override void EnterState(SplWeapon aim)
    {
        aim.anim.SetBool("Aiming", false);
        // aim.currentFov = aim.idleFov;
    }
    public override void UpdateState(SplWeapon aim)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
    }
}
