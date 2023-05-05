using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Damage : BuffManager
{
    TurretManager tM;
    private void Start()
    {
        tM = FindObjectOfType<TurretManager>();
        tM.bulletDamage += 1; 
    }
}
