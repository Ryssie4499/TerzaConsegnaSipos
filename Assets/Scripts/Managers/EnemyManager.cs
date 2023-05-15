using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static event Action OnFinishLineReached;     //creo un evento relativo al raggiungimento del villaggio da parte degli enemy
    public static event Action OnEnemyDeath;            //e un evento relativo alla loro morte

    [SerializeField] public GameObject[] patrolPoints;  //i punti lungo i quali gli enemies si spostano

    public float movementSpeed;                         //velocità di movimento
    public float rotationSpeed;                         //velocità di rotazione

    public float health;                                //vita degli enemy
    public float timer;                                 //timer della tartaruga (rallentamento del movimento degli enemies, ma non viene rallentato lo spawn)

    //Refs
    InputManager IM;
    public GameManager GM;

    protected virtual void Start()
    {
        IM = FindObjectOfType<InputManager>();
    }

    protected virtual void FixedUpdate()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)        //in running
            MoveAround();                                               //gli enemies si muovono lungo il loro tracciato
    }

    public int index = 0;                                               //l'indice del patrol point raggiunto/da raggiungere
    void MoveAround()
    {
        if (IM.turtle)                                                                                                          //se la tartaruga è attiva
        {
            timer += Time.deltaTime;                                                                                            //parte un timer
            if (timer < 7)                                                                                                      //finchè non sono passati 7 secondi
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[index].transform.position, 0.2f);     //il movimento degli enemy lungo i patrol points è a velocità ridotta
            else                                                                                                                //finiti i 7 secondi la tartaruga si disattiva e il timer si resetta (non si riattiverà più)
            {
                IM.used = true;
                IM.turtle = false;
                IM.Turtle.SetActive(false);
                timer = 0;
            }
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[index].transform.position, movementSpeed); //se la tartaruga non è attiva, il movimento procede da un patrol point all'altro con una velocità predefinita

        Vector3 targetDirection = patrolPoints[index].transform.position - transform.position;                                  //la direzione dell'enemy sarà quella che gli garantisce di raggiungere il punto successivo
        float singleStep = rotationSpeed * Time.deltaTime;                                                                      //questo è l'angolo di rotazione dell'enemy con una velocità predefinita
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);                     //la nuova direzione sposta il forward dell'enemy verso il punto successivo con la velocità di base
        transform.rotation = Quaternion.LookRotation(newDirection);                                                             //la rotazione dell'enemy avviene in modo che il suo forward sia nella nuova direzione

        if (Vector3.Distance(transform.position, patrolPoints[index].transform.position) <= 0)                                  //se la distanza tra l'enemy e il punto è minore di zero
        {
            if (patrolPoints.Length - 1 != index)                                                                               //e la quantità di punti è diversa dall'indice attuale
                index++;                                                                                                        //l'indice aumenta, perciò l'enemy passerà al punto successivo
        }
    }

    //creo un metodo che gestisca il danno che le torrette fanno all'enemy e se la sua vita scendesse sotto zero, distruggerebbe l'enemy in questione e richiamerebbe l'evento relativo alla sua morte (lo score)
    public void Damage(float Damage)
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            health -= Damage;
            if (health <= 0)
            {
                Destroy(gameObject);
                OnEnemyDeath?.Invoke();
            }
        }
    }

    //all'ingresso dell'enemy nel villaggio del player (se il gioco è in running), l'enemy viene distrutto e viene richiamato l'evento relativo alla perdita di vita del player
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Village") && GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            Destroy(gameObject);
            OnFinishLineReached?.Invoke();
        }
    }
}
