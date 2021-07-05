using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        var playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementAutomatic>();

        if (Input.GetKeyDown(KeyCode.S) && playerScript.shootAmmo > 0)
        {
            playerScript.shootAmmo -= 1;
            Shoot();
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
