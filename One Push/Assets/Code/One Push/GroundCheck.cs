using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public bool isGrounded;

    void OnTriggerStay2D(Collider2D col)
    {
        //If player touches the ground
        if (col.CompareTag("Ground"))
        {
            isGrounded = true;
            //Debug.Log("Is now grounded");
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            isGrounded = false;
        }       
    }

}
