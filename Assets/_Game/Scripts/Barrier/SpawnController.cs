using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject barrierPrefab;
    [SerializeField] private float rateSpawn;
    [SerializeField] private float[] posSpawns = new float[2];
    private float currentTime;
    private int position;
    private float yAxys;
    void Start()
    {
        currentTime = 0f;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= rateSpawn)
        {
            currentTime = 0f;

            position = Random.Range(1, 100);
            yAxys = position > 50 ? posSpawns[0] : posSpawns[1];
            
            GameObject barrierObj = Instantiate(barrierPrefab, new Vector3(transform.position.x, yAxys, barrierPrefab.transform.position.z), Quaternion.identity);
        }
    }
}
