using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Canvas mainMenu;

    public Canvas optionsMenu;
    public Canvas soundsMenu;
    public Canvas videoMenu;

    public Dropdown resolution;
    public Dropdown quality;
    public int rWidth;
    public int rHeight;
    public int qualityInt;

    public Button load;
    public GameObject saveButtonPrefab;
    public Text saveText;
    public GameObject manager;

    public Slider volumeSlider;
    private void Awake()
    {
        optionsMenu.enabled = false;
        soundsMenu.enabled = false;
        videoMenu.enabled = false;
        mainMenu.enabled = true;
    }
    private void Update()
    {
        Cursor.visible = true;
        if (PlayerPrefs.HasKey("Personagem"))
        {
            load.enabled = true;
        }
        else
            load.enabled = false;
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
    }

    public void NewGame()
    {
        manager = GameObject.FindGameObjectWithTag("SaveManager");
        manager.GetComponent<SaveManager>().gameLoaded = false;
        manager.GetComponent<SaveManager>().nextScene = "Level Design - Casa Int";
        SceneManager.LoadScene("Cutscene - Intro");
    }
    public void Options()
    {
        mainMenu.enabled = false;
        optionsMenu.enabled = true;
    }

    public void SoundMenu()
    {
        optionsMenu.enabled = false;
        soundsMenu.enabled = true;
    }

    public void VideoMenu()
    {
        optionsMenu.enabled = false;
        videoMenu.enabled = true;
    }



    public void ReturnToOptions()
    {
        soundsMenu.enabled = false;
        videoMenu.enabled = false;
        optionsMenu.enabled = true;
    }

    public void LoadGame()
    {
        manager = GameObject.FindGameObjectWithTag("SaveManager");
        manager.GetComponent<SaveManager>().gameLoaded = true;
        manager.GetComponent<SaveManager>().nextScene = PlayerPrefs.GetString("Cena");
        SceneManager.LoadScene("Loading");
    }


    public void ReturnToMenu()
    {
        //SceneManager.LoadScene("Menu");
        soundsMenu.enabled = false;
        videoMenu.enabled = false;
        optionsMenu.enabled = false;
        mainMenu.enabled = true;
    }

    public void Resolution()
    {
        string rOption = resolution.GetComponentInChildren<Text>().text;
        rOption.Replace(" ", "");
        string[] rWidthHeight = rOption.Split('x');
        rWidth = int.Parse(rWidthHeight[0]);
        rHeight = int.Parse(rWidthHeight[1]);

    }

    public void SetVideoConfig()
    {
        Resolution();
        Screen.SetResolution(rWidth, rHeight, Screen.fullScreen);
        if (quality.GetComponentInChildren<Text>().text == "High")
        {
            qualityInt = 2;
        }
        else if (quality.GetComponentInChildren<Text>().text == "Medium")
        {
            qualityInt = 1;
        }
        else if (quality.GetComponentInChildren<Text>().text == "Low")
        {
            qualityInt = 0;
        }
        QualitySettings.SetQualityLevel(qualityInt);
        PlayerPrefs.SetInt("ResolutionW", rWidth);
        PlayerPrefs.SetInt("ResolutionH", rHeight);
        PlayerPrefs.SetInt("Quality", qualityInt);
        print("WIDTH: " + rWidth + "   HEIGHT: " + rHeight + "   QUALITY: " + qualityInt);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        
    }
}
