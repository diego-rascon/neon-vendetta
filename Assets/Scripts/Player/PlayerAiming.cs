using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    RaycastWeapon weapon;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.StartFiring();
        }

        if (weapon.isFiring)
        {
            weapon.UpdateFiring(Time.deltaTime);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            weapon.StopFiring();
        }
    }
}
