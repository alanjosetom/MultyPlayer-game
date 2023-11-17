using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public float ammo;
    [HideInInspector] public float currentAmmo;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void foundAmmo()
    {
        currentAmmo += 30;
        if (currentAmmo >= 120)
        {
            currentAmmo = 120;
        }

    }
}
