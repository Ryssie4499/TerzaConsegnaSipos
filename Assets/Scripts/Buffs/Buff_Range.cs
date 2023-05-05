using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Range : BuffManager
{
    TurretManager tM;
    private void Start()
    {
        tM = FindObjectOfType<TurretManager>();
        tM.range += 4;
    }
}
