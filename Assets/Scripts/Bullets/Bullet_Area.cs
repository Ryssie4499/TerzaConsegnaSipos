using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Area : trackingBullet
{

    public float explosionRange;
    void Start()
    {

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
    void Update()
    {
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);

            foreach (Collider collider in colliders)
            {
                if (collider.GetComponent<EnemyManager>() != null)
                {
                    collider.GetComponent<EnemyManager>().Damage(bulletDamage);
                }
            }
            Destroy(this.gameObject);
        }
    }
}
