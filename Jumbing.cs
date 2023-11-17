using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumbing : MovementBase
{
    public override void EnterState(PlayerController movement)
    {
        if (movement.previousState == movement.idleState) movement.anim.SetTrigger("IdleJump");
        else if (movement.previousState == movement.walk || movement.previousState == movement.run) movement.anim.SetTrigger("RunJump");
    }
    public override void UpdateState(PlayerController movement)
    {
        //     if (movement.jumped && movement.IsGround())
        //     {
        //         movement.jumped = false;
        //         if (movement.hInput == 0 && movement.vInput == 0) movement.SwitchState(movement.idleState);
        //         else if (Input.GetKey(KeyCode.LeftShift)) movement.SwitchState(movement.run);
        //         else movement.SwitchState(movement.walk);
        //     }
    }
}
