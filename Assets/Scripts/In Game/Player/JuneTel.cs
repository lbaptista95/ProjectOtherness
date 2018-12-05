using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cradle;

public class JuneTel : MonoBehaviour
{

    // Use this for initialization
    float telTimer;
    AudioSource audioSource;
    AudioClip telClip;
    AlternarControles playerControl;
    GameObject interactMessage;
    [SerializeField]
    string typeOfInteraction;
    bool playAgain;
    bool readMessage;
    GameObject nextLevel;
    public GameObject twineTextPlayer;
    public GameObject twineTextPlayerPrefab;
    Story story;
    MouseFake mouse;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerControl = GetComponent<AlternarControles>();
        telClip = GetComponent<Audio>().clips[6];
        playAgain = true;
        interactMessage = GameObject.FindGameObjectWithTag("GameMessage");
        nextLevel = GameObject.FindGameObjectWithTag("NextLevel");
        if (SceneManager.GetActiveScene().name == "Level Design - Casa Int")
            nextLevel.SetActive(false);
        readMessage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level Design - Casa Int")
        {
            telTimer += Time.deltaTime;
            if (telTimer >= 4 && !readMessage)
            {
                audioSource.clip = telClip;
                if (playAgain)
                {
                    audioSource.Play();
                    playAgain = false;
                }
                if (!audioSource.isPlaying)
                    playAgain = true;
                playerControl.InteractionAvailable(typeOfInteraction);

            }

            if (playerControl.interact && playerControl.interactionType == this.typeOfInteraction)
            {
                readMessage = true;
                TriggerMessage();
            }
            if (twineTextPlayer != null)
            {
                twineTextPlayer.GetComponent<Canvas>().enabled = true;
                if (story.CurrentPassage.Name == "Ok.")
                {
                    nextLevel.SetActive(true);
                    Cursor.visible = false;
                    Destroy(twineTextPlayer);
                }
            }
        }
    }
    public void TriggerMessage()
    {
        if (GameObject.FindGameObjectsWithTag("TwineTextPlayer").Length == 0)
            twineTextPlayer = Instantiate(twineTextPlayerPrefab);
        twineTextPlayer.GetComponent<TwineTextPlayer>().Story = GetComponent<PhoneMessage>();
        story = twineTextPlayer.GetComponent<TwineTextPlayer>().Story;

        twineTextPlayer.GetComponent<TwineTextPlayer>().Story.Begin();
        twineTextPlayer.GetComponent<Canvas>().enabled = true;
        Cursor.visible = true;
    }
}
