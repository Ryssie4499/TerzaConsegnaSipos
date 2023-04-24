using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/Managers/GameManager")]
public class GameManager : ScriptableObject
{
    public enum GameStatus
    {
        gameStart,
        gameRunning,
        gamePaused,
        gameEnd
    }
    public GameStatus gameStatus;
}
