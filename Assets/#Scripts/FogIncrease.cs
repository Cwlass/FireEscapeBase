using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogIncrease : MonoBehaviour {

    float timer = 0f;
	
	void Update () {

        if (Time.time > 3f && timer < 10f)
        {
            timer += Time.deltaTime;

            RenderSettings.fogDensity = timer * 0.02f;
            RenderSettings.fog = true;
        }
    }
}
