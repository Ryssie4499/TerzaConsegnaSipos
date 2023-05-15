using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public bool placeIt;              //la condizione secondo la quale posso o non posso posizionare una torretta
    [HideInInspector] public bool buffPlaceIt;          //la condizione secondo la quale posso o non posso posizionare un buff
    [HideInInspector] public bool turtle;               //tartaruga attiva o disattiva
    [HideInInspector] public bool used;                 //tartaruga usata o non usata
    [HideInInspector] public bool max;                  //il massimo di torrette piazzabili è/non è stato raggiunto

    [HideInInspector] public int placementCounter;      //torrette piazzate

    [HideInInspector] public float timer;               //timer di gioco (ogni quanto si può piazzare una torretta)
    [HideInInspector] public float randomicTimer;       //timer dell'ultima wave (ogni quanto si può piazzare le ultime torrette randomiche)

    public LayerMask placeMask;                         //layer dove si possono piazzare sia buff che torrette (sopra le torrette e i buff)
    public LayerMask platform;                          //layer dove si possono piazzare solo le torrette (sopra le pedane)

    public GameObject[] turrets;                        //le torrette
    public GameObject Monkey, Duck, Elephant, Frog, Eagle, Koala, Turtle;   //i pulsanti

    public float rate;                                  //il rate tra l'attivazione di un pulsante randomico e l'altro

    //Refs
    UIManager UM;
    public GameManager GM;
    void Start()
    {
        UM = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)                                    //in running
        {
            timer += Time.deltaTime;                                                                //parte un timer
            Activation();                                                                           //e cominciano ad attivarsi i pulsanti delle torrette disponibili
        }
    }

    //creo un metodo per piazzare le torrette
    public void Placement(int num)
    {
        if (Input.GetButtonDown("Mouse0"))                                                          //col tasto sinistro del mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                            //faccio partire un raggio dalla camera al punto esatto sul quale è posizionato il mouse
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeMask | platform))                //se si tratta di una pedana o di una torretta
            {
                if (hit.transform.position.y < 31)                                                  //e se la posizione del cursore è sotto i 31 di altezza
                {
                    Instantiate(turrets[num], hit.transform.position, Quaternion.identity);         //istanzio la torretta corrispondente nella posizione cliccata con il mouse
                    foreach (GameObject light in UM.lights)                                         //e tutte le luci della lista si spengono
                    {
                        light.SetActive(false);
                    }
                    Destroy(hit.transform.gameObject);                                              //elimino il collider sotto la torretta appena piazzata così non si può più piazzarvi sopra altre torrette
                    placeIt = false;                                                                //una volta piazzata la torretta non ne posso piazzare altre dello stesso tipo senza ricliccare sull'icona corrispondente
                    placementCounter++;                                                             //il conteggio delle torrette e dei buff piazzati aumenta
                }
            }
        }
    }

    //creo un metodo per piazzare i buff
    public void BuffPlacement(int num)
    {
        if (Input.GetButtonDown("Mouse0"))                                                          //col tasto sinistro del mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                            //faccio partire un raggio dalla camera al punto esatto sul quale è posizionato il mouse
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeMask))                           //se si tratta di una torretta o di un buff
            {
                if (hit.transform.position.y < 31)                                                  //e se si trova sotto i 31 di altezza
                {
                    Instantiate(turrets[num], hit.transform.position, Quaternion.identity);         //istanzio il buff corrispondente nella posizione cliccata con il mouse
                    foreach (GameObject light in UM.lights)                                         //e tutte le luci della lista si spengono
                    {
                        light.SetActive(false);
                    }
                    Destroy(hit.transform.gameObject);                                              //elimino il collider sotto il buff appena piazzato così non si può più piazzarvi sopra altre torrette
                    buffPlaceIt = false;                                                            //una volta piazzato il buff non ne posso piazzare altri dello stesso tipo senza ricliccare sull'icona corrispondente
                    placementCounter++;                                                             //il conteggio delle torrette e dei buff piazzati aumenta
                }
            }
        }
    }

    //attivazione dei pulsanti per un gameflow equilibrato
    public void Activation()
    {
        int index = 0;
        if (placementCounter == 2 || placementCounter == 8)
        {
            Duck.SetActive(false);
            Eagle.SetActive(true);
        }
        if (placementCounter == 3 || placementCounter == 9)
            Eagle.SetActive(false);
        if ((placementCounter == 3 && timer >= 15) || (placementCounter == 9 && timer >= 90))
            Monkey.SetActive(true);
        if (placementCounter == 4 || placementCounter == 10)
            Monkey.SetActive(false);
        if ((placementCounter == 4 && timer >= 30) || (placementCounter == 10 && timer >= 100))
            Frog.SetActive(true);
        if (placementCounter == 5 || placementCounter == 11)
            Frog.SetActive(false);
        if ((placementCounter == 5 && timer >= 40) || (placementCounter == 11 && timer >= 110))
            Elephant.SetActive(true);
        if (placementCounter == 6 || placementCounter == 12)
        {
            Elephant.SetActive(false);
            Koala.SetActive(true);
        }
        if (placementCounter == 7 || placementCounter == 13)
            Koala.SetActive(false);
        if ((placementCounter == 7 && timer >= 60))
            Duck.SetActive(true);
        if (timer >= 95 && used == false)
            Turtle.SetActive(true);
        if(placementCounter == 25)
        {
            max = true;
            Koala.SetActive(false);
            Frog.SetActive(false);
            Monkey.SetActive(false);
            Eagle.SetActive(false);
            Duck.SetActive(false);
            Elephant.SetActive(false);
        }

        //dopo i 120 secondi, se non è ancora stato raggiunto il numero massimo di torrette piazzate
        if (timer >= 120 && max == false)
        {
            randomicTimer += Time.deltaTime;        //parte un nuovo timer
            if (randomicTimer >= rate)              //e ogni tot secondi attiva randomicamente uno dei pulsanti, anche se dovesse essere già attivo (per dare un po' più di difficoltà al gioco che sta volgendo al termine)
            {
                index = Random.Range(0, 6);
                switch (index)
                {
                    case 0:
                        Duck.SetActive(true);
                        break;
                    case 1:
                        Eagle.SetActive(true);
                        break;
                    case 2:
                        Monkey.SetActive(true);
                        break;
                    case 3:
                        Frog.SetActive(true);
                        break;
                    case 4:
                        Elephant.SetActive(true);
                        break;
                    case 5:
                        Koala.SetActive(true);
                        break;
                }
                randomicTimer = 0;
            }
        }
        //una volta raggiunto il numero massimo di torrette piazzabili, non si può far altro che rallentare i nemici con la tartaruga e accelerare i tempi con il coniglio, in attesa della propria sconfitta
    }
}
