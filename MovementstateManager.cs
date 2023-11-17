using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementstateManager : MonoBehaviour
{
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float aimSpeed = 2, aimBackSpeed = 1;

    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float hInput, vInput;
    CharacterController playerController;
    Vector3 playerPos;
    [SerializeField] float yGround;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity = -9.8f;
    Vector3 Velocity;
    MovementBase currentState;

    public idle idleState = new idle();
    public walking walk = new walking();
    // public aiming aim = new aiming();

    // public Paim aim = new Paim();
    public running run = new running();
    [HideInInspector] public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<CharacterController>();
        SwitchState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        DirectionandMove();
        Gravity();
        // currentState.UpdateState(this);
        anim.SetFloat("hInput", hInput);
        anim.SetFloat("vInput", vInput);
    }
    public void SwitchState(MovementBase state)
    {
        currentState = state;
        // currentState.EnterState(this);
    }
    void DirectionandMove()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        direction = transform.forward * vInput + transform.right * hInput;
        playerController.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
    }
    bool IsGround()
    {
        playerPos = new Vector3(transform.position.x, transform.position.y - yGround, transform.position.z);
        if (Physics.CheckSphere(playerPos, playerController.radius - 0.05f, groundMask)) return true;
        return false;
    }
    void Gravity()
    {
        if (!IsGround())
        {
            Velocity.y += gravity * Time.deltaTime;

        }
        else if (Velocity.y < 0) { Velocity.y = -2; }
        playerController.Move(Velocity * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(playerPos, playerController.radius - 0.05f);
    }

}
