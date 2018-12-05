using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour {
    bool staffOut = false;
    public bool hideGun;
    public float staffRange = 15f;
	// Use this for initialization
	void Awake ()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!staffOut)
            {
                staffOut = true;
                hideGun = true;
                this.gameObject.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                staffOut = false;
                hideGun = false;
                this.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
	}
}
