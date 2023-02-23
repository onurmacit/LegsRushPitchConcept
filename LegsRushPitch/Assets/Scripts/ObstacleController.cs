using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleController : MonoBehaviour
{
    
    private PlayerSpawner playerSpawner;
    private GameObject playerSpawnerGO;
   
    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawner = playerSpawnerGO.GetComponent<PlayerSpawner>();
    }
  
    void Update()
    {   
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {           
            playerSpawner.PlayerGotKilled(other.gameObject);
        }
    }
}
