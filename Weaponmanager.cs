using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponmanager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [SerializeField] GameObject arrow;
    [SerializeField] Transform spawnPos;
    [SerializeField] float aVelocity;
    [SerializeField] int pershot = 1;
    AimStateManager aim;
    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponentInParent<AimStateManager>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire()) Fire();
    }
    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        return false;
    }
    void Fire()
    {
        fireRateTimer = 0;
        // spawnPos.LookAt(aim.aimPos);
        GameObject currentArrow = Instantiate(arrow, spawnPos.position, spawnPos.rotation);
        Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
        rb.AddForce(spawnPos.forward * aVelocity, ForceMode.Impulse);
    }
}
