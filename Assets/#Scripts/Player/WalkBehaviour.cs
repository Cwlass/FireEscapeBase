using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehaviour : MonoBehaviour {
	public GameObject gun;
	public GameObject fire;
	public GameObject fire2;

	public float jumpLenght = 0.6f;
	// Use this for initialization
	void Start () {

		gun = this.transform.Find("Main Camera").gameObject;
        if (fire != null)
        {
            fire.SetActive(false);
        }
        if (fire2 != null)
        {
            fire2.SetActive(false);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("f")) {// activate the fire
            if (fire != null)
            {
                fire.SetActive(true);
            }
            if (fire2 != null)
            {
                fire2.SetActive(true);
            }
		}

		if (Input.GetButtonDown("Jump")){
			Vector3 r = gun.transform.eulerAngles;
			Vector3 r2 = this.transform.eulerAngles;
			r.y = r.y - r2.y;

			this.transform.Translate(jumpLenght*Mathf.Sin(r.y/180.0f*Mathf.PI),0.0f,jumpLenght*Mathf.Cos(r.y/180.0f*Mathf.PI));

            //Debug.Log("Jumped with r = " + r);
        }

    }
}
