using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private EnemyManager target;                                            //ogni oggetto contenente lo script EnemyManager sar� considerato un target

    [Header("Setup Buff:")]
    public float range;                                                     //il buff avr� un range entro il quale un target pu� essere preso di mira
    public float rotationSpeed;                                             //e avr� anche una velocit� di rotazione
    public List<EnemyManager> targetsInRange = new List<EnemyManager>();    //creo una lista che conterr� tutti i target all'interno del range
    public GameObject fire;                                                 //il fuoco che si accende solo sopra all'ultima torretta della pila e solo se ha raggiunto la massima altezza

    //Refs
    public CapsuleCollider cc;                                              //c'� bisogno di una reference al Collider della torretta (cos� potr� essere della stessa dimensione del range di default)
    public GameManager GM;
    protected virtual void Update()
    {
        cc.radius = range;                                                  //il collider aumenta all'aumentare del raggio d'azione del buff
        cc.height = range;                                                  //anche l'altezza del collider � la stessa del range

        if (transform.position.y > 27)                                      //se la posizione in altezza del buff � maggiore di 27 attivo il fuoco (visto che non posso piazzare altre torrette sopra)
        {
            fire.SetActive(true);
        }

        if (GM.gameStatus == GameManager.GameStatus.gameRunning)            //in game running
        {
            CheckTargetsInRange();                                          //controllo se i target nel range sono ancora vivi e se non lo sono, li rimuovo dalla lista

            GetClosest(ref target);                                         //e scelgo quale target mirare per primo (d� la preferenza al pi� vicino)

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
    protected virtual void CheckTargetsInRange()
    {
        for (int index = 0; index < targetsInRange.Count; index++)      //controllo l'intera lista di target all'interno del range
        {
            if (targetsInRange[index].health <= 0)                      //se la vita di uno di loro fosse minore di zero
            {
                targetsInRange.Remove(targetsInRange[index]);           //verrebbe rimosso dalla lista
            }
        }
    }

    //questo metodo controlla qual'� l'enemy pi� vicino al buff per prenderlo di mira
    protected virtual void GetClosest(ref EnemyManager target)
    {
        target = null;                                                  //come condizione iniziale non ci dev'essere nessun target, in modo che solo i nemici che entrano nel range e sono pi� vicini al buff possano diventarlo
        foreach (EnemyManager enemy in targetsInRange)                  //ogni nemico nel range del buff
        {
            if (target == null)                                         //se non esistono target
            {
                target = enemy;                                         //diventa un target
            }
            else                                                        //se invece esistono gi� dei target
            {
                if (GetDistance(target) > GetDistance(enemy))           //se la loro distanza dal buff � maggiore di quella dell'enemy nel range...
                {
                    target = enemy;                                     //l'enemy pi� vicino diventa il nuovo target
                }
            }
        }
    }

    //questo metodo controlla la distanza tra il target e il buff
    protected virtual float GetDistance(EnemyManager target)
    {
        if (target != null)                                                                     //una volta accertata l'esistenza del target
            return Vector3.Distance(this.transform.position, target.transform.position);        //restituisco la distanza tra il buff e il target
        else                                                                                    //se invece non dovesse pi� esistere un target
            return 0;                                                                           //la funzione non restituir� alcun valore e terminer� il controllo
    }

    //all'ingresso dell'enemy nel trigger del buff...
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetsInRange.Add(other.GetComponent<EnemyManager>());     //l'enemy viene aggiunto alla lista di target nel range
    }

    //all'uscita dell'enemy dal trigger del buff...
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetsInRange.Remove(other.GetComponent<EnemyManager>());  //l'enemy viene rimosso dalla lista di target nel range
    }

}
