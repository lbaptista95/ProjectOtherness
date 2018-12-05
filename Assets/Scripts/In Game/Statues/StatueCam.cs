using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCam : MonoBehaviour {

    // Use this for initialization
    GameObject player;
	void Start () {
        player = GameObject.Find("June(Clone)");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform.position);
        transform.position.Set(transform.position.x + 0.01f, transform.position.y, transform.position.z);
	}
}
