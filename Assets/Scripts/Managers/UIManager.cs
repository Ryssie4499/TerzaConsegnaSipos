using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject GuideCanvas;
    public GameObject PauseCanvas;
    public GameObject EndCanvas;
    public GameObject ButtonOn, ButtonOff;
    public GameObject Page1;
    public GameObject Page2;
    public GameObject Page3;
    public GameObject[] tips;
    public GameObject[] lights;

    public AudioSource Soundtrack;

    public Text enemies;
    public TextMeshProUGUI record;
    public TextMeshProUGUI finalScore;

    public Image[] hearts;
    public int numOfHearts;

    [HideInInspector] public int num;
    public bool accelerate;
    public bool sfxOn;

    InputManager IM;
    public GameManager GM;

    private void Start()
    {
        sfxOn = true;
        tips[Random.Range(0, 4)].SetActive(true);
        IM = FindObjectOfType<InputManager>();
        GM.gameStatus = GameManager.GameStatus.gameStart;
    }

    void Update()
    {
        record.text = StatsManager.Instance.HighScore.ToString();
        finalScore.text = StatsManager.Instance.Score.ToString();

        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            enemies.text = StatsManager.Instance.Score.ToString();

            if (StatsManager.Instance.Score % 20 == 0 && StatsManager.Instance.Score != 0)
            {
                StatsManager.Instance.Healing();
            }
            else
            {
                StatsManager.Instance.healing = false;
            }
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
        if (GM.gameStatus == GameManager.GameStatus.gameEnd)
        {
            EndCanvas.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GM.gameStatus == GameManager.GameStatus.gameRunning)        //se clicco il tasto ESC e il gioco è in running si attiva il menù di pausa
        {
            GM.gameStatus = GameManager.GameStatus.gamePaused;
            PauseCanvas.SetActive(true);
        }
        else if (GM.gameStatus == GameManager.GameStatus.gamePaused && Input.GetKeyDown(KeyCode.Escape))    //se sono in pausa e clicco il tasto ESC
        {
            PLAY();                                                                                         //richiamo la funzione che mi permette di tornare in running
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
        Page1.SetActive(true);
        Page2.SetActive(false);
        Page3.SetActive(false);
        PauseCanvas.SetActive(false);
        GM.gameStatus = GameManager.GameStatus.gameRunning;
    }

    public void GUIDE()
    {
        StartCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        GuideCanvas.SetActive(true);
        GM.gameStatus = GameManager.GameStatus.Guide;
    }

    public void NEXTGUIDE()
    {
        Page1.SetActive(false);
        Page2.SetActive(true);
    }

    public void LASTGUIDE()
    {
        Page2.SetActive(false);
        Page3.SetActive(true);
    }

    public void PREVIOUSGUIDE()
    {
        Page3.SetActive(false);
        Page2.SetActive(true);
    }

    public void FIRSTGUIDE()
    {
        Page2.SetActive(false);
        Page1.SetActive(true);
    }

    public void RESTART()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EXIT()
    {
        Application.Quit();
    }

    public void Turtle()
    {
        IM.turtle = true;
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
    public void AUDIO()
    {
        if (sfxOn)
        {
            Soundtrack.volume = 0;
            ButtonOff.SetActive(true);
            ButtonOn.SetActive(false);
            sfxOn = false;
        }
        else
        {
            Soundtrack.volume = 1;
            ButtonOff.SetActive(false);
            ButtonOn.SetActive(true);
            sfxOn = true;
        }
    }
}
