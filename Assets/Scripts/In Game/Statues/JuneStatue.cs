using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;

public class JuneStatue : MonoBehaviour {
    MouseFake mouse;
    GameObject june;
    public GameObject twineTextPlayer;
    public GameObject twineTextPlayerPrefab;
    Story story;
    GameObject tLight;
    public bool mission;
    public AudioSource aSource;
   

    private void Awake()
    {
        tLight = GameObject.Find("JuneStatueLight");
        tLight.SetActive(false);
        aSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (twineTextPlayer != null)
        {
            if (story.CurrentPassage.Name == "Please, stop!")
            {
                Destroy(twineTextPlayer);
            }
        }
    }
    public void TriggerDialogue()
    {
        if (GameObject.FindGameObjectsWithTag("TwineTextPlayer").Length == 0)
            twineTextPlayer = Instantiate(twineTextPlayerPrefab);
        twineTextPlayer.GetComponent<TwineTextPlayer>().Story = GetComponent<JuneStatueDialogue>();
        story = twineTextPlayer.GetComponent<TwineTextPlayer>().Story;
        twineTextPlayer.GetComponent<TwineTextPlayer>().Story.Begin();
        twineTextPlayer.GetComponent<Canvas>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            aSource.Play();
            tLight.SetActive(true);
            mouse = FindObjectOfType<MouseFake>();
            mouse.GetComponent<Renderer>().enabled = false;
            mouse.enabled = false;
            Cursor.visible = true;
            TriggerDialogue();
        }
    }

}