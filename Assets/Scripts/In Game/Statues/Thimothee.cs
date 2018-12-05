using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;

public class Thimothee : MonoBehaviour
{

    MouseFake mouse;
    GameObject june;
    public GameObject twineTextPlayer;
    public GameObject twineTextPlayerPrefab;
    Story story;
    public GameObject tLight;
    public bool mission;
    public AudioSource aSource;

    private void Start()
    {
        tLight = GameObject.Find("ThimotheeLight");
        tLight.SetActive(false);
        aSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (twineTextPlayer != null)
        {
            if (twineTextPlayer.GetComponent<TwineTextPlayer>().Story.CurrentPassage.Name == "<NEXT>")
            {
                Destroy(twineTextPlayer);
            }
        }
    }
    public void TriggerDialogue()
    {
        if (GameObject.FindGameObjectsWithTag("TwineTextPlayer").Length == 0)
            twineTextPlayer = Instantiate(twineTextPlayerPrefab);
        twineTextPlayer.GetComponent<TwineTextPlayer>().Story = GetComponent<ThimotheeDialogue>();
        story = twineTextPlayer.GetComponent<TwineTextPlayer>().Story;
        twineTextPlayer.GetComponent<Canvas>().enabled = true;
        twineTextPlayer.GetComponent<TwineTextPlayer>().Story.Begin();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            aSource.PlayOneShot(aSource.clip);            
            tLight.SetActive(true);
            mouse = FindObjectOfType<MouseFake>();
            mouse.GetComponent<Renderer>().enabled = false;
            mouse.enabled = false;
            Cursor.visible = true;
            TriggerDialogue();
        }
    }

}