using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float timer;
    public float rate;
    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= rate)
        {
            Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.identity);

            timer = 0;
        }
    }
}
