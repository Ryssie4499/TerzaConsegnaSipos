using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingBullet : MonoBehaviour
{
    public EnemyManager target;
    public float bulletSpeed;
    public float bulletDamage;

    void FixedUpdate()
    {
        Debug.Log("Update");
        transform.position =  Vector3.MoveTowards(transform.position, target.transform.position, bulletSpeed);

        if(transform.position == target.transform.position)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetupBullet(EnemyManager Target, float BulletSpeed, float BulletDamage)
    {
        target = Target;
        bulletSpeed = BulletSpeed;
        bulletDamage = BulletDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target.gameObject)
        {
            other.GetComponent<EnemyManager>().Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }
    }
}
