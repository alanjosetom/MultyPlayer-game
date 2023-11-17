using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SplWeapon : Bow
{
    [SerializeField] Camera cam;
    PhotonView PV;
    [SerializeField] Transform camPos;
    public Animator anim;
    AimBase currentState;
    public DisarmState disArm = new DisarmState();
    public AimState Aim = new AimState();
    // public Transform aimPos;
    // [HideInInspector] public Vector3 actualAimPos;
    [SerializeField] float aimSmoothspeed = 20;
    [SerializeField] LayerMask aimMask;
    public float smoothSpeed = 10;
    [SerializeField] Transform spawnPos;
    [SerializeField] float aVelocity;
    public WeaponAmmo ammo;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    private void Start()
    {
        SwitchState(disArm);
    }
    private void Update()
    {
        currentState.UpdateState(this);

    }
    public override void Use()
    {
        if (ammo.currentAmmo > 0)
        {
            Shoot();
        }
        else if (ammo.currentAmmo == 0)
        {
            Debug.Log("collect some arrows");
        }

    }
    void Shoot()
    {

        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f));
        ray.origin = cam.transform.position;
        // if (Physics.Raycast(ray, out RaycastHit hit))
        // {
        //     hit.collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(((WeaponInfo)itemInfo).damage);
        //     PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        // }
        // Vector2 screenCent = new Vector2(Screen.width / 2, Screen.height / 2);
        // Ray ray = Camera.main.ScreenPointToRay(screenCent);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            hit.collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(((WeaponInfo)itemInfo).damage);

            // aimPos.position = Vector3.Lerp(spawnPos.position, hit.point, aimSmoothspeed * Time.deltaTime);
            PV.RPC("RPC_Shoot", RpcTarget.All, spawnPos.position, hit.normal);
        }

    }
    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, .3f);
        if (colliders.Length != 0)
        {
            ammo.currentAmmo--;
            GameObject impactObj = Instantiate(arrowImpactPrefab, spawnPos.position, Quaternion.LookRotation(hitNormal, Vector3.up) * arrowImpactPrefab.transform.rotation);
            Destroy(impactObj, 10.0f);
            // impactObj.transform.SetParent(colliders[0].transform);
            Rigidbody rb = impactObj.GetComponent<Rigidbody>();
            rb.AddForce(spawnPos.forward * aVelocity, ForceMode.Impulse);
            anim.SetBool("fire", true);
        }
        anim.SetBool("fire", false);
    }
    public void SwitchState(AimBase state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
