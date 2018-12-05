using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.AI;

public class PlayScene : MonoBehaviour {

    // Use this for initialization
    VideoPlayer videoPlayer;
    AudioSource audioSource;
    public double endVideo;
    GameObject player;
	void Start () {
        audioSource = GetComponent<AudioSource>();
        videoPlayer = GetComponent<VideoPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (videoPlayer.time >= endVideo)
        {
            GameObject.FindGameObjectWithTag("NextLevel").GetComponent<NextLevelWithoutButton>().enabled = true;
            GameObject.Find("HUDCanvas").GetComponent<Canvas>().enabled = true;
            videoPlayer.Stop();
            audioSource.Stop();
            GetComponent<SphereCollider>().enabled = false;
            player.GetComponent<AlternarControles>().enabled = true;
            player.GetComponent<ControleTeclado>().enabled = true;
            player.GetComponent<ControleMouse>().enabled = true;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "June(Clone)")
        {
            player = other.gameObject;
            player.GetComponent<AlternarControles>().enabled = false;
            player.GetComponent<ControleTeclado>().enabled = false;
            player.GetComponent<ControleMouse>().enabled = false;
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.GetComponent<Animator>().SetBool("Walk",false);
            player.GetComponent<Animator>().SetBool("Run", false);
            GameObject.FindGameObjectWithTag("NextLevel").GetComponent<NextLevelWithoutButton>().enabled = false;
            GameObject.Find("HUDCanvas").GetComponent<Canvas>().enabled = false;
            videoPlayer.Play();
            audioSource.Play();
        }
    }
}
