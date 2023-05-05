using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private static StatsManager instance;
    public static StatsManager Instance { get => instance; private set => instance = value; }

    private static int highScore = 0;

    public int Health { get; private set; }
    public int Score { get; private set; }
    public int HighScore { get => highScore; }
    //public GameManager GM;
    UIManager uM;
    private void Awake()
    {
        uM = FindObjectOfType<UIManager>();
        instance = this;
        Health = 5;
        Score = 0;
    }

    private void OnEnable()
    {
        EnemyManager.OnFinishLineReached += TakeDamage;
        EnemyManager.OnEnemyDeath += ScoreUp;
    }

    private void OnDisable()
    {
        EnemyManager.OnFinishLineReached -= TakeDamage;
        EnemyManager.OnEnemyDeath -= ScoreUp;
    }

    private void TakeDamage()
    {
        Health--;
        Debug.Log(Health);

        if (Health <= 0)
        {
            GameOver();
        }
    }
    public void Healing()
    {
        if (Health < 5)
        {
            Health++;
        }
    }
    private void ScoreUp()
    {
        Score++;
        highScore = Mathf.Max(highScore, Score);
    }

    private void GameOver()
    {
        //uM.EndCanvas.SetActive(true);
        //GM.gameStatus = GameManager.GameStatus.gameEnd;
    }
}
