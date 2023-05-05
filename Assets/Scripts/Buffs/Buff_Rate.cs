using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Rate : BuffManager
{
    TurretManager tM;
    private void Start()
    {
        tM = FindObjectOfType<TurretManager>();
        if (tM.rateOfFire > 1)
            tM.rateOfFire -= 1;
    }
}
