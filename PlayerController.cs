using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using UnityEngine.UI;
public class PlayerController : MonoBehaviourPunCallbacks, IDamagable
{
    [SerializeField] Image healthbarImg;
    [SerializeField] GameObject ui;
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSensitivity, sprintSpeed, jumbForce, smoothTime;
    [SerializeField] Item[] items;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float hInput, vInput;
    CharacterController playerControllerObj;
    // [SerializeField] float yGround;
    // [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float jumpForce = 200;
    // [HideInInspector] public bool jumped;
    Vector3 playerPos;
    Vector3 Velocity;
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float aimSpeed = 2, aimBackSpeed = 1;
    int itemIndex;
    int previousIndex = -1;
    Rigidbody rb;
    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMove;
    Vector3 moveAmount;
    PhotonView PV;
    const float maxHealth = 100f;
    float currentHealth = maxHealth;
    PlayerManager playerManager;
    public MovementBase currentState;
    public idle idleState = new idle();
    public walking walk = new walking();
    // public aiming aim = new aiming();

    // public Paim aim = new Paim();
    public running run = new running();
    public MovementBase previousState;
    public Jumbing jump = new Jumbing();
    [HideInInspector] public Animator anim;
    public float airSpeed = 1.5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        playerControllerObj = GetComponent<CharacterController>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
        anim = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            SwitchState(idleState);
            EquipItem(0);
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
            Destroy(ui);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;
        Look();
        Move();
        Jump();
        // Gravity();
        // FallingFn();
        currentState.UpdateState(this);
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(itemIndex + 1);
            }

        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (itemIndex <= 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            items[itemIndex].Use();
        }
        if (transform.position.y < -10f)
        {
            Die();
        }
    }
    public void SwitchState(MovementBase state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
    void Move()
    {
        // Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        // moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMove, smoothTime);
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        // Vector3 airDirection = Vector3.zero;
        // if (!IsGround()) airDirection = transform.forward * vInput + transform.right * hInput;
        // else direction = transform.forward * vInput + transform.right * hInput;

        direction = transform.forward * vInput + transform.right * hInput;
        playerControllerObj.Move(direction * currentMoveSpeed * Time.deltaTime);
        anim.SetFloat("hzInput", hInput);
        anim.SetFloat("vInput", vInput);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
    // public bool IsGround()
    // {
    //     playerPos = new Vector3(transform.position.x, transform.position.y - yGround, transform.position.z);
    //     if (Physics.CheckSphere(playerPos, playerControllerObj.radius - 0.05f, groundMask)) return true;
    //     return false;
    // }
    // void Gravity()
    // {
    //     if (!IsGround())
    //     {
    //         Velocity.y += gravity * Time.deltaTime;

    //     }
    //     else if (Velocity.y < 0) { Velocity.y = -2; }
    //     playerControllerObj.Move(Velocity * Time.deltaTime);
    // }
    // void FallingFn() => anim.SetBool("Falling", !IsGround());
    // public void JumpForce() => Velocity.y += jumpForceobj;

    // public void Jumped() => jumped = true;
    void EquipItem(int _index)
    {
        if (_index == previousIndex)
            return;
        itemIndex = _index;
        items[itemIndex].itemObject.SetActive(true);
        if (previousIndex != -1)
        {
            items[previousIndex].itemObject.SetActive(false);
        }
        previousIndex = itemIndex;
        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("ItemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("itemIndex") && !PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    }
    public void SetGrounded(bool _grounded)
    {
        grounded = _grounded;

    }
    // private void FixedUpdate()
    // {
    //     if (!PV.IsMine)
    //         return;
    //     rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    // }
    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_TakeDamage", PV.Owner, damage);
    }
    [PunRPC]
    void RPC_TakeDamage(float damage, PhotonMessageInfo info)
    {
        currentHealth -= damage;
        healthbarImg.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
            PlayerManager.Find(info.Sender).GetKill();
        }
    }
    void Die()
    {
        playerManager.Die();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerPos, playerControllerObj.radius - 0.05f);
    }
}
