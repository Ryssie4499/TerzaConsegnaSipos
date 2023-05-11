using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Damage : BuffManager
{
    BuffPositionCheck bpC;
    private void Start()
    {
        bpC = FindObjectOfType<BuffPositionCheck>();
    }
    protected override void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            base.Update();
            foreach (TurretManager turret in bpC.turretComponents)
            {
                turret.bulletDamage = 3;
            }
        }
    }
}
