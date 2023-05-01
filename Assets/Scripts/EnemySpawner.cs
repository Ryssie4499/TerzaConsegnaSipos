using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float timer;
    public float changeTimer;
    public float rate;
    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
        changeTimer += Time.deltaTime;
        if(changeTimer>=50)
        {
            rate = 2.5f;
        }
        if (timer >= rate)
        {
            Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.identity);

            timer = 0;
        }
    }
}
