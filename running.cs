using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class running : MovementBase
{
    public override void EnterState(PlayerController movement)
    {
        movement.anim.SetBool("Running", true);
    }
    public override void UpdateState(PlayerController movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.walk);
        else if (movement.direction.magnitude < 0.1f) ExitState(movement, movement.idleState);

        if (movement.vInput < 0) movement.currentMoveSpeed = movement.runBackSpeed;
        else movement.currentMoveSpeed = movement.runSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            ExitState(movement, movement.jump);
        }
    }
    void ExitState(PlayerController movement, MovementBase state)
    {
        movement.anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
