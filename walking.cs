using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MovementBase
{
    public override void EnterState(PlayerController movement)
    {
        movement.anim.SetBool("Walking", true);
    }
    public override void UpdateState(PlayerController movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.run);
        // else if (Input.GetKeyDown(KeyCode.Mouse1)) ExitState(movement, movement.aim);
        else if (movement.direction.magnitude < 0.1f) ExitState(movement, movement.idleState);

        if (movement.vInput < 0) movement.currentMoveSpeed = movement.walkBackSpeed;
        else movement.currentMoveSpeed = movement.walkSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            ExitState(movement, movement.jump);
        }
    }



    void ExitState(PlayerController movement, MovementBase state)
    {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
