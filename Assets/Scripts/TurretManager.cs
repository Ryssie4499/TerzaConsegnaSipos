using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    private EnemyManager target;


    public CapsuleCollider cc;

    [Header("Setup Turret:")]
    public float range;
    public float rateOfFire;

    [Header("Setup Bullet:")]
    public GameObject bullet;
    public float bulletDamage;
    public float bulletSpeed;
    public float rotationSpeed;

    public List<EnemyManager> targetsInRange = new List<EnemyManager>();

    private void OnValidate()
    {
        cc.radius = range;
        cc.height = range;
    }

    private void Start()
    {

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
            GetClosest(ref target);
        }

        if (target != null)
        {
            triggerTimer = false;
            Shoot();
            target = null;
        }
        foreach (EnemyManager enemy in targetsInRange)
        {
            Vector3 targetDirection = enemy.transform.position - transform.position;
            float singleStep = rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
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
        myBullet.transform.position = this.transform.position + new Vector3(0, 0.6f, 0);
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