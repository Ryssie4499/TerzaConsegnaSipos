using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPositionCheck : MonoBehaviour
{
    public List<TurretManager> turretComponents = new List<TurretManager>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Turret"))
        {
            turretComponents.Add(other.GetComponent<TurretManager>());
        }
    }
}
