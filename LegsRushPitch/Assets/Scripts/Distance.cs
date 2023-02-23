using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Distance : MonoBehaviour
{
    public Transform zombiee;
    public GameObject zombie;
    public GameObject player;
    public float distance;
    
    void Start()
    {
        
    }
   
    void Update()
    {
        distance = Vector3.Distance(zombie.transform.position, player.transform.position);
        if (distance > 3)
        {
            zombiee.DOScale(new Vector3(2, 2, 2), 3f);
        }
        if(distance < 5)
        {
            zombiee.DOScale(new Vector3(1,1,1), 3f);
        }
    }
}
