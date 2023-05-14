using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//la classe che aumenta il range delle torrette, eredita dal BuffManager il movimento e la selezione degli enemy da colpire
public class Buff_Range : BuffManager
{
    BuffPositionCheck bpC;                                                  //questo è il collider che conterrà tutte le torrette influenzate della pila
    private void Start()
    {
        bpC = FindObjectOfType<BuffPositionCheck>();
    }
    protected override void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)            //mi assicuro che il gioco sia in gameRunning
        {
            base.Update();                                                  //richiamo dalla classe base l'update

            foreach (TurretManager turret in bpC.turretComponents)          //ad ogni torretta che sia all'interno del trigger
            {
                turret.range = 12;                                          //assegno un nuovo range più elevato
            }
        }
    }
}
