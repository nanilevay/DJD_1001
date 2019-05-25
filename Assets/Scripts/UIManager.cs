using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameObject[] pause;

    // checking if player presses "p" key
    private bool pausePressed;

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;

        pause = GameObject.FindGameObjectsWithTag("PauseMenu");

    
        HidePaused();
    }

    private void FixedUpdate()
    {

        pausePressed = Input.GetButtonDown("Pause");

        if (pausePressed)
        {   
            pauseMenuToggler();
        }
    }

    // checking if the game is currently paused or not in order to toggle on / off
    public void pauseMenuToggler()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPaused();
        }

        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePaused();
        }
    }

    // if continue is pressed in pause menu
    public void Continue()
    {
        pauseMenuToggler();
    }

    public void ShowPaused()
    {
        // need to optimise this
        foreach (GameObject g in pause)
        {
            g.SetActive(true);
        }
        
    }

    public void HidePaused()
    {
        foreach (GameObject g in pause)
        {
            g.SetActive(false);
        }
        
    }

    // to load any of the scenes needed
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);  
    }

    // quits game if player clicks "Quit" option in menus
    public void QuitGame()
    { 
        Application.Quit();
    }
}
