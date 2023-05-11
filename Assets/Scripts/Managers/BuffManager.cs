using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private EnemyManager target;

    public CapsuleCollider cc;

    [Header("Setup Turret:")]
    public float range;
    public float rotationSpeed;
    public List<EnemyManager> targetsInRange = new List<EnemyManager>();
    public GameManager GM;

    protected virtual void Update()
    {
        cc.radius = range;
        cc.height = range;
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            CheckTargetsInRange();

            GetClosest(ref target);

            if (target != null)
            {
                target = null;
            }
            foreach (EnemyManager enemy in targetsInRange)
            {
                if (enemy != null)
                {
                    Vector3 targetDirection = enemy.transform.position - transform.position;
                    float singleStep = rotationSpeed * Time.deltaTime;
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                    transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
                }
            }
        }
    }


    protected virtual void CheckTargetsInRange()
    {
        for (int index = 0; index < targetsInRange.Count; index++)
        {
            if (targetsInRange[index].health <= 0)
            {
                targetsInRange.Remove(targetsInRange[index]);
            }
        }
    }

    protected virtual void GetClosest(ref EnemyManager target)
    {
        target = null;
        foreach (EnemyManager enemy in targetsInRange)
        {
            if (target == null)
            {
                target = enemy;
            }
            else
            {
                if (GetDistance(target) > GetDistance(enemy))
                {
                    target = enemy;
                }
            }
        }
    }

    protected virtual float GetDistance(EnemyManager target)
    {
        if (target != null)
            return Vector3.Distance(this.transform.position, target.transform.position);
        else
            return 0;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetsInRange.Add(other.GetComponent<EnemyManager>());
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetsInRange.Remove(other.GetComponent<EnemyManager>());
    }

}
