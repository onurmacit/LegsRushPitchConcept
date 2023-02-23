using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSpawner : MonoBehaviour
{
    public Transform character;
    public GameObject playerGO;
    public List<GameObject> playersList = new List<GameObject>();
    float playerSpeed = 5;
    float speedUp = 1;
    float maxSpeed = 15;
    float speedDown = 1f;
    float xSpeed;
    float maxXPosition = 4.1f;
    bool isPlayerMooving;
    float minSpeed = 5;


    public AudioSource playerSpawnerAudioSource;
    public AudioClip gateClip, congratsClip, failClip;
 
    void Start()
    {   
        //isPlayerMooving = true;         
    }

    
    void Update()
    {
        if (isPlayerMooving == false)
        {
            return;
        }
        float touchX = 0;
        float newXValue = 0f;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            xSpeed = 250f;
            touchX = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }

        else if (Input.GetMouseButton(0))
        {
            xSpeed = 125f;
            touchX = Input.GetAxis("Mouse X");
        }

        newXValue = transform.position.x + xSpeed * touchX * Time.deltaTime;
        newXValue = Mathf.Clamp(newXValue, - maxXPosition, maxXPosition);
        Vector3 playerNewPosition = new Vector3(newXValue, transform.position.y, transform.position.z + playerSpeed * Time.deltaTime);
        transform.position = playerNewPosition;

    }

    public void SpawnPlayer(int gateValue, GateType gateType)
    {
        PlayAudio(gateClip);
        if(gateType == GateType.thinnerType)
        {
            for (int i = 0; i < gateValue; i++)
            {
                GameObject newPlayerGO = Instantiate(playerGO, GetPlayerPositon(), Quaternion.identity, transform);
                playersList.Add(newPlayerGO);
            }
           
            playerSpeed = playerSpeed +speedUp;
            if (playerSpeed > 15)
            {
                playerSpeed = maxSpeed;
            }
        }

        else if (gateType == GateType.fatterType)
        {
            int newPlayerCount = (playersList.Count * gateValue) - playersList.Count;
            for (int i = 0; i < newPlayerCount; i++)
            {
                GameObject newPlayerGO = Instantiate(playerGO, GetPlayerPositon(), Quaternion.identity, transform);
                playersList.Add(newPlayerGO);
            }
            character.DOScale(new Vector3(1,1,1), 1f);
            playerSpeed = playerSpeed + speedUp * 2;
            if (playerSpeed > 15)
            {
                playerSpeed = maxSpeed;
            }
        }     
    }



    public Vector3 GetPlayerPositon()
    {
        Vector3 position = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + position;
        return newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish Line")
        {
            StopBackgroundMusic();
            PlayAudio(failClip);    
            isPlayerMooving = false;
            GameManager.instance.ShowFailPanel();
        }
    }

    private void CheckPlayersCount()
    {
        if (playersList.Count <= 0)
        {
            StopBackgroundMusic();
            PlayAudio(failClip);
            StopPlayer();
            GameManager.instance.ShowFailPanel();
        }
    }

    public void StopPlayer()
    {
        isPlayerMooving = false;
    }

    public void GameStarted()
    {
        isPlayerMooving = true;
    }


    public void PlayerGotKilled(GameObject playerGo)
    {        
        playersList.Remove(playerGo);
        Destroy(playerGo);
        CheckPlayersCount();
        playerSpeed = playerSpeed - speedDown;
        if (playerSpeed < 5)
        {
            playerSpeed = minSpeed;
        }
    }       

    public void PlayAudio(AudioClip clip)
    {
        if(playerSpawnerAudioSource != null)
        {
            playerSpawnerAudioSource.PlayOneShot(clip, 0.4f);
        }

    }

    public void StopBackgroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }
}
