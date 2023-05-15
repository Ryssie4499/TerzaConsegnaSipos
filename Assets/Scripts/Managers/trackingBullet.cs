using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingBullet : MonoBehaviour
{
    public float bulletSpeed;           //velocità del proiettile
    public float bulletDamage;          //danno del proiettile

    public EnemyManager target;         //ogni oggetto in scena contenente lo script EnemyManager verrà considerato un target
    public GameManager GM;
    protected virtual void FixedUpdate()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)                                                                                    //se il gioco è in running
        {
            if (target != null)                                                                                                                     //ed esiste un target...
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0, 5, 0), bulletSpeed);        //faccio muovere il proiettile in direzione del target (un po' più in alto, così può colpire anche l'aquila)
                if (transform.position == target.transform.position + new Vector3(0, 5, 0))                                                         //e se raggiunge la posizione del target
                {
                    Destroy(gameObject);                                                                                                            //distruggo il proiettile
                }
            }
            else                                                                                                                                    //quando non esiste più il target
                Destroy(gameObject);                                                                                                                //distruggo il proiettile
        }
    }
    //creo un setup del bullet che richiamerò in TurretManager
    public void SetupBullet(EnemyManager Target, float BulletSpeed, float BulletDamage)
    {
        target = Target;
        bulletSpeed = BulletSpeed;
        bulletDamage = BulletDamage;
    }

    //al contatto del proiettile con il target, se esiste ancora, gli infligge danno e si auto-distrugge
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (target!=null && other.gameObject == target.gameObject)
        {
            other.GetComponent<EnemyManager>().Damage(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}
