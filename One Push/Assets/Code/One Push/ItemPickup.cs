using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private GameController controller;

    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Reverses all (in)visible objects after pickup
        if (col.gameObject.tag == "ItemPickup")
        {
            Destroy(gameObject);
            controller.reverser *= -1;          
        }
    }
}
