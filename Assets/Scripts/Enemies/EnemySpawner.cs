using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float rate = 5;
    public float rateIncreasing = 0.5f;
    public int callsBeforeRateIncrease = 5;
    float rateTimer;
    int callCounter = 0;
    public int enemyCounter;
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

        GameObject Enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], gameObject.transform.position, Quaternion.identity);
        enemyCounter++;
        if (callCounter >= callsBeforeRateIncrease && rate >= 2 * rateIncreasing)   //il rate si velocizza solo fin quando sarà maggiore di zero, dopo non ha più senso
        {
            rate -= rateIncreasing;
            callCounter = 0;
        }
        rateTimer = rate;
    }
    int multiplier = 1;
    int counter;
    private void OnTriggerExit(Collider other)
    {
        if (enemyCounter >= multiplier * 4)
        {
            counter = 4 * (multiplier + 1);
            if (enemyCounter < counter)
            {
                other.GetComponent<EnemyManager>().health += multiplier + 4;
                //Debug.Log(other.GetComponent<EnemyManager>().health);
            }
            else
                multiplier++;
        }
    }
}
