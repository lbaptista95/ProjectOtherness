using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public MouseFake mouseFake;
    public GameObject cursorFake;
    public GameObject pauseMenu;
    // Use this for initialization
    void Awake()
    {
        cursorFake = GameObject.FindGameObjectWithTag("Cursor");
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }
    void ResumeGame()
    {
        if (GameObject.FindGameObjectsWithTag("TwineTextPlayer") == null)
            Cursor.visible = false;
        cursorFake.GetComponent<Renderer>().enabled = true;
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenu.SetActive(false);

    }

    void PauseGame()
    {
        cursorFake.GetComponent<Renderer>().enabled = false;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeButton()
    {
        if (gameIsPaused)
        {
            Cursor.visible = false;
            cursorFake.GetComponent<Renderer>().enabled = true;
            Time.timeScale = 1f;
            gameIsPaused = false;
            pauseMenu.SetActive(false);
        }
    }

    public void ReturnToMenu()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuSemManager");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
