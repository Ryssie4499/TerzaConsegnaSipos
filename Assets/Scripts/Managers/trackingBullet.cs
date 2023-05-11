using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingBullet : MonoBehaviour
{
    public EnemyManager target;
    public float bulletSpeed;
    public float bulletDamage;
    public GameManager GM;
    protected virtual void FixedUpdate()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            if (target != null)
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0, 5, 0), bulletSpeed);
            else
                Destroy(gameObject);
            if (target != null)
            {
                if (transform.position == target.transform.position + new Vector3(0, 5, 0))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    public void SetupBullet(EnemyManager Target, float BulletSpeed, float BulletDamage)
    {
        target = Target;
        bulletSpeed = BulletSpeed;
        bulletDamage = BulletDamage;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (target!=null && other.gameObject == target.gameObject)
        {
            other.GetComponent<EnemyManager>().Damage(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}
