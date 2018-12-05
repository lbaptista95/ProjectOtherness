﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    GameObject mouse;
    GameObject june;
    ControleMouse mouseControl;
    ControleTeclado keyControl;
    public GameObject twineTextPlayer;
    public GameObject twineTextPlayerPrefab;
    Story story;
    AlternarControles playerControl;
    public string typeOfInteraction;
    public bool askingToChat;
    public bool mission;
    public GameObject interlocutor;
    private void Awake()
    {
        mouse = GameObject.FindGameObjectWithTag("Cursor");
        playerControl = GetComponentInParent<AlternarControles>();
        keyControl = GetComponentInParent<ControleTeclado>();
        mouseControl = GetComponentInParent<ControleMouse>();
        if (SceneManager.GetActiveScene().name == "Level Design - QG Girafas")
        {
            GameObject.FindGameObjectWithTag("NextLevel").GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Update()
    {
        if (interlocutor != null)
            if (Vector3.Angle(transform.root.forward, (interlocutor.transform.position - transform.root.position)) <= 120)
            {
                if (interlocutor.GetComponent<Dialogue>().interaction != "")
                {
                    typeOfInteraction = interlocutor.GetComponent<Dialogue>().interaction;
                    playerControl.InteractionAvailable(typeOfInteraction);
                }
                askingToChat = true;
            }
            else
            {
                playerControl.ClearInteractions();                
                if (twineTextPlayer == null)
                    askingToChat = false;
            }
        if (askingToChat)
        {
            if (interlocutor.GetComponent<Dialogue>().interaction != "")
            {
                if (playerControl.interact)
                {
                    mouse.GetComponent<Renderer>().enabled = false;
                    Cursor.visible = true;
                    TriggerDialogue();                    
                }
            }
            if (twineTextPlayer != null)
            {
                twineTextPlayer.GetComponent<Canvas>().enabled = true;                
                if (mouseControl.enabled)
                    mouseControl.enabled = false;
                GetComponentInParent<NavMeshAgent>().isStopped = true;
                GetComponentInParent<Animator>().SetBool("Walk", false);
                GetComponentInParent<Animator>().SetBool("Run", false);
                keyControl.enabled = false;
                if (twineTextPlayer.GetComponent<TwineTextPlayer>().GetComponent<Canvas>().enabled)
                    playerControl.ClearInteractions();
                if (interlocutor.GetComponent<Dialogue>().endOfStories.Length == 1)
                {
                    if (twineTextPlayer.GetComponent<TwineTextPlayer>().Story.CurrentPassage.Name == interlocutor.GetComponent<Dialogue>().endOfStories[0] ||
                        (twineTextPlayer.GetComponent<TwineTextPlayer>().Story.CurrentPassage.Name == "Passagem Sem Nome" && twineTextPlayer.GetComponent<TwineTextPlayer>().Story.CurrentPassage.Index > 0))
                    {
                        print("ACABA DIALOGO");
                        if (interlocutor.GetComponent<Dialogue>().missions.Length > 0)
                        {
                            GameObject.Find("Quest").GetComponent<Text>().text = interlocutor.GetComponent<Dialogue>().missions[0];
                            GetComponentInParent<AlternarControles>().mission = true;
                        }
                        if (interlocutor.name == "KeyDialogue")
                        {
                            GameObject.Find("Quest").GetComponent<Text>().text = interlocutor.GetComponent<Dialogue>().missions[0];
                            GameObject.Find("QuestIMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Letter");
                            GetComponentInParent<AlternarControles>().mission = true;
                            var tempColor = GameObject.Find("QuestIMG").GetComponent<Image>().color;
                            tempColor.a = 1f;
                            tempColor.r = 1f;
                            tempColor.g = 1f;
                            tempColor.b = 1f;
                            GameObject.Find("QuestIMG").GetComponent<Image>().color = tempColor;
                            GameObject.Find("Barreira1").GetComponent<MeshRenderer>().enabled = false;
                            GameObject.Find("Barreira1").GetComponent<BoxCollider>().enabled = false;
                            GameObject.Find("Barreira1").GetComponent<NavMeshObstacle>().enabled = false;
                        }
                        {
                            for (int x = 0; x < GameObject.FindGameObjectsWithTag("InfoBoard").Length; x++)
                            {
                                GameObject.FindGameObjectsWithTag("InfoBoard")[x].GetComponent<SphereCollider>().enabled = true;
                            }
                        }
                        if (interlocutor.name == "Quadro4")
                        {
                            GetComponentInParent<AlternarControles>().mission = false;
                            GameObject.Find("Quest").GetComponent<Text>().text = "You have the Staff now. Press Q to draw it and Space to use it";
                            GetComponentInParent<ControleTeclado>().isGiraffe = true;
                            GameObject.FindGameObjectWithTag("NextLevel").GetComponent<BoxCollider>().enabled = true;
                        }
                        if (interlocutor.name == "Boss")
                        {
                            SceneManager.LoadScene("Menu");
                        }
                        if (interlocutor.name == "SoldadoMorto")
                        {
                            interlocutor.GetComponent<Animator>().SetTrigger("Die");
                            Destroy(GameObject.Find("ParedeInvisivelPonte"));
                        }
                        Destroy(twineTextPlayer);
                        Cursor.visible = false;
                        playerControl.chating = false;
                        if (mouseControl.enabled)
                            mouseControl.enabled = false;
                        GetComponentInParent<NavMeshAgent>().isStopped = false;
                        keyControl.enabled = true;
                    }
                }
                if (twineTextPlayer.GetComponent<TwineTextPlayer>().Story.CurrentPassage.Name == "(Hand him the letter)")
                {
                    GameObject.Find("Quest").GetComponent<Text>().text = "";
                    GetComponentInParent<AlternarControles>().mission = false;
                    var tempColor = GameObject.Find("QuestIMG").GetComponent<Image>().color;
                    tempColor.a = 0f;
                    GameObject.Find("QuestIMG").GetComponent<Image>().color = tempColor;
                }
            }
        }
    }
    public void TriggerDialogue()
    {
        playerControl.chating = true;
        if (mouseControl.enabled)
            mouseControl.enabled = false;
        GetComponentInParent<NavMeshAgent>().isStopped = true;
        GetComponentInParent<Animator>().SetBool("Walk", false);
        GetComponentInParent<Animator>().SetBool("Run", false);
        keyControl.enabled = false;
        playerControl.ClearInteractions();
        if (GameObject.FindGameObjectsWithTag("TwineTextPlayer").Length == 0)
            twineTextPlayer = Instantiate(twineTextPlayerPrefab);
        if (interlocutor.GetComponent<Dialogue>().dialogueNames.Length == 1)
        {
            string type = interlocutor.GetComponent<Dialogue>().dialogueNames[0];
            twineTextPlayer.GetComponent<TwineTextPlayer>().Story = interlocutor.GetComponent(type) as Cradle.Story;
        }
        twineTextPlayer.GetComponent<Canvas>().enabled = true;
        twineTextPlayer.GetComponent<TwineTextPlayer>().StartStory = true;
        twineTextPlayer.GetComponent<TwineTextPlayer>().Story.Begin();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Dialogue>() != null && other.gameObject.GetComponent<Dialogue>().enabled)
        {
            print(other.gameObject.name);
            interlocutor = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Dialogue>() != null && other.gameObject.GetComponent<Dialogue>().enabled)
        {
            interlocutor = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Dialogue>() != null && other.gameObject.GetComponent<Dialogue>().enabled)
        {
            
            interlocutor = null;
            playerControl.ClearInteractions();
            typeOfInteraction = "";
            askingToChat = false;
        }
    }

}