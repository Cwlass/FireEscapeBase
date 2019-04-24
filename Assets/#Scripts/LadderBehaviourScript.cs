using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBehaviourScript : MonoBehaviour {
	AudioSource metalSound, engineSound;
	AudioSource  ladderSound;
	bool startCrane, engineStarted,engineStarted2, engineSoundIsPlaying, metalSoundIsPlaying,landedOnGround ;
	int craneAnimationCounter;
	public Component engine;
	public Component ladder;
	public Component person;


    void Start () {
		startCrane = false;
		engineStarted = false;
		engineStarted2 = false;
		craneAnimationCounter = 0;
		engineSoundIsPlaying = false;
		metalSoundIsPlaying = false;
		landedOnGround= false;
		metalSound = this.GetComponent<AudioSource> ();
	}
	

	void Update () {
		if (startCrane) {
			int cuePoint1 = 150;
			int cuePoint2 = 1600;

			craneAnimationCounter++;
			if (craneAnimationCounter > cuePoint1) {
				if (engine) {
					if (engineStarted == false) {
					//Debug.Log ("Engine started");
					metalSound.Pause ();
						engineSound = engine.GetComponent<AudioSource> ();
						engineSound.loop = true;
						engineSound.Play ();
						engineStarted = true;
					}
				}

				Vector3 a = new Vector3(-0.0040f, 0f, 0f); //Move horizontal
				this.transform.Translate (a);
				person.transform.Translate (a.z, a.y, a.x); //x and z are switched for crane and person

				// Remove fog when going away from building
				RenderSettings.fogDensity = RenderSettings.fogDensity / craneAnimationCounter * cuePoint1;
				if (RenderSettings.fogDensity<0.0001)
					RenderSettings.fog = false;

				if (person.transform.position.z < -21.20f) {//STOP for a moment
					metalSound.UnPause ();
//temp					engineSound.Pause (); 
					this.transform.Translate (-a.x, a.y - 0.00f, a.z);//Undo horizontal translation.Stop vertical
					person.transform.Translate (a.z, a.y - 0.00f, -a.x);

					//metalSound.loop = false;

					if (craneAnimationCounter > (cuePoint2 + 210)) {//Go Down
//temp						engineSound.UnPause (); 
						//metalSoundIsPlaying = false;
						if (person.transform.position.y > -105.7) {//check if on ground
							
							this.transform.Translate (0.0f, 0.0f - 0.03f, 0.0f); //Undo horizontal translation. And go down
							person.transform.Translate (0.0f, 0.0f - 0.03f, 0.0f);
						} else {// On the ground
							landedOnGround=true;
							startCrane = false;
							this.transform.Translate (0.0f, 0.0f - 0.00f, 0.0f);//Undo horizontal translation.Stop vertical
							person.transform.Translate (0.0f, 0.0f - 0.00f, 0.0f);
							//temp engineSound.Pause (); 

						}
						//Debug.Log ("crane goin down");
						if (engineStarted2 == false) {
							engineStarted2 = true;
							ladderSound = ladder.GetComponent<AudioSource> ();
							ladderSound.loop = true;
							ladderSound.Play ();
						}
					}
				}
			}
		

		}
	}

	void OnTriggerEnter(Collider other){
		if(!startCrane)
		{
			metalSound = this.GetComponent<AudioSource> ();
			metalSound.loop = true;
			metalSound.Play ();
			startCrane = true;
			//Debug.Log ("crane activated");
		}
}
	void OnTriggerExit(Collider other){
		metalSound.Stop();
	}
}
