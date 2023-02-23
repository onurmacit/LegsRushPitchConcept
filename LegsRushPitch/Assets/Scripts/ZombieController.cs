using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZombieController : MonoBehaviour
{
    public GameObject playerSpawnerGO;
    public PlayerSpawner playerSpawner;
    float zombieSpeed = 1.5f;
    public AudioClip congratsClip;
   

    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawner = playerSpawnerGO.GetComponent<PlayerSpawner>();              
    } 

    void Update()
    {

        Vector3 zombieNewPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + zombieSpeed * Time.deltaTime);
        transform.position = zombieNewPosition;   
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerSpawner.PlayAudio(congratsClip);
            zombieSpeed = 0;
            Destroy(gameObject);
            GameManager.instance.ShowSuccesPanel();
            playerSpawner.StopPlayer();
            playerSpawner.StopBackgroundMusic();
            
        }
    }
  
}
