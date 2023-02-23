using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject MainPanel;
    public GameObject FailPanel;
    public GameObject SuccesPanel;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartButtonTapped()
    {
        
        MainPanel.gameObject.SetActive(false);
        GameObject playerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        PlayerSpawner playerSpawner = playerGO.GetComponent<PlayerSpawner>();
        playerSpawner.GameStarted();
     
    }

    public void ShowFailPanel()
    {
        FailPanel.gameObject.SetActive(true);
    }

    public void RestartButtonTapped()
    {
        SceneManager.LoadScene("LevelScene1");
    }

    public void ShowSuccesPanel()
    {
        SuccesPanel.gameObject.SetActive(true);
       
    }

    public void NextLevelButtonTapped()
    {
        SceneManager.LoadScene("LevelScene1");
    }

}
