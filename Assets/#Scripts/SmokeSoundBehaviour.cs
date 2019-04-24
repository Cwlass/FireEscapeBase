using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSoundBehaviour : MonoBehaviour {
	public Component deurTriggrer;
	public bool soundOn;
	AudioSource smokeSound ;
	// Use this for initialization
	void Start () {
		soundOn = false;
		smokeSound = this.GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (soundOn == false) {
			Deur k = deurTriggrer.GetComponent<Deur> ();
				if (k.deurIsOpening) {
				smokeSound.loop = true;
				smokeSound.Play ();
				soundOn = true;
			}
		}
}
}
