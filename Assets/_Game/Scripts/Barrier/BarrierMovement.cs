using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject player;
    private bool isPoint;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        
        if(transform.position.x <= -7)
        {
            Destroy(transform.gameObject);
        }

        if(transform.position.x < player.transform.position.x && !isPoint){
            isPoint = true;
            PlayerController.Point++;
        }
    }
}
