using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private static StatsManager instance;                                                           //utilizzo il singleton per facilitarmi l'accesso alle variabili e ai metodi relativi alle statistiche e per permettermi di mantenere il record anche tra una sessione di gioco e l'altra
    public static StatsManager Instance { get => instance; private set => instance = value; }

    private static int highScore = 0;                                                               //di default il record è uguale a 0
    public int Health { get; private set; }                                                         //la vita del player (visibile attraverso le piume)
    public int Score { get; private set; }                                                          //il conteggio di nemici uccisi (i teschi)
    public int HighScore { get => highScore; }                                                      //il record (nel menu di start sotto la tartaruga)
    public bool healing;                                                                            //la condizione di guarigione

    //Refs
    public GameManager GM;
    private void Awake()
    {
        instance = this;                                                                            //mi assicuro che l'unico StatsManager sia questo
        Health = 5;                                                                                 //la vita iniziale del player è al massimo (5)
        Score = 0;                                                                                  //lo score è azzerato ad ogni sessione
    }
    private void OnEnable()
    {
        EnemyManager.OnFinishLineReached += TakeDamage;                                             //richiamo l'azione relativa all'arrivo dell'enemy alla sua destinazione (il villaggio) e reco danno al player
        EnemyManager.OnEnemyDeath += ScoreUp;                                                       //richiamo l'azione relativa alla morte dell'enemy e aumento il punteggio
    }

    private void OnDisable()                                                                        //quando si resetta la scena, torno alle condizioni di inizio senza danno e senza punteggio
    {
        EnemyManager.OnFinishLineReached -= TakeDamage;
        EnemyManager.OnEnemyDeath -= ScoreUp;
    }

    //questo metodo arreca danno al player e se la vita scende a 0, il gioco va in game over
    private void TakeDamage()
    {
        Health--;

        if (Health <= 0)
        {
            GameOver();
        }
    }

    //questo metodo cura il player di una sola vita
    public void Healing()
    {
        if (Health < 5)
        {
            if (healing == false)
            {
                Health++;
                healing = true;
            }
        }
    }

    //questo metodo aumenta il punteggio di gioco e il record (che fa un controllo per assicurarsi di essere sempre uguale o maggiore al punteggio attuale
    private void ScoreUp()                                                                          
    {
        Score++;
        highScore = Mathf.Max(highScore, Score);
    }

    //questo metodo fa finire lo status di gioco a gameEnd
    private void GameOver()
    {
        GM.gameStatus = GameManager.GameStatus.gameEnd;
    }
}
