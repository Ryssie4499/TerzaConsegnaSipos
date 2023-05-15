using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Canvas e UI")]
    public GameObject StartCanvas, GuideCanvas, PauseCanvas, EndCanvas;     //i menu di inizio, tutorial, pausa e fine
    public GameObject ButtonOn, ButtonOff;                                  //i pulsanti dell'audio (acceso e spento)
    public GameObject Page1, Page2, Page3;                                  //le pagine della guida
    public Text enemies;                                                    //il conteggio di nemici uccisi
    public TextMeshProUGUI record;                                          //il record visibile nel menu di start
    public TextMeshProUGUI finalScore;                                      //il punteggio visibile alla fine della partita
    public GameObject[] tips;                                               //i consigli di fine gioco
    public Image[] hearts;                                                  //le piume che fungono da vite

    [Header("SFX e VFX")]
    public AudioSource Soundtrack;                                          //la musica di sottofondo
    public GameObject[] lights;                                             //le luci che si accendono ogni volta che selezioni una torretta per segnalare le postazioni disponibili

    [HideInInspector] public int num;                                       //il numero che corrisponde alla torretta da piazzare
    [HideInInspector] public int numOfHearts;                               //il numero di vite

    [HideInInspector] public bool accelerate;                               //la condizione secondo la quale il gioco è a velocità normale o accelerata
    [HideInInspector] public bool sfxOn;                                    //la condizione secondo la quale l'audio è acceso o è spento

    //Refs
    InputManager IM;
    public GameManager GM;

    private void Start()
    {
        IM = FindObjectOfType<InputManager>();
        sfxOn = true;                                                       //di default la musica è accesa
        tips[Random.Range(0, 4)].SetActive(true);                           //all'inizio della partita viene scelto il consiglio da mostrare a fine gioco
        GM.gameStatus = GameManager.GameStatus.gameStart;                   //di default il gioco parte in start
    }

    void Update()
    {
        StatsManager stats = StatsManager.Instance;

        record.text = stats.HighScore.ToString();                           //assegno al text del record l'high score calcolato nello StatsManager
        finalScore.text = stats.Score.ToString();                           //assegno al text del final score lo score calcolato nello StatsManager

        if (GM.gameStatus == GameManager.GameStatus.gameRunning)            //in running
        {
            enemies.text = stats.Score.ToString();                          //assegno al text degli enemy uccisi lo score calcolato nello StatsManager

            if (stats.Score % 20 == 0 && stats.Score != 0)                  //se lo score è un multiplo di 20 e non è uguale a zero...
            {
                stats.Healing();                                            //la vita del player aumenta di uno
            }
            else                                                            //in caso contrario
            {
                stats.healing = false;                                      //non guarisce
            }
            if (IM.placeIt)                                                 //se posso piazzare una torretta
            {
                IM.Placement(num);                                          //istanzio la torretta corrispondente al numero scelto
            }
            if (IM.buffPlaceIt)                                             //se posso piazzare un buff
            {
                IM.BuffPlacement(num);                                      //istanzio il buff corrispondente al numero scelto
            }
            if (stats.Health < numOfHearts)                                 //se la vita del player è minore della quantità di piume visibili in scena
            {
                numOfHearts = stats.Health;                                 //le piume si aggiornano
            }
            for (int i = 0; i < hearts.Length; i++)                         //per ogni piuma controllo se...
            {
                if (i < stats.Health)                                       //l'indice della piuma è minore della vita del player
                {
                    hearts[i].enabled = true;                               //e se lo è, la piuma va attivata
                }
                else                                                        //invece se l'indice della piuma è maggiore della vita del player
                {
                    hearts[i].enabled = false;                              //disattivo la piuma corrispondente
                }
            }
        }

        if (GM.gameStatus == GameManager.GameStatus.gameStart)                              //in start
        {
            StartCanvas.SetActive(true);                                                    //viene attivato il menu di start
        }
        if (GM.gameStatus == GameManager.GameStatus.gameEnd)                                //alla fine del gioco
        {
            EndCanvas.SetActive(true);                                                      //viene attivato il menu di fine
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GM.gameStatus == GameManager.GameStatus.gameRunning)        //se clicco il tasto ESC e il gioco è in running si attiva il menu di pausa
        {
            GM.gameStatus = GameManager.GameStatus.gamePaused;
            PauseCanvas.SetActive(true);
        }
        else if (GM.gameStatus == GameManager.GameStatus.gamePaused && Input.GetKeyDown(KeyCode.Escape))    //se sono in pausa e clicco il tasto ESC
        {
            PLAY();                                                                                         //richiamo la funzione che mi permette di tornare in running
        }
    }

#region TURRETS_&_BUFFS
    public void Monkey()                            //la scimmia è la torretta che spara a raffica
    {
        foreach (GameObject light in lights)        //ogni luce nella lista
        {
            light.SetActive(true);                  //viene accesa quando si clicca sull'icona della scimmia
            num = 0;                                //l'indice della scimmia è 0
            IM.placeIt = true;                      //e dal momento in cui ho scelto la scimmia, posso piazzarla
        }
    }
    public void Duck()                              //la papera è la torretta che spara colpi normali
    {
        foreach (GameObject light in lights)        //ogni luce nella lista
        {
            light.SetActive(true);                  //viene accesa quando si clicca sull'icona della papera
            num = 1;                                //l'indice della papera è 1
            IM.placeIt = true;                      //e dal momento in cui ho scelto la papera, posso piazzarla
        }
    }
    public void Elephant()                          //l'elefante è la torretta che spara colpi ad area
    {
        foreach (GameObject light in lights)        //ogni luce nella lista
        {
            light.SetActive(true);                  //viene accesa quando si clicca sull'icona dell'elefante
            num = 2;                                //l'indice dell'elefante è 2
            IM.placeIt = true;                      //e dal momento in cui ho scelto l'elefante, posso piazzarlo
        }
    }
    public void Frog()                              //la rana è il buff che aumenta il rate
    {
        foreach (GameObject light in lights)        //ogni luce nella lista
        {
            light.SetActive(true);                  //viene accesa quando si clicca sull'icona della rana
            num = 3;                                //l'indice della rana è 3
            IM.buffPlaceIt = true;                  //e dal momento in cui ho scelto la rana, posso piazzarla
        }
    }
    public void Eagle()                             //l'aquila è il buff che aumenta il range
    {
        foreach (GameObject light in lights)        //ogni luce nella lista
        {
            light.SetActive(true);                  //viene accesa quando si clicca sull'icona dell'aquila
            num = 4;                                //l'indice dell'aquila è 4
            IM.buffPlaceIt = true;                  //e dal momento in cui ho scelto l'aquila, posso piazzarla
        }
    }
    public void Koala()                             //il koala è il buff che aumenta il damage
    {
        foreach (GameObject light in lights)        //ogni luce nella lista
        {
            light.SetActive(true);                  //viene accesa quando si clicca sull'icona del koala
            num = 5;                                //l'indice del koala è 5
            IM.buffPlaceIt = true;                  //e dal momento in cui ho scelto il koala, posso piazzarlo
        }
    }
    #endregion
#region BUTTONS
    public void PLAY()                                              //il tasto play del menu di start, delle pagine di ogni guida ed il continue del menu di pausa
    {
        //disattivo i vari menu per entrare in gioco
        StartCanvas.SetActive(false);
        GuideCanvas.SetActive(false);
        Page1.SetActive(true);
        Page2.SetActive(false);
        Page3.SetActive(false);
        PauseCanvas.SetActive(false);
        GM.gameStatus = GameManager.GameStatus.gameRunning;
    }

    public void GUIDE()                                             //il tasto guide del menu di start e del menu di pausa
    {
        //disattivo i vari menu per entrare in quello dei tutorial
        StartCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        GuideCanvas.SetActive(true);
        GM.gameStatus = GameManager.GameStatus.Guide;
    }

    public void NEXTGUIDE()                                         //il tasto next della prima pagina di guida: apre la seconda pagina e chiude la prima           >>
    {
        Page1.SetActive(false);
        Page2.SetActive(true);
    }

    public void LASTGUIDE()                                         //il tasto next della seconda pagina di guida: apre la terza pagina e chiude la seconda         >>
    {
        Page2.SetActive(false);
        Page3.SetActive(true);
    }

    public void PREVIOUSGUIDE()                                     //il tasto back della terza pagina di guida: chiude la terza pagina e apre la seconda            <<
    {
        Page3.SetActive(false);
        Page2.SetActive(true);
    }

    public void FIRSTGUIDE()                                        //il tasto back della seconda pagina di guida: chiude la seconda pagina e apre la prima         <<
    {
        Page2.SetActive(false);
        Page1.SetActive(true);
    }

    public void RESTART()                                           //il tasto restart del menu di pausa e retry del menu di fine
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //resetta la scena corrente tranne il record
    }

    public void EXIT()                                              //il tasto quit di tutti i menu: fa uscire dal gioco
    {
        Application.Quit();
    }

    public void Turtle()                                            //il tasto a forma di tartaruga che si accende verso metà partita per rallentare i nemici
    {
        IM.turtle = true;
    }
    public void Accelerate()                                        //il tasto a forma di coniglio che accelera i tempi di gioco
    {
        if (!accelerate)                                            //se il tempo scorre normale, lo accelera (lo raddoppia)
        {
            Time.timeScale = 2f;
            accelerate = true;
        }
        else                                                        //se il tempo scorre veloce, torna normale (si dimezza)
        {
            Time.timeScale = 1f;
            accelerate = false;
        }

    }
    public void AUDIO()                                             //il tasto audio che si trova solo in gioco: disattiva e riattiva l'audio di gioco
    {
        if (sfxOn)                                                  //se l'audio è attivo e clicco il pulsante, il volume si azzera (così se lo riattivi non ricomincia da capo) e si attiva il pulsante per riaccendere il volume
        {
            Soundtrack.volume = 0;
            ButtonOff.SetActive(true);
            ButtonOn.SetActive(false);
            sfxOn = false;
        }
        else                                                       //se l'audio è muto e clicco il pulsante, il volume torna al massimo e si riattiva il pulsante per abbassarlo
        {
            Soundtrack.volume = 1;
            ButtonOff.SetActive(false);
            ButtonOn.SetActive(true);
            sfxOn = true;
        }
    }
    #endregion
}
