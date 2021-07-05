using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    //Make GameObject persistent between Scenes
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
