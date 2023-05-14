using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private EnemyManager target;                                            //ogni oggetto contenente lo script EnemyManager sarà considerato un target

    [Header("Setup Buff:")]
    public float range;                                                     //il buff avrà un range entro il quale un target può essere preso di mira
    public float rotationSpeed;                                             //e avrà anche una velocità di rotazione
    public List<EnemyManager> targetsInRange = new List<EnemyManager>();    //creo una lista che conterrà tutti i target all'interno del range
    public GameObject fire;                                                 //il fuoco che si accende solo sopra all'ultima torretta della pila e solo se ha raggiunto la massima altezza

    //Refs
    public CapsuleCollider cc;                                              //c'è bisogno di una reference al Collider della torretta (così potrà essere della stessa dimensione del range di default)
    public GameManager GM;
    protected virtual void Update()
    {
        cc.radius = range;                                                  //il collider aumenta all'aumentare del raggio d'azione del buff
        cc.height = range;                                                  //anche l'altezza del collider è la stessa del range

        if (transform.position.y > 27)                                      // se la posizione in altezza del buff è maggiore di 27 attivo il fuoco (visto che non posso piazzare altre torrette sopra)
        {
            fire.SetActive(true);
        }

        if (GM.gameStatus == GameManager.GameStatus.gameRunning)            //in game running
        {
            CheckTargetsInRange();                                          //controllo quali sono i target all'interno del range

            GetClosest(ref target);                                         //e scelgo quale target mirare per primo (dà la preferenza al più vicino)

            foreach (EnemyManager enemy in targetsInRange)                  //per ogni nemico nel range
            {
                if (enemy != null)                                          //se il nemico esiste ancora
                {
                    Vector3 targetDirection = enemy.transform.position - transform.position;                                //
                    float singleStep = rotationSpeed * Time.deltaTime;                                                      //
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);     //
                    transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));           //
                }
            }
        }
    }


    protected virtual void CheckTargetsInRange()
    {
        for (int index = 0; index < targetsInRange.Count; index++)
        {
            if (targetsInRange[index].health <= 0)
            {
                targetsInRange.Remove(targetsInRange[index]);
            }
        }
    }

    protected virtual void GetClosest(ref EnemyManager target)
    {
        target = null;
        foreach (EnemyManager enemy in targetsInRange)
        {
            if (target == null)
            {
                target = enemy;
            }
            else
            {
                if (GetDistance(target) > GetDistance(enemy))
                {
                    target = enemy;
                }
            }
        }
    }

    protected virtual float GetDistance(EnemyManager target)
    {
        if (target != null)
            return Vector3.Distance(this.transform.position, target.transform.position);
        else
            return 0;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetsInRange.Add(other.GetComponent<EnemyManager>());
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetsInRange.Remove(other.GetComponent<EnemyManager>());
    }

}
