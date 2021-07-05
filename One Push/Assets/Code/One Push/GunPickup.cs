using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            var playerScript = hitInfo.GetComponent<PlayerMovementAutomatic>();
            playerScript.shootAmmo += 1;

            Destroy(gameObject);
        }
            
    }

}
