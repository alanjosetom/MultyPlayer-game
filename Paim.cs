using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paim : MovementBase
{
    public override void EnterState(PlayerController movement)
    {
        movement.anim.SetBool("Aiming", true);
    }
    public override void UpdateState(PlayerController movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.run);
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (movement.direction.magnitude < 0.1f) ExitState(movement, movement.idleState);
            else ExitState(movement, movement.walk);
        }

        if (movement.vInput < 0) movement.currentMoveSpeed = movement.aimBackSpeed;
        else movement.currentMoveSpeed = movement.aimSpeed;
    }
    void ExitState(PlayerController movement, MovementBase state)
    {
        movement.anim.SetBool("Aiming", false);
        movement.SwitchState(state);
    }
}
