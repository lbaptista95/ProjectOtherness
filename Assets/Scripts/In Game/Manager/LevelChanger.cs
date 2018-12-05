using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public Animator fadeAnim;
	// Update is called once per frame
	void Update () {
        
	}

    public void FadeToLevel()
    {
        fadeAnim = GetComponent<Animator>();
        fadeAnim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (SceneManager.GetActiveScene().name == "Loading")
        {

        }
        SceneManager.LoadScene("Loading");
    }
}
