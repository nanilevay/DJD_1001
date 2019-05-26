using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GmManager : MonoBehaviour
{
    [SerializeField] int        livesNum;

    int currentLives;

    public static GmManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public void LoseLife()
    {
        currentLives--;

    }

    void Start()
    {
        ResetGame();
    }

    /*
    public void LoseLife()
    {
        currentLives--;
    }
    */

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public void ResetGame()
    {
        currentLives = livesNum;
    }


}
