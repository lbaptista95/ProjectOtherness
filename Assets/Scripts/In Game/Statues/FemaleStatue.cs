using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;

public class FemaleStatue : MonoBehaviour
{
    MouseFake mouse;
    GameObject june;
    public GameObject twineTextPlayer;
    public GameObject twineTextPlayerPrefab;
    Story story;
    GameObject tLight;
    public bool mission;
    Camera cam;
    public AudioSource aSource;
    public GameObject fog;

    private void Start()
    {
        tLight = GameObject.Find("FemaleLight");
        tLight.SetActive(false);
        cam = Camera.main;
        aSource = GetComponent<AudioSource>();        
    }

    private void Update()
    {

        if (twineTextPlayer != null)
        {
            if (story.CurrentPassage.Name == "<NEXT>")
            {
                Destroy(twineTextPlayer);
            }
        }
    }
    public void TriggerDialogue()
    {
        if (GameObject.FindGameObjectsWithTag("TwineTextPlayer").Length == 0)
            twineTextPlayer = Instantiate(twineTextPlayerPrefab);
        twineTextPlayer.GetComponent<TwineTextPlayer>().Story = GetComponent<FemaleStatueDialogue>();
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