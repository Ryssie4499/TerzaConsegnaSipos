using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject GuideCanvas;
    public Text enemies;
    public GameObject[] lights;
    public Image[] hearts;
    public int numOfHearts;
    public bool accelerate;
    InputManager IM;
    public GameManager GM;
    [HideInInspector] public int num;
    private void Start()
    {
        IM = FindObjectOfType<InputManager>();
        GM.gameStatus = GameManager.GameStatus.gameStart;
    }
    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            enemies.text = StatsManager.Instance.Score.ToString();

            if (IM.placeIt)
            {
                IM.Placement(num);
            }
            if (IM.buffPlaceIt)
            {
                IM.BuffPlacement(num);
            }
            if (StatsManager.Instance.Health < numOfHearts)
            {
                numOfHearts = StatsManager.Instance.Health;
            }
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < StatsManager.Instance.Health)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }
        if (GM.gameStatus == GameManager.GameStatus.gameStart)
        {
            StartCanvas.SetActive(true);
        }
    }


    public void Monkey()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 0;
            IM.placeIt = true;
        }
    }
    public void Duck()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 1;
            IM.placeIt = true;
        }
    }
    public void Elephant()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 2;
            IM.placeIt = true;
        }
    }
    public void Frog()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 3;
            IM.buffPlaceIt = true;
        }
    }
    public void Eagle()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 4;
            IM.buffPlaceIt = true;
        }
    }
    public void Koala()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            num = 5;
            IM.buffPlaceIt = true;
        }
    }
    public void PLAY()
    {
        StartCanvas.SetActive(false);
        GuideCanvas.SetActive(false);
        Debug.Log("Giocaaaa");
        GM.gameStatus = GameManager.GameStatus.gameRunning;
    }

    public void GUIDE()
    {
        Debug.Log("Guideee");
        StartCanvas.SetActive(false);
        GuideCanvas.SetActive(true);
        GM.gameStatus = GameManager.GameStatus.Guide;
    }

    public void Accelerate()
    {
        if (!accelerate)
        {
            Time.timeScale = 2f;
            accelerate = true;
        }
        else
        {
            Time.timeScale = 1f;
            accelerate = false;
        }
        
    }
}
