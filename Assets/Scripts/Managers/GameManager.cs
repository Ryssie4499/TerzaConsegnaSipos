using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//essendo il gameStatus una variabile utilizzata da tutti gli script ho deciso di farla diventare uno scriptable object accessibile da qualsiasi classe gli faccia riferimento
[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/Managers/GameManager")]
public class GameManager : ScriptableObject
{
    public enum GameStatus
    {
        gameStart,                      //menu di inizio
        gameRunning,                    //gioco: ogni azione di gioco funziona solo in running
        gamePaused,                     //menu di pausa
        gameEnd,                        //menu di fine gioco
        Guide                           //guide/tutorial di gioco
    }
    public GameStatus gameStatus;
}
