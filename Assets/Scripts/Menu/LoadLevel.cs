using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	// Use this for initialization
    public string scName;
    public GameObject sManager;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {        
            sManager = GameObject.FindGameObjectWithTag("SaveManager");
            SceneManager.LoadScene(sManager.GetComponent<SaveManager>().nextScene);        
	}
}
