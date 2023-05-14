using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//la classe del Buff che aumenta il damage dei proiettili, eredita le stesse proprietà di movimento e di selezione degli enemy da colpire dal BuffManager
public class Buff_Damage : BuffManager
{
    //Refs
    BuffPositionCheck bpC;                                                  //questo è il collider che conterrà tutte le torrette della pila

    private void Start()
    {
        bpC = FindObjectOfType<BuffPositionCheck>();
    }
    protected override void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)            //assicurandomi che il gioco sia in running...
        {
            base.Update();                                                  //richiamo l'update del BuffManager
            foreach (TurretManager turret in bpC.turretComponents)          //e poi ad ogni torretta che si trova all'interno del collider...
            {
                turret.bulletDamage = 3;                                    //assegno il nuovo valore del damage (di proposito uguale a quello del colpo ad area)
            }
        }
    }
}
