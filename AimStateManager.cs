using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    AimBase currentState;
    public DisarmState disArm = new DisarmState();
    public AimState Aim = new AimState();
    [SerializeField]
    // float mouseSense = 1.0f;
    // public Cinemachine.AxisState xAxis, yAxis;
    float xAxis, yAxis;
    [SerializeField] Transform camPos;
    [HideInInspector] public Animator anim;
    // [HideInInspector] public Camera vCam;
    // public float camFlow = 40;
    // [HideInInspector] public float idleFov;
    // [HideInInspector] public float currentFov;
    // public float smoothSpeed = 10;
    // public Transform aimPos;
    // [HideInInspector] public Vector3 actualAimPos;
    // [SerializeField] float aimSmoothspeed = 20;
    // [SerializeField] LayerMask aimMask;
    // Start is called before the first frame update
    void Start()
    {
        // vCam = GetComponentInChildren<Camera>();
        // idleFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(disArm);
        // GameObject cam = GameObject.FindGameObjectWithTag("cam");
        // camPos = cam.transform;

    }

    // Update is called once per frame
    void Update()
    {
        // xAxis.Update(Time.deltaTime);
        // yAxis.Update(Time.deltaTime);
        // xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        // yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        // yAxis = Mathf.Clamp(yAxis, -80, 80);
        // currentState.UpdateState(this);
        // vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, smoothSpeed * Time.deltaTime);
        // Vector2 screenCent = new Vector2(Screen.width / 2, Screen.height / 2);
        // Ray ray = Camera.main.ScreenPointToRay(screenCent);
        // if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        // {
        //     aimPos.position = Vector3.Lerp(aimPos.position, hit.point, smoothSpeed * Time.deltaTime);
        // }
    }
    private void LateUpdate()
    {
        // camPos.localEulerAngles = new Vector3(yAxis.Value, camPos.localEulerAngles.y, camPos.localEulerAngles.z);
        // transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
        // camPos.localEulerAngles = new Vector3(yAxis, camPos.localEulerAngles.y, camPos.localEulerAngles.z);
        // transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }
    public void SwitchState(AimBase state)
    {
        currentState = state;
        // currentState.EnterState(this);
    }
}
