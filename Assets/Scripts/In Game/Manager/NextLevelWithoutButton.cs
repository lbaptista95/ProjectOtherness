using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelWithoutButton : MonoBehaviour {

    public string nextSceneName;
    public GameObject sMan;
    public GameObject player;    
    // Use this for initialization
    void Start()
    {
        sMan = GameObject.FindGameObjectWithTag("SaveManager");
        player = GameObject.Find("June(Clone)");
        GetComponent<Renderer>().enabled = false;        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sMan.GetComponent<SaveManager>().isJuneGiraffe = player.GetComponent<ControleTeclado>().isGiraffe;
            sMan.GetComponent<SaveManager>().mission = GameObject.Find("Quest").GetComponent<Text>().text;
            sMan.GetComponent<SaveManager>().missionBool = player.GetComponentInParent<AlternarControles>().mission;
            sMan.GetComponent<SaveManager>().questColor = GameObject.Find("QuestIMG").GetComponent<Image>().color;
            if (GameObject.Find("QuestIMG").GetComponent<Image>().sprite != null)
                sMan.GetComponent<SaveManager>().questSprite = GameObject.Find("QuestIMG").GetComponent<Image>().sprite.name;
            LoadNextScene();
        }
    }
    
    void LoadNextScene()
    {
        sMan.GetComponent<SaveManager>().nextScene = nextSceneName;
        SceneManager.LoadScene("NextLevel");
    }
}