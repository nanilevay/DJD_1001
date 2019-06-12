using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image lifeBarImage;

    GameObject[] pause;

    public static UIManager instance;

    // checking if player presses "p" key
    private bool pausePressed;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //UpdateLivesDisplay();

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

    private void Update()
    {
        UpdateLiveDisplay();
    }

    private void UpdateLiveDisplay()
    {
        lifeBarImage.fillAmount = LvlManager.instance.GetCurrentLifePercentage();
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
        // need to optimize this
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
