using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float rate = 6;
    public float rateIncreasing = 0.5f;
    public int callsBeforeRateIncrease = 8;
    float rateTimer;
    int callCounter = 0;
    public GameManager GM;
    void Start()
    {
        rateTimer = rate;
    }

    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            rateTimer -= Time.deltaTime;
            if (rateTimer <= 0)
            {
                SpawnEnemies();
            }
        }
    }

    void SpawnEnemies()
    {
        callCounter++;                                                              //ad ogni spawn aumenta il conteggio e quando raggiunge quello di rateIncrease, aumenta il rate

        GameObject Enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.identity);

        if (callCounter >= callsBeforeRateIncrease && rate >= 4 * rateIncreasing)   //il rate si velocizza solo fin quando sarà maggiore di zero, dopo non ha più senso (auguri a chiudere i popup alla velocità della luce)
        {
            rate -= rateIncreasing;
            callCounter = 0;
        }
        rateTimer = rate;
    }
}
