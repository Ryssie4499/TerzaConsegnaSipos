using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameManager GM;
    [SerializeField]
    public GameObject[] patrolPoints; 
    
    public int enemiesCount;

    public float movementSpeed; // velocity molt

    public float health; // health & damage
    private void Start()
    {
        //SetupPatrolPoints();
        transform.position = patrolPoints[0].transform.position;
        enemiesCount++;
    }

    void FixedUpdate()
    {
        MoveAround();
    }

    public int index = 0;
    void MoveAround()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[index].transform.position, movementSpeed);
        if (Vector3.Distance(transform.position, patrolPoints[index].transform.position) <= 0)
        {
            if (patrolPoints.Length - 1 != index)
                index++;
        }
    }

    public void Damage(float Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    //void SetupPatrolPoints()
    //{
    //    patrolPoints = GM.mapPoints;
    //}
}
