using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deur : MonoBehaviour {
    public GameObject pancarte;

    bool deurOpen = false;
    public bool deurIsOpening = false;
    public bool doorIsClosing = false;

    int anim = 0;
    int autoCloseCounter = 0;


	void Start () {
        if (pancarte != null)
        {
            pancarte.SetActive(false);
        }
	}


    void Update () {
			

		if(deurIsOpening==true)
		{
			this.transform.Translate(-0.010f,0f,0f);
			anim++;
			if (anim>=100){
				autoCloseCounter = 0;
				deurOpen = true;
				deurIsOpening = false;
				//Debug.Log ("Deur is open.");
			}
			//Debug.Log ("Deur gaat open");
		} 

		if (deurOpen == true) {
			autoCloseCounter++;
			if (autoCloseCounter >= 25) {

                if (pancarte != null)
                {
                    pancarte.SetActive(true);
                }
            }

            if (autoCloseCounter >= 500) {
				autoCloseCounter = 0;
				deurOpen = false;
				doorIsClosing = true;
				//Debug.Log ("Deur gaat terug toe Trigger");
				AudioSource doorSound = this.GetComponent<AudioSource> ();
				doorSound.loop = false;
				doorSound.Play ();
			}
		}

		if (doorIsClosing == true) {
			this.transform.Translate (+0.010f, 0f, 0f);
			anim--;
			if (anim <= 0) {
				doorIsClosing = false;
                //Debug.Log ("Deur gaat terug toe");

                if (pancarte != null)
                {
                    pancarte.SetActive(false);
                }
            }
        }
	}



	void OnTriggerEnter(Collider other){
		if(deurOpen==false)
		 {
			deurIsOpening = true;
			doorIsClosing = false;
			//Debug.Log ("Deur gaat open");

            AudioSource doorSound = this.GetComponent<AudioSource> ();
			doorSound.loop = false;
			doorSound.Play ();

        }
        else {
			this.transform.Translate(0f,0f,0f);
			//Debug.Log ("Deur is al open.");
		}
	
	}
}
