using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class GoToGame : MonoBehaviour
{
    //SCRIPT PRESENTE EM TODA CUTSCENE, SERVE PARA CARREGAR A PRÓXIMA CENA
    // Use this for initialization
    public string sceneName;
    VideoPlayer vPlayer;
    public float timeToGo;
    void Start()
    {
        vPlayer = GetComponent<VideoPlayer>();

    }

    // Update is called once per frame
    void Update()
    {
        print(vPlayer.time);
        if (sceneName != "")
            GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>().nextScene = sceneName;
        if (vPlayer.time >= timeToGo || Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Loading");
    }
}
