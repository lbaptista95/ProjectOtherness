using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerazinhah : MonoBehaviour {

    // Use this for initialization
    GameObject target;
	void Start () {
        target = GameObject.Find("June(Clone)");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(target.transform.position);
        transform.position.Set(target.transform.position.x, transform.position.y, target.transform.position.z);
	}
}
