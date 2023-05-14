using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//la classe che aumenta il rate dei proiettili, eredita dalla classe base BuffManager il movimento e la selezione degli enemy da colpire
public class Buff_Rate : BuffManager
{
    BuffPositionCheck bpC;                                              //questo è il collider che contiene tutta la pila di torrette
    private void Start()
    {
        bpC = FindObjectOfType<BuffPositionCheck>();
    }
    protected override void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)        //se il gioco è in running
        {
            base.Update();                                              //richiamo l'update della classe base
            foreach (TurretManager turret in bpC.turretComponents)      //ad ogni torretta interna al trigger
            {
                turret.rateOfFire = 0.5f;                               //assegno un rate maggiore (volutamente uguale a quello della torretta a mitragliatrice)
            }
        }
    }
}
