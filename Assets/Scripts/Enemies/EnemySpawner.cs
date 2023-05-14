using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;                        //creo un array che conterrà i tipi di nemici da spawnare

    public float rate = 6f;                             //i secondi che passano tra lo spawn di un enemy e l'altro
    public float rateIncreasing = 1f;                   //di quanto aumenta il rate

    public int callsBeforeRateIncrease = 5;             //ogni quanti enemy spawnati aumenta il rate

    int enemyCounter;                                   //il numero di nemici spawnati
    float rateTimer;                                    //il timer relativo al rate di spawn
    int callCounter = 0;                                //il numero di chiamate

    //Refs
    public GameManager GM;

    void Start()
    {
        rateTimer = rate;                               //dallo start, passano 6 secondi prima che spawni il primo enemy (esattamente il valore del rate)
    }

    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)                    //controllo che ogni azione avvenga in gameRunning, per evitare che in pausa continuino a spawnare nemici
        {
            rateTimer -= Time.deltaTime;                                            //il timer relativo al rate diminuisce di uno ogni secondo, fino a raggiungere lo zero
            if (rateTimer <= 0)                                                     //dopo lo zero, vengono spawnati gli enemy
            {
                SpawnEnemies();
            }
        }
    }

    void SpawnEnemies()
    {
        callCounter++;                                                              //ad ogni spawn aumenta il conteggio delle call e quando raggiunge lo stesso valore di rateIncrease, aumenta il rate

        GameObject Enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], gameObject.transform.position, Quaternion.identity); //istanzio un tipo di Enemy randomico tra quelli presenti nell'array nella posizione dell'empty (spawner)

        enemyCounter++;                                                             //ad ogni spawn aumenta il conteggio di nemici spawnati

        if (callCounter >= callsBeforeRateIncrease && rate >= 2 * rateIncreasing)   //il rate si velocizza ogni volta che raggiunge il valore di chiamate prima dell'aumento, solo fin quando è maggiore di 2 (dopo i nemici spawnano talmente vicini tra loro, da rendere impossibile ucciderli tutti)
        {
            rate -= rateIncreasing;                                                 //i secondi tra uno spawn e l'altro diminuiscono di volta in volta dello stesso valore
            callCounter = 0;                                                        //una volta aumentato il rate, il conteggio delle chiamate si azzera, così può ricominciare a spawnare i 5 nemici prima di aumentare di nuovo il rate
        }
        rateTimer = rate;                                                           //dopo lo spawn, il timer torna ad avere il valore del rate (già diminuito di 1 se ha fatto il rateIncrease) per poter rifare il conto alla rovescia
    }

    int multiplier = 1;                                                             //il moltiplicatore gestisce sia di quanto aumentare il conteggio degli enemy, sia di quanto aumentarne la vita
    int counter;                                                                    //questo mi servirà per gestire il limite tra un'ondata di enemy e l'altra
    private void OnTriggerExit(Collider other)
    {
        if (enemyCounter >= multiplier * 4)                                         //una volta uscito dal trigger dello spawn, se gli enemy spawnati sono più di 4 all'inizio (poi di 8, 12, 16...)
        {
            counter = 4 * (multiplier + 1);                                         //assegno al counter il valore del conteggio degli enemy successivo (all'inizio 8, poi 12, 16, 20...)
            if (enemyCounter < counter)                                             //e se il numero di enemy spawnati è minore del prossimo conteggio (perciò all'inizio tra 4 e 8)
            {
                other.GetComponent<EnemyManager>().health += multiplier + 4;        //la vita dell'enemy uscito dal trigger, aumenterà in base al valore del moltiplicatore (all'inizio di 5, poi di 6, 7, 8...)
            }
            else
                multiplier++;                                                       //nel caso in cui il conteggio degli enemy spawnati sia maggiore del conteggio successivo, il moltiplicatore aumenta di uno
        }
    }

    //in questo modo ogni tot nemici spawnati (e man mano che si avanza, quel numero aumenta) la loro vita aumenta, per rendere il game flow più complesso e meno monotono
}
