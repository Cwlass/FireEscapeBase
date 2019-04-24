using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public class FireButtonBehaviour : MonoBehaviour
{
    bool glassBroken = false;

    void OnTriggerEnter(Collider other)
    {
        if (glassBroken == false)
        {
            //Debug.Log("Pressed the Fire Alarm Button");

            glassBroken = true;

            AudioSource breakSound = this.GetComponent<AudioSource>();
            breakSound.loop = false;
            breakSound.Play();

            Alarm alarm = gameObject.GetComponentInParent(typeof(Alarm)) as Alarm;
            alarm.ActivateAlarm();
        }

    }
}
