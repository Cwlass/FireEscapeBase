using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    bool alarmActivated = false;

    bool lightOn = false;
    int lightAnimCounter = 0;
    int lightCycles = 0;

    float alarmTime = 0f;

    [SerializeField] GameObject ceilingSmokeLight;
    [SerializeField] GameObject ceilingSmokeHeavy;

    [SerializeField] GameObject sirenSpeakers;
    [SerializeField] GameObject[] emergencyLights;



    void Update()
    {
        if (alarmActivated)
        {
            if (lightCycles < 10)
            {
                PlayLightAnimation();
            }

            /*
            if (ceilingSmokeLight.activeSelf && Time.time > alarmTime + 20f)
            {
                ceilingSmokeLight.SetActive(false);
            }
            */
        }
    }


    public void ActivateAlarm()
    {
        if (!alarmActivated)
        {
            alarmTime = Time.time;
            alarmActivated = true;
            PlayAlarmSound();
            ceilingSmokeHeavy.SetActive(true);
        }
    }


    void PlayAlarmSound()
    {
        AudioSource au = sirenSpeakers.GetComponent<AudioSource>();
        au.loop = true;
        au.Play();
    }

    void PlayLightAnimation()
    {
        //Debug.Log("light animation");
        if (lightOn == false)
        {
            lightAnimCounter++;
            if (lightAnimCounter > (15 - lightCycles))
            {
                lightCycles++;
                //RenderSettings.fogDensity = lightCycles / 40.0f;
                //RenderSettings.fog = true;

                for (int i = 0; i < emergencyLights.Length; i++)
                {
                    emergencyLights[i].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                    AudioSource au = emergencyLights[i].GetComponent<AudioSource>();
                    au.loop = false;
                    au.Play();
                }

                //Debug.Log("lights on");
                lightOn = true;
            }
        }
        if (lightOn == true)
        {
            lightAnimCounter = lightAnimCounter - 2;
            if (lightAnimCounter <= 0)
            {
                for (int i = 0; i < emergencyLights.Length; i++)
                {
                    emergencyLights[i].GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                }

                //Debug.Log("lights off");
                lightOn = false;
            }
        }

    }
}
