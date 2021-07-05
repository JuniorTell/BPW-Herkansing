using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVisible : MonoBehaviour
{
    private GameController controller;
    public bool startInvisible;
    private int platVisible;

    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //Sets the (in)visibility of each individual platform, using a universal int in the game controller
        if (startInvisible == true)
        {
            platVisible = controller.reverser * -1;
        }
        else
        {
            platVisible = controller.reverser;
        }

        if (platVisible == -1)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
