using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idle : MovementBase
{
    public override void EnterState(PlayerController movement)
    {

    }
    public override void UpdateState(PlayerController movement)
    {
        if (movement.direction.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) movement.SwitchState(movement.run);
            else movement.SwitchState(movement.walk);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            movement.SwitchState(movement.jump);
        }
        // if (Input.GetKey(KeyCode.Mouse1)) movement.SwitchState(movement.aim);
    }
}
