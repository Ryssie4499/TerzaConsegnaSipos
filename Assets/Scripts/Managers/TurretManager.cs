using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    private EnemyManager target;                                            //ogni oggetto contenente lo script EnemyManager sar� considerato un target

    [Header("Setup Turret:")]
    public float range;                                                     //la torretta avr� un range entro il quale un target pu� essere preso di mira
    public float rotationSpeed;                                             //questa � la velocit� di rotazione della torretta
    public List<EnemyManager> targetsInRange = new List<EnemyManager>();    //creo una lista che conterr� tutti i target all'interno del range
    public GameObject fire;                                                 //il fuoco che si accende solo sopra all'ultima torretta della pila e solo se ha raggiunto la massima altezza

    [Header("Setup Bullet:")]
    public GameObject bullet;                                               //qui metter� il prefab del proiettile che la torretta sparer�
    public float rateOfFire;                                                //questo � il rateo di fuoco (modificabile da inspector, dalle classi figlie o dai buff)
    public float bulletDamage;                                              //questo sar� il danno del proiettile (modificabile da inspector, dalle classi figlie o dai buff)
    public float bulletSpeed;                                               //questa invece � la velocit� del proiettile (modificabile da inspector o dalle classi figlie)

    public CapsuleCollider cc;                                              //c'� bisogno di una reference al collider della torretta (cos� potr� essere della stessa dimensione del range di default)
    public GameManager GM;
    private void Start()
    {
        if (transform.position.y > 27)                                      //se la posizione in altezza della torretta � maggiore di 27 attivo il fuoco (visto che non posso piazzare altre torrette sopra)
        {
            fire.SetActive(true);
        }
    }
    private void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)            //in game running
        {
            cc.radius = range;                                              //il collider aumenta all'aumentare del raggio d'azione della torretta
            cc.height = range;                                              //anche l'altezza del collider � la stessa del range
            CheckTargetsInRange();                                          //controllo se i target nel range sono ancora vivi e se non lo sono, li rimuovo dalla lista

            if (!triggerTimer)                                              //una volta sparato, parte un timer 
            {
                timer();
            }
            else                                                            //se non ha ancora sparato cerca il pi� vicino dei nemici
            {
                GetClosest(ref target);
            }

            if (target != null)                                             //se ho un target sparo e faccio ripartire il timer e tolgo l'enemy dai target in modo da poter rifare il controllo della prossimit� e definire un nuovo target
            {
                triggerTimer = false;
                Shoot();
                target = null;
            }
            foreach (EnemyManager enemy in targetsInRange)                  //per ogni nemico nel range
            {
                if (enemy != null)                                          //se il nemico esiste ancora
                {
                    Vector3 targetDirection = enemy.transform.position - transform.position;                                //questo � il vettore direzione che prender� la torretta all'arrivo di un nemico
                    float singleStep = rotationSpeed * Time.deltaTime;                                                      //questo invece � l'angolo di rotazione della torretta nel tempo con una determinata velocit�
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);     //la direzione nuova della torretta sar� tale da spostare il proprio forward nella direzione del target ruotando con una determinata velocit�
                    transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));           //la rotazione della torretta avr� come forward la nuova direzione sull'asse x e sull'asse z, mentre invece prender� 0 come valore sull'asse y
                }
            }
        }
    }

    //creo un metodo che controlli se il target � ancora vivo, nel caso contrario, lo rimuover�
    void CheckTargetsInRange()
    {
        for (int index = 0; index < targetsInRange.Count; index++)      //controllo l'intera lista di target all'interno del range
        {
            if (targetsInRange[index].health <= 0)                      //se la vita di uno di loro fosse minore di zero
            {
                targetsInRange.Remove(targetsInRange[index]);           //verrebbe rimosso dalla lista
            }
        }

    }
    //questo metodo controlla qual'� l'enemy pi� vicino alla torretta per prenderlo di mira
    void GetClosest(ref EnemyManager target)
    {
        target = null;                                                  //come condizione iniziale non ci dev'essere nessun target, in modo che solo i nemici che entrano nel range e sono pi� vicini alla torretta possano diventarlo
        foreach (EnemyManager enemy in targetsInRange)                  //ogni nemico nel range della torretta
        {
            if (target == null)                                         //se non esistono target
            {
                target = enemy;                                         //diventa un target
            }
            else                                                        //se invece esistono gi� dei target
            {
                if (GetDistance(target) > GetDistance(enemy))           //se la loro distanza dalla torretta � maggiore di quella dell'enemy nel range...
                {
                    target = enemy;                                     //l'enemy pi� vicino diventa il nuovo target
                }
            }
        }
    }
    //questo metodo controlla la distanza tra il target e la torretta
    float GetDistance(EnemyManager target)
    {
        return Vector3.Distance(this.transform.position, target.transform.position);    //restituisco la distanza tra il buff e il target
    }

    float time = 0;                     //il timer relativo al rate
    bool triggerTimer = true;           //la condizione per cui il timer scatta o la torretta spara
    private void timer()
    {
        time += Time.deltaTime;
        if (time >= rateOfFire)         //quando il timer raggiunge il valore del rate
        {
            triggerTimer = true;        //il timer diventa true (perci� � pronto a sparare)
            time = 0;                   //e il timer viene resettato
        }
    }

    //creo un metodo di shooting
    private void Shoot()
    {
        GameObject myBullet = Instantiate(bullet);                                                  //istanzio il proiettile
        myBullet.transform.position = this.transform.position + new Vector3(0, 0.6f, 0);            //gli do la posizione di inizio (leggermente rialzata perch� il baricentro della torretta � pi� basso)
        myBullet.transform.rotation = this.transform.rotation;                                      //gli do la stessa rotazione della torretta
        myBullet.GetComponent<trackingBullet>().SetupBullet(target, bulletSpeed, bulletDamage);     //cerco il component trackingBullet nel proiettile e gli setto le statistiche (target, velocit� e danno)
    }

    //all'ingresso dell'enemy nel trigger della torretta...
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetsInRange.Add(other.GetComponent<EnemyManager>());     //l'enemy viene aggiunto alla lista di target nel range
    }

    //all'uscita dell'enemy dal trigger della torretta...
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetsInRange.Remove(other.GetComponent<EnemyManager>());  //l'enemy viene rimosso dalla lista di target nel range
    }
}