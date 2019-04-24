using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperspeed : MonoBehaviour {

    void Update () {
        if (Input.GetKeyDown("h"))
        {
            Time.timeScale = 8f;
        } else 
        if (Input.GetKeyUp("h")) 
        {
            Time.timeScale = 1f;
        }
	}
}
