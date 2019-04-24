using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEndZoneTrigger: MonoBehaviour {

    bool startTimer = false;
    [SerializeField] float timerEndTime = 10f;
    float timer = 0f;

    void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
            Debug.Log("timer tick :" + timer);

            if (timer >= timerEndTime)
            {
                SceneLoader sceneLoader = gameObject.GetComponentInParent(typeof(SceneLoader)) as SceneLoader;
                sceneLoader.sceneCompleted = true;
                //Debug.Log("would load the level now");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        startTimer = true;
        Debug.Log("Timer started. Collided with " + other.gameObject.name);
    }
}
