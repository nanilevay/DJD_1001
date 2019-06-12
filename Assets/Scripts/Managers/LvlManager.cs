using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{
    [SerializeField] GameObject       playerPrefab;
    [SerializeField] Transform        spawnPoint;
    [SerializeField] CameraController cameraCtrl;

    private int        playerCurrentHP;
    private int        playerMaxHP;
    private GameObject playerChar;

    public static LvlManager instance;
   
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Respawn();
       // UIManager.instance.UpdateLivesDisplay();
    }
    void Respawn()
    {
        if(playerChar != null)
        {
            Destroy(playerChar);
        }

        playerChar = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        cameraCtrl.target = playerChar.transform;
    }

    public void LoseLife()
    {
        GmManager.instance.LoseLife();
      //  UIManager.instance.UpdateLivesDisplay();

        if(GmManager.instance.GetCurrentLives() < 0)
        {
            UIManager.instance.LoadScene("GameOver");
        }
        else
        {
            Respawn();
        }
    }

    public void SetPlayerMaxHP(int hp)
    {
        playerMaxHP = hp;
    }
    public void SetPlayerCurrentHP(int hp)
    {
        playerCurrentHP = hp;
    }

    public float GetCurrentLifePercentage()
    {
        return 1 - playerCurrentHP / playerMaxHP;
    }
}
