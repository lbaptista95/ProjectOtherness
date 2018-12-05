using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRotateHealthBar : MonoBehaviour {

    public Quaternion rotation;
	// Use this for initialization
	void Awake () {
        rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = rotation;
	}
}
