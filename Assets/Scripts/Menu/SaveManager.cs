using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //SaveManager serve para passar informações importantes entre cenas 
    public bool gameLoaded;
    public string nextScene;
    public bool isJuneGiraffe;
    public bool missionBool;
    public string mission;
    public string questSprite;
    public Color questColor;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
