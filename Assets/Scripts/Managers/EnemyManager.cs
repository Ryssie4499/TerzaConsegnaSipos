using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static event Action OnFinishLineReached;
    public static event Action OnEnemyDeath;
    [SerializeField]
    public GameObject[] patrolPoints;

    public float movementSpeed; // velocity molt
    public float rotationSpeed;

    public float health; // health & damage

    public float timer;
    UIManager UM;
    public GameManager GM;
    private void Start()
    {
        UM = FindObjectOfType<UIManager>();
        transform.position = patrolPoints[0].transform.position;
    }

    void FixedUpdate()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
            MoveAround();
    }

    public int index = 0;
    void MoveAround()
    {
        if (UM.turtle)
        {
            timer += Time.deltaTime;
            if (timer < 7)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[index].transform.position, 0.2f);
            }
            else
            {
                UM.turtle = false;
                timer = 0;
            }
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[index].transform.position, movementSpeed);

        Vector3 targetDirection = patrolPoints[index].transform.position - transform.position;
        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (Vector3.Distance(transform.position, patrolPoints[index].transform.position) <= 0)
        {
            if (patrolPoints.Length - 1 != index)
                index++;
        }
    }

    public void Damage(float Damage)
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            health -= Damage;
            if (health <= 0)
            {
                Destroy(gameObject);
                OnEnemyDeath?.Invoke();
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Village") && GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            Destroy(gameObject);
            OnFinishLineReached?.Invoke();
        }
    }
}
