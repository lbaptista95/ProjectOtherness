using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour {

    // Use this for initialization
    public string _sceneName;
    GameObject saveManager;
	void Start () {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager");
	}
	
	// Update is called once per frame
	void Update () {
        if (saveManager.GetComponent<SaveManager>().gameLoaded)
            saveManager.GetComponent<SaveManager>().gameLoaded = false;
        _sceneName = saveManager.GetComponent<SaveManager>().nextScene;
        SceneManager.LoadScene(_sceneName);
        
	}
}
