using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Essendo l'unico tipo di proiettile diverso, ho creato una classe a parte che erediti le stesse proprietà degli altri proiettili, modificando però le condizioni di trigger per il danno
public class Bullet_Area : trackingBullet
{
    //range dell'esplosione ad area: tutto ciò che sta all'interno di quell'area viene distrutto
    public float explosionRange;

    //non richiamo la classe base perchè se lo facessi, il danno sarebbe raddoppiato, perciò qui do un tipo diverso di danno al proiettile che colpisca ad area anzichè in un solo punto
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)                                                  //quando il proiettile entra in contatto con il target (il nemico entrato nel range della torretta)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);       //creo un array contenente i collider che si trovano all'interno del range dell'esplosione

            foreach (Collider collider in colliders)                                                //per ogni collider appartenente all'array di colliders colpiti...
            {
                if (collider.GetComponent<EnemyManager>() != null)                                  //se esiste un component EnemyManager nell'object che contiene il collider colpito...
                {
                    collider.GetComponent<EnemyManager>().Damage(bulletDamage);                     //cerco il metodo relativo al damage nel component EnemyManager presente sullo stesso enemy a cui appartiene il collider colpito
                }
            }
            Destroy(this.gameObject);                                                               //distruggo il proiettile
        }
    }
}
