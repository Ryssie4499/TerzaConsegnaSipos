using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPositionCheck : MonoBehaviour
{
    public List<TurretManager> turretComponents = new List<TurretManager>();
    public GameManager GM;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Turret") && GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            turretComponents.Add(other.GetComponent<TurretManager>());
        }
    }
}
