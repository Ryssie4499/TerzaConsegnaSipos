using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Range : BuffManager
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
                turret.range = 12;
            }
        }
    }
}
