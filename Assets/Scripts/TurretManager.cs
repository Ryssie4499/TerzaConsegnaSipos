using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    private EnemyManager target;

    public enum Target { Closest, Furthest, Strongest };

    public CapsuleCollider cc;

    [Header("Setup Turret:")]
    public Target focusType;
    public float range;
    public float rateOfFire;

    [Header("Setup Bullet:")]
    public GameObject bullet;
    public float bulletDamage;
    public float bulletSpeed;

    public List<EnemyManager> targetsInRange = new List<EnemyManager>();

    private void OnValidate()
    {
        cc.radius = range;
        cc.height = range;
    }

    private void Start()
    {
        Debug.Log("Start");
    }


    private void Update()
    {
        CheckTargetsInRange();

        if (!triggerTimer)
        {
            timer();
        }
        else
        {
            switch (focusType) //restituire dall'array il tipo EnemyPatrol che rispetta la condizione sottostante
            {
                case Target.Closest: // restituire il più vicino
                    GetClosest(ref target);

                    break;

                case Target.Furthest: // restituire il più lontano
                    GetFurthest(ref target);

                    break;

                case Target.Strongest: // restituire quello con più vita
                    GetStrongest(ref target);

                    break;

                default:
                    Debug.Log("Enum out of bound!");
                    break;
            }

            if (target != null)
            {
                triggerTimer = false;
                Shoot();
                target = null;
            }

        }

    }

    void CheckTargetsInRange()
    {
        foreach (EnemyManager enemy in targetsInRange)
        {
            if (enemy.health <= 0)
            {
                targetsInRange.Remove(enemy);
            }
        }
    }

    void GetClosest(ref EnemyManager target)
    {
        target = null;
        foreach (EnemyManager enemy in targetsInRange) // 2
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

    void GetFurthest(ref EnemyManager target)
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
                if (GetDistance(target) < GetDistance(enemy))
                {
                    target = enemy;
                }
            }
        }
    }

    void GetStrongest(ref EnemyManager target)
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
                if (target.health > enemy.health)
                {
                    target = enemy;
                }
            }
        }
    }

    float GetDistance(EnemyManager target)
    {
        return Vector3.Distance(this.transform.position, target.transform.position);
    }

    float time = 0;
    bool triggerTimer = true;
    private void timer()
    {
        time += Time.deltaTime;
        if (time >= rateOfFire)
        {
            triggerTimer = true;
            time = 0;
        }
    }

    private void Shoot()
    {
        GameObject myBullet = Instantiate(bullet);
        myBullet.transform.position = this.transform.position;
        myBullet.transform.rotation = this.transform.rotation;
        myBullet.GetComponent<trackingBullet>().SetupBullet(target, bulletSpeed, bulletDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetsInRange.Add(other.GetComponent<EnemyManager>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetsInRange.Remove(other.GetComponent<EnemyManager>());
    }
}
