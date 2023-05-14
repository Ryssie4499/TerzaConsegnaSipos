using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPositionCheck : MonoBehaviour
{
    public List<TurretManager> turretComponents = new List<TurretManager>();                            //creo una lista di torrette influenzabili

    //Refs
    public GameManager GM;

    private void OnTriggerEnter(Collider other)                                                         //tutte le torrette che entrano in contatto con il trigger del buff (quelle sulla stessa pila)...
    {
        if (other.CompareTag("Turret") && GM.gameStatus == GameManager.GameStatus.gameRunning)          //solo se si tratta di torrette (non di buff) e il gioco è in running
        {
            turretComponents.Add(other.GetComponent<TurretManager>());                                  //...vengono aggiunte alla lista di quelle da influenzare
        }
    }
}
